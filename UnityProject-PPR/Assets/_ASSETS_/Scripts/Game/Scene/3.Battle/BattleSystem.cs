using PSW.Core.Enums;
using System.Collections;
using UnityEngine;

public class BattleSystem : BehaviourSingleton<BattleSystem>
{
    public GamePlayType GamePlay { get; set; } // ���� ������ ����ǰ� �ִ� PlayType
    public ElementType PlayedElementType { get; set; } // ���� �÷��� �� ���õǰ� �ִ� ElementType

    [SerializeField] private GameBoard board;

    [Header("Battle Enemy")]
    [SerializeField] private EnemyBlueprint enemyBlueprint;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemyField;
    private BattleEnemy battleEnemy;

    protected override void Awake()
    {
        base.Awake();

        this.GamePlay = GamePlayType.Ready;
        this.PlayedElementType = ElementType.None;

        BattleEnemyCreate();
    }

    private void BattleEnemyCreate()
    {
        var enemyClone = Instantiate(this.enemyPrefab, enemyField);
        this.battleEnemy = enemyClone.GetComponent<BattleEnemy>();

        if (Player.Instance != null)
        {
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
        // ���� �ʱ�ȭ
        InitGameBattle();

        yield return new WaitForSeconds(1.0f);

        while (this.GamePlay != GamePlayType.End)
        {
            // �÷��̾� �ൿ �� 
            yield return StartCoroutine(PlayerTurn());

            // �� ����� => ���� ��, ���� Ż��
            if (this.GamePlay == GamePlayType.EnemyDead) break;

            // �� �� ����
            yield return StartCoroutine(EnemyTurn());

            // �� �� ��
            yield return StartCoroutine(EnemyTurnEnd());

            // �÷��̾� ����� => ���� ����, ���� Ż��
            if (this.GamePlay == GamePlayType.PlayerDead) break;
        }

        // ���� ��
        if (this.GamePlay == GamePlayType.EnemyDead)
        {
            // �� ����� => ���� �ޱ�
            yield return StartCoroutine(StageReward());
        }
        else if (this.GamePlay == GamePlayType.PlayerDead)
        {
            // �÷��̾� ����� => ���� ����
            yield return StartCoroutine(GameOver());
        }
    }

    /// <summary>
    /// �� ��ư ������ => �� �ڷΰ���
    /// </summary>
    protected void OnBackButtonClick()
    {
        //SceneLoader.Instance.PlayerCheckSceneLoad(SceneNames.Battle);
    }

    private void Update()
    {
        // escape ��ư üũ
        if (Input.GetKey(KeyCode.Escape))
        {
            OnBackButtonClick();
        }
    }

    /// <summary>
    /// ���� ���� �ʱ�ȭ
    /// </summary>
    public void InitGameBattle()
    {
        // board �¾�
        this.board.SetBoard();
    }

    /// <summary>
    /// �÷��̾� �ൿ
    /// </summary>
    /// <returns></returns>
    public IEnumerator PlayerTurn()
    {
        // �÷��̾� �ʱ�ȭ event
        GameBoardEvents.OnPlayerTurnInit.Invoke();

        yield return StartCoroutine(BattleNotice.Instance.UpdateNotice("Player Turn"));

        // �� ��ų ����
        this.battleEnemy.EnemySkillInstance();

        // �÷��̾� �ൿ �ݺ�
        while (BattlePlayer.CurrentACT > 0 
            && this.GamePlay != GamePlayType.EnemyDead 
            && this.GamePlay != GamePlayType.PlayerDead)
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
    /// �� �ൿ ����
    /// </summary>
    /// <returns></returns>
    public IEnumerator EnemyTurn()
    {
        yield return StartCoroutine(BattleNotice.Instance.UpdateNotice("Enemy Turn"));

        // �� ��ų ���
        yield return StartCoroutine(this.battleEnemy.EnemyUseSkill());
    }

    /// <summary>
    /// �� �ൿ ��
    /// </summary>
    /// <returns></returns>
    public IEnumerator EnemyTurnEnd()
    {
        yield return StartCoroutine(BattleNotice.Instance.UpdateNotice("Enemy End"));

        // �� ��ų �ı�
        yield return StartCoroutine(this.battleEnemy.EnemySkillDestroy());
    }

    /// <summary>
    /// ���� �� => ���� â Ȯ��
    /// </summary>
    /// <returns></returns>
    public IEnumerator StageReward()
    {
        yield return new WaitForSeconds(0.5f);

        BattleReward.Instance.Show();
    }

    /// <summary>
    /// ���� �� => �ٽ� Stage Scene
    /// </summary>
    /// <returns></returns>
    public IEnumerator StageEnd()
    {
        // Stage Scene ���� ���� 
        if (StageSystem.Instance != null)
        {
            yield return new WaitForSeconds(0.5f);
            SceneLoader.Instance.UnLoadAdditiveScene(SceneNames.Battle);
        }
        // �׽�Ʈ�� Battle Scene �ݺ�
        else
        {
            yield return new WaitForSeconds(0.5f);
            SceneLoader.Instance.LoadScene(SceneNames.Battle);
        }
    }

    /// <summary>
    /// �÷��̾� ���
    /// </summary>
    /// <returns></returns>
    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.5f);

        // TODO => Ư�� �κ� �����ϱ�
        // �κ� ������ �ǵ�����
        SceneLoader.Instance.LoadScene(SceneNames.Lobby);
    }

    /// <summary>
    /// �÷��̰� ������ elements �������� ���� �� �ҷ���
    /// �׸��� ���õ� elements�� despawned
    /// </summary>
    private void OnElementsDespawned()
    {
        var selectElements = this.board.GetSelectElements();

        // Update Point
        switch (PlayedElementType)
        {
            case ElementType.Attack:
                BattlePlayer.Instance.PlayerAttack(selectElements, this.battleEnemy);
                break;

            case ElementType.Defense:
                BattlePlayer.Instance.PlayerDefense(selectElements);
                break;

            case ElementType.Potion:
                BattlePlayer.Instance.PlayerRecovery(selectElements);
                break;

            case ElementType.Coin:
                BattlePlayer.Instance.PlayerEarn(selectElements);
                break;
        }
    }

    /// <summary>
    /// �÷��̾� �ൿ�� ���� ���� �� �ൿ ����
    /// </summary>
    /// <returns></returns>
    private IEnumerator EnemyAction()
    {
        yield return StartCoroutine(this.battleEnemy.EnemyUseSkill());
    }
}