using PSW.Core.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : BehaviourSingleton<BattleSystem>
{
    public BattlePlay BattlePlay { get; set; } // ���� ������ ����ǰ� �ִ� PlayType
    public CardType PlayedElementType { get; set; } // ���� �÷��� �� ���õǰ� �ִ� ElementType
    public BattleEnemy TargetEnemy { get; set; } // Ÿ������ ������ ��

    [Header("Settings")]
    [SerializeField] private Camera battleCamera;
    [SerializeField] private Canvas battleCanvas;
    [SerializeField] private Canvas rewardCanvas;

    [Header("Battle Enemy")]
    [SerializeField] private Transform enemyField;
    [SerializeField] private ToggleGroup enemyToggleGroup;
    [SerializeField] private EnemyEncounter enemyEncounter;
    [SerializeField] private GameObject enemyPrefab;
    public List<BattleEnemy> BattleEnemys = new();

    [Header("Script")]
    [SerializeField] private GameBoard gameBoard;
    [SerializeField] private BattlePlayer battlePlayer;
    [SerializeField] private RewardsSystem battleRewards;

    [Header("Debug")]
    [SerializeField] private BattleDebug battleDebug;

    protected override void Awake()
    {
        base.Awake();

        this.PlayedElementType = CardType.None;

        GameManager.Instance.CameraChange(this.battleCamera, this.battleCanvas);
        GameManager.Instance.CameraChange(this.rewardCanvas);

        BattleEnemySetting();
    }

    private void BattleEnemySetting()
    {
        // Enemy ��ī���� ����
        if (Player.Instance != null)
        {
            this.enemyEncounter = GameManager.Instance.EnemyEncounter;
        }

        // Enemy ����
        for (int i = 0; i < this.enemyEncounter.SpawnCount; i++)
        {
            var battleEnemy = Instantiate(this.enemyPrefab, this.enemyField).GetComponent<BattleEnemy>();
            this.BattleEnemys.Add(battleEnemy);

            battleEnemy.SetPosition(this.enemyEncounter.SpawnCount, i);
            battleEnemy.Set(this, this.enemyToggleGroup, this.enemyEncounter.SpawnEnemys[i]);
        }

        // Enemy Type �� ���� BGM ����
        if (this.enemyEncounter.EnemyType == EnemyType.Elite)
        {
            AudioBGM.Instance.BGMChange(AudioBGM.Instance.elite);
        }
        else if (this.enemyEncounter.EnemyType == EnemyType.Boss)
        {
            AudioBGM.Instance.BGMChange(AudioBGM.Instance.boss);
        }

        // Enemy Type �� ���� ���� ����
        this.battleRewards.SetBattleRewards(this.enemyEncounter.EnemyType);
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
    /// �ڷ�ƾ Start�� �� ����
    /// </summary>
    /// <returns></returns>
    private IEnumerator Start()
    {
        // ���� ����
        this.gameBoard.SetBoard();

        yield return YieldCache.WaitForSeconds(1.0f);

        // ���� ���� (�ݺ�)
        while (true)
        {
            // �÷��̾� �ൿ �� 
            yield return StartCoroutine(PlayerTurn());
            // �� �ൿ ��
            yield return StartCoroutine(EnemyTurn());
        }
    }

    /// <summary>
    /// ���� üũ
    /// </summary>
    public void BattleCheck(BattlePlay battleType)
    {
        this.BattlePlay = battleType;

        StopAllCoroutines();
        StartCoroutine(BattleEnd());
    }

    /// <summary>
    /// ���� �� => ���� �� ��� ���
    /// </summary>
    /// <returns></returns>
    public IEnumerator BattleEnd()
    {
        yield return YieldCache.WaitForSeconds(1.5f);

        // ���� ��
        if (this.BattlePlay == BattlePlay.PlayerDead)
        {
            GameOver();
        }
        else if (this.BattlePlay == BattlePlay.EnemyAllDead)
        {
            GameClear();
        }
    }

    /// <summary>
    /// �÷��̾� �ൿ
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayerTurn()
    {
        if (this.battlePlayer.OnStart)
        {
            yield return StartCoroutine(this.battlePlayer.PlayerOnStart());
        }
        else
        {
            this.battlePlayer.PlayerOnInit();
        }

        yield return StartCoroutine(BattleNotice.Instance.UpdateNotice("Player Turn"));

        // �� ��ų ����
        foreach (var battleEnemy in this.BattleEnemys)
        {
            battleEnemy.EnemySkillInstance(this.gameBoard);
        }

        // �÷��̾� �ൿ �ݺ�
        // �ּ��� 2�� �ൿ�� �Ҹ��ؾ� �۵� => �ൿ���� 1 ������ ��� �� ����
        while (this.battlePlayer.CurrentACT > 1)
        {
            // �÷��̾� card ����
            yield return this.gameBoard.WaitForSelection();
            // ���� ���õ� cards �Ҹ� 
            yield return this.gameBoard.DespawnSelection();
            // �� ������ cards �̵� 
            yield return this.gameBoard.WaitForMovement();
            // �Ҹ� �� cards �� ��ŭ �����
            yield return this.gameBoard.RespawnCards();
            // �ൿ ���� �� �� ���� üũ
            EnemyStateCheck();
        }
    }

    /// <summary>
    /// �� �ൿ ��
    /// </summary>
    /// <returns></returns>
    public IEnumerator EnemyTurn()
    {
        // �� �ʱ�ȭ
        foreach (var battleEnemy in this.BattleEnemys)
        {
            battleEnemy.Init();
        }

        yield return StartCoroutine(BattleNotice.Instance.UpdateNotice("Enemy Turn"));

        // �� ��ų ���
        foreach (var battleEnemy in this.BattleEnemys)
        {
            yield return StartCoroutine(battleEnemy.EnemyUseSkill());
        }
    }

    /// <summary>
    /// �� ���� üũ
    /// </summary>
    /// <returns></returns>
    private void EnemyStateCheck()
    {
        foreach (var battleEnemy in this.BattleEnemys)
        {
            battleEnemy.EnemyCheckState();
        }
    }

    /// <summary>
    /// ���� �� �÷��̾� ���
    /// </summary>
    /// <returns></returns>
    public void GameOver()
    {
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
    public void GameClear()
    {
        this.battleRewards.Show();
    }

    /// <summary>
    /// �÷��̰� ������ elements �������� ���� �� �ҷ���
    /// �׸��� ���õ� elements�� despawned
    /// </summary>
    private void OnElementsDespawned()
    {
        var selectElements = this.gameBoard.GetSelectCards();

        switch (PlayedElementType)
        {
            case CardType.Attack:
                this.battlePlayer.PlayerAttack(selectElements, this.TargetEnemy);
                break;
            case CardType.Defense:
                this.battlePlayer.PlayerShield(selectElements);
                break;
        }
    }
}
