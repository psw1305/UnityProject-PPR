using PSW.Core.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : BehaviourSingleton<BattleSystem>
{
    public BattlePlay BattlePlay { get; set; } // 현재 게임이 진행되고 있는 PlayType
    public CardType PlayedElementType { get; set; } // 게임 플레이 시 선택되고 있는 ElementType
    public BattleEnemy TargetEnemy { get; set; } // 타겟으로 지정된 적

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
        // Enemy 인카운터 정보
        if (Player.Instance != null)
        {
            this.enemyEncounter = GameManager.Instance.EnemyEncounter;
        }

        // Enemy 생성
        for (int i = 0; i < this.enemyEncounter.SpawnCount; i++)
        {
            var battleEnemy = Instantiate(this.enemyPrefab, this.enemyField).GetComponent<BattleEnemy>();
            this.BattleEnemys.Add(battleEnemy);

            battleEnemy.SetPosition(this.enemyEncounter.SpawnCount, i);
            battleEnemy.Set(this, this.enemyToggleGroup, this.enemyEncounter.SpawnEnemys[i]);
        }

        // Enemy Type 에 따른 BGM 변경
        if (this.enemyEncounter.EnemyType == EnemyType.Elite)
        {
            AudioBGM.Instance.BGMChange(AudioBGM.Instance.elite);
        }
        else if (this.enemyEncounter.EnemyType == EnemyType.Boss)
        {
            AudioBGM.Instance.BGMChange(AudioBGM.Instance.boss);
        }

        // Enemy Type 에 따른 보상 구분
        this.battleRewards.SetBattleRewards(this.enemyEncounter.EnemyType);
    }

    /// <summary>
    /// Element Despawned 관련 Events 리스너 추가
    /// </summary>
    private void OnEnable()
    {
        GameBoardEvents.OnElementsDespawned.AddListener(OnElementsDespawned);
    }

    /// <summary>
    /// Element Despawned 관련 events 리스너 제거
    /// </summary>
    private void OnDisable()
    {
        GameBoardEvents.OnElementsDespawned.RemoveListener(OnElementsDespawned);
    }

    /// <summary>
    /// 코루틴 Start로 턴 제어
    /// </summary>
    /// <returns></returns>
    private IEnumerator Start()
    {
        // 전투 세팅
        this.gameBoard.SetBoard();

        yield return YieldCache.WaitForSeconds(1.0f);

        // 전투 시작 (반복)
        while (true)
        {
            // 플레이어 행동 턴 
            yield return StartCoroutine(PlayerTurn());
            // 적 행동 턴
            yield return StartCoroutine(EnemyTurn());
        }
    }

    /// <summary>
    /// 전투 체크
    /// </summary>
    public void BattleCheck(BattlePlay battleType)
    {
        this.BattlePlay = battleType;

        StopAllCoroutines();
        StartCoroutine(BattleEnd());
    }

    /// <summary>
    /// 전투 끝 => 보상 및 결과 출력
    /// </summary>
    /// <returns></returns>
    public IEnumerator BattleEnd()
    {
        yield return YieldCache.WaitForSeconds(1.5f);

        // 전투 끝
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
    /// 플레이어 행동
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

        // 적 스킬 생성
        foreach (var battleEnemy in this.BattleEnemys)
        {
            battleEnemy.EnemySkillInstance(this.gameBoard);
        }

        // 플레이어 행동 반복
        // 최소한 2의 행동을 소모해야 작동 => 행동력이 1 이하일 경우 턴 종료
        while (this.battlePlayer.CurrentACT > 1)
        {
            // 플레이어 card 선택
            yield return this.gameBoard.WaitForSelection();
            // 최종 선택된 cards 소멸 
            yield return this.gameBoard.DespawnSelection();
            // 빈 곳으로 cards 이동 
            yield return this.gameBoard.WaitForMovement();
            // 소멸 된 cards 수 만큼 재생성
            yield return this.gameBoard.RespawnCards();
            // 행동 끝난 후 적 상태 체크
            EnemyStateCheck();
        }
    }

    /// <summary>
    /// 적 행동 턴
    /// </summary>
    /// <returns></returns>
    public IEnumerator EnemyTurn()
    {
        // 적 초기화
        foreach (var battleEnemy in this.BattleEnemys)
        {
            battleEnemy.Init();
        }

        yield return StartCoroutine(BattleNotice.Instance.UpdateNotice("Enemy Turn"));

        // 적 스킬 사용
        foreach (var battleEnemy in this.BattleEnemys)
        {
            yield return StartCoroutine(battleEnemy.EnemyUseSkill());
        }
    }

    /// <summary>
    /// 적 상태 체크
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
    /// 전투 중 플레이어 사망
    /// </summary>
    /// <returns></returns>
    public void GameOver()
    {
        if (Player.Instance != null)
        {
            // 플레이어 결과 창 생성
            Player.Instance.GameOver();
        }
        else
        {
            // [Debug] Battle Scene 반복
            SceneLoader.Instance.LoadScene(SceneNames.Battle);
        }
    }

    /// <summary>
    /// 전투 끝 => 보상 창 확인
    /// </summary>
    /// <returns></returns>
    public void GameClear()
    {
        this.battleRewards.Show();
    }

    /// <summary>
    /// 플레이가 선택한 elements 움직임이 끝날 때 불러옴
    /// 그리고 선택된 elements는 despawned
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
