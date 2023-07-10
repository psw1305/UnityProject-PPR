using PSW.Core.Enums;
using System.Collections;
using UnityEngine;

public class BattleSystem : BehaviourSingleton<BattleSystem>
{
    public BattleType BattlePlay { get; set; } // ���� ������ ����ǰ� �ִ� PlayType
    public ElementType PlayedElementType { get; set; } // ���� �÷��� �� ���õǰ� �ִ� ElementType

    [Header("Settings")]
    [SerializeField] private Camera battleCamera;
    [SerializeField] private Canvas battleCanvas;
    [SerializeField] private Canvas rewardCanvas;
    [SerializeField] private GameBoard board;
    [SerializeField] private BattlePlayer battlePlayer;

    [Header("Battle Enemy")]
    [SerializeField] private EnemyBlueprint enemyBlueprint;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemyField;
    private BattleEnemy battleEnemy;

    protected override void Awake()
    {
        base.Awake();

        this.PlayedElementType = ElementType.None;

        BattleEnemyCreate();

        GameManager.Instance.CameraChange(this.battleCamera, this.battleCanvas);
        GameManager.Instance.CameraChange(this.rewardCanvas);
    }

    private void BattleEnemyCreate()
    {
        var enemyClone = Instantiate(this.enemyPrefab, enemyField);
        this.battleEnemy = enemyClone.GetComponent<BattleEnemy>();

        if (Player.Instance != null)
        {
            // �� ���� ��������
            this.enemyBlueprint = Player.BattleEnemy;
        }

        this.battleEnemy.Set(this.enemyBlueprint);

        if (this.enemyBlueprint.EnemyType == EnemyType.Elite)
        {
            AudioBGM.Instance.BGMChange(AudioBGM.Instance.elite);
        }
        else if (this.enemyBlueprint.EnemyType == EnemyType.Boss)
        {
            AudioBGM.Instance.BGMChange(AudioBGM.Instance.boss);
        }
    }

    /// <summary>
    /// Element Despawned ���� Events ������ �߰�
    /// </summary>
    private void OnEnable()
    {
        GameBoardEvents.OnElementsDespawned.AddListener(OnElementsDespawned);
    }

    /// <summary>
    /// Element Despawned ���� events ������ ����
    /// </summary>
    private void OnDisable()
    {
        GameBoardEvents.OnElementsDespawned.RemoveListener(OnElementsDespawned);
    }

    /// <summary>
    /// Start �ֱ⿡�� ���� ����
    /// </summary>
    /// <returns></returns>
    private IEnumerator Start()
    {
        // ���� ����
        yield return StartCoroutine(BattleSetting());

        // ���� �÷���
        while (true)
        {
            // �÷��̾� �ൿ �� 
            yield return StartCoroutine(PlayerTurn());
            // �� ��� �� => ���� ��, ���� Ż��
            if (this.BattlePlay == BattleType.EnemyDead) break;
            // �� �ൿ ��
            yield return StartCoroutine(EnemyTurn());
            // �÷��̾� ��� �� => ���� ����, ���� Ż��
            if (this.BattlePlay == BattleType.PlayerDead) break;
        }

        // ���� ��
        if (this.BattlePlay == BattleType.PlayerDead)
        {
            yield return StartCoroutine(GameOver());
        }
        else if (this.BattlePlay == BattleType.EnemyDead)
        {
            yield return StartCoroutine(GameClear());
        }
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    private IEnumerator BattleSetting()
    {
        // board �¾�
        this.board.SetBoard();

        yield return new WaitForSeconds(1.0f);
    }

    /// <summary>
    /// �÷��̾� �ൿ
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayerTurn()
    {
        // �÷��̾� �ʱ�ȭ
        GameBoardEvents.OnPlayerTurnInit.Invoke();

        yield return StartCoroutine(BattleNotice.Instance.UpdateNotice("Player Turn"));

        // �� ��ų ����
        this.battleEnemy.EnemySkillInstance();

        // �÷��̾� �ൿ �ݺ�
        // �ּ��� 2�� �ൿ�� �Ҹ��ؾ� �۵� => �ൿ���� 1 ������ ��� �� ����
        while (this.battlePlayer.CurrentACT > 1 
            && this.BattlePlay != BattleType.EnemyDead 
            && this.BattlePlay != BattleType.PlayerDead)
        {
            // �÷��̾� element ����
            yield return this.board.WaitForSelection();
            // ���� ���õ� elements �Ҹ� 
            yield return this.board.DespawnSelection();
            // �� ������ elements �̵� 
            yield return this.board.WaitForMovement();
            // �Ҹ� �� elements �� ��ŭ �����
            yield return this.board.RespawnElements();
        }
    }

    /// <summary>
    /// �� �ൿ ��
    /// </summary>
    /// <returns></returns>
    public IEnumerator EnemyTurn()
    {
        // �� �ʱ�ȭ
        GameBoardEvents.OnEnemyTurnInit.Invoke();

        yield return StartCoroutine(BattleNotice.Instance.UpdateNotice("Enemy Turn"));
        // �� ��ų ���
        yield return StartCoroutine(this.battleEnemy.EnemyUseSkill());
        // ��ų ����� �ı�
        yield return StartCoroutine(this.battleEnemy.EnemySkillDestroy());
    }

    /// <summary>
    /// ���� �� �÷��̾� ���
    /// </summary>
    /// <returns></returns>
    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.5f);

        if (Player.Instance != null)
        {
            // �÷��̾� ��� â ����
            Player.Instance.GameOver();
        }
        else
        {
            // [Debug] Battle Scene �ݺ�
            SceneLoader.Instance.LoadScene(SceneNames.Battle);
        }
    }

    /// <summary>
    /// ���� �� => ���� â Ȯ��
    /// </summary>
    /// <returns></returns>
    public IEnumerator GameClear()
    {
        yield return new WaitForSeconds(0.5f);

        BattleReward.Instance.Show();
    }

    /// <summary>
    /// �÷��̰� ������ elements �������� ���� �� �ҷ���
    /// �׸��� ���õ� elements�� despawned
    /// </summary>
    private void OnElementsDespawned()
    {
        var selectElements = this.board.GetSelectElements();

        switch (PlayedElementType)
        {
            case ElementType.Attack:
                this.battlePlayer.PlayerAttack(selectElements, this.battleEnemy);
                break;
            case ElementType.Defense:
                this.battlePlayer.PlayerDefense(selectElements);
                break;
        }
    }
}
