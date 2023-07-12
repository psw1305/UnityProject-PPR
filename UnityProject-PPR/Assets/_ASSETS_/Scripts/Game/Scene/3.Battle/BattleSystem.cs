using PSW.Core.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Transform enemyField;
    [SerializeField] private ToggleGroup enemyToggleGroup;
    [SerializeField] private int enemyCount;
    [SerializeField] private List<BattleEnemy> battleEnemys = new();
    [SerializeField] private EnemyBlueprint enemyBlueprint;
    [SerializeField] private GameObject enemyPrefab;

    public List<BattleEnemy> BattleEnemys => this.battleEnemys;  
    public BattleEnemy SelectedEnemy { get; set; }

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
        for (int i = 0; i < this.enemyCount; i++)
        {
            var battleEnemy = Instantiate(this.enemyPrefab, this.enemyField).GetComponent<BattleEnemy>();
            this.battleEnemys.Add(battleEnemy);

            if (Player.Instance != null)
            {
                // �� ���� ��������
                this.enemyBlueprint = Player.BattleEnemy;
            }

            battleEnemy.Set(this, this.enemyToggleGroup, this.enemyBlueprint);
        }

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
            if (this.BattlePlay == BattleType.EnemyAllDead) break;
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
        else if (this.BattlePlay == BattleType.EnemyAllDead)
        {
            yield return StartCoroutine(GameClear());
        }
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    private IEnumerator BattleSetting()
    {
        // game board ����
        this.board.SetBoard();

        yield return YieldCache.WaitForSeconds(1.0f);
    }

    /// <summary>
    /// ���� üũ
    /// </summary>
    public void BattleCheck()
    {
        if (this.BattleEnemys.Count == 0)
        {
            this.BattlePlay = BattleType.EnemyAllDead;
        }
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
        foreach (var battleEnemy in this.battleEnemys)
        {
            battleEnemy.EnemySkillInstance();
        }

        // �÷��̾� �ൿ �ݺ�
        // �ּ��� 2�� �ൿ�� �Ҹ��ؾ� �۵� => �ൿ���� 1 ������ ��� �� ����
        while (this.battlePlayer.CurrentACT > 1 
            && this.BattlePlay != BattleType.EnemyAllDead 
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
        foreach (var battleEnemy in this.battleEnemys)
        {
            battleEnemy.Init();
        }

        yield return StartCoroutine(BattleNotice.Instance.UpdateNotice("Enemy Turn"));

        // �� ��ų ���
        foreach (var battleEnemy in this.battleEnemys)
        {
            yield return StartCoroutine(battleEnemy.EnemyUseSkill());
        }
    }

    /// <summary>
    /// ���� �� �÷��̾� ���
    /// </summary>
    /// <returns></returns>
    public IEnumerator GameOver()
    {
        yield return YieldCache.WaitForSeconds(0.5f);

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
        yield return YieldCache.WaitForSeconds(0.5f);

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
                this.battlePlayer.PlayerAttack(selectElements, this.SelectedEnemy);
                break;
            case ElementType.Defense:
                this.battlePlayer.PlayerDefense(selectElements);
                break;
        }
    }
}
