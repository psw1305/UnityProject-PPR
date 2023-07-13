using PSW.Core.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : BehaviourSingleton<BattleSystem>
{
    public BattlePlay BattlePlay { get; set; } // 현재 게임이 진행되고 있는 PlayType
    public ElementType PlayedElementType { get; set; } // 게임 플레이 시 선택되고 있는 ElementType

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

        GameManager.Instance.CameraChange(this.battleCamera, this.battleCanvas);
        GameManager.Instance.CameraChange(this.rewardCanvas);

        BattleEnemySetting();
    }

    private void BattleEnemySetting()
    {
        for (int i = 0; i < this.enemyCount; i++)
        {
            var battleEnemy = Instantiate(this.enemyPrefab, this.enemyField).GetComponent<BattleEnemy>();
            this.battleEnemys.Add(battleEnemy);

            if (Player.Instance != null)
            {
                // 적 정보 가져오기
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
        this.board.SetBoard();

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
        // 플레이어 초기화
        GameBoardEvents.OnPlayerTurnInit.Invoke();

        yield return StartCoroutine(BattleNotice.Instance.UpdateNotice("Player Turn"));

        // 적 스킬 생성
        foreach (var battleEnemy in this.battleEnemys)
        {
            battleEnemy.EnemySkillInstance();
        }

        // 플레이어 행동 반복
        // 최소한 2의 행동을 소모해야 작동 => 행동력이 1 이하일 경우 턴 종료
        while (this.battlePlayer.CurrentACT > 1)
        {
            // 플레이어 element 선택
            yield return this.board.WaitForSelection();
            // 최종 선택된 elements 소멸 
            yield return this.board.DespawnSelection();
            // 빈 곳으로 elements 이동 
            yield return this.board.WaitForMovement();
            // 소멸 된 elements 수 만큼 재생성
            yield return this.board.RespawnElements();
        }
    }

    /// <summary>
    /// 적 행동 턴
    /// </summary>
    /// <returns></returns>
    public IEnumerator EnemyTurn()
    {
        // 적 초기화
        foreach (var battleEnemy in this.battleEnemys)
        {
            battleEnemy.Init();
        }

        yield return StartCoroutine(BattleNotice.Instance.UpdateNotice("Enemy Turn"));

        // 적 스킬 사용
        foreach (var battleEnemy in this.battleEnemys)
        {
            yield return StartCoroutine(battleEnemy.EnemyUseSkill());
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
        BattleReward.Instance.Show();
    }

    /// <summary>
    /// 플레이가 선택한 elements 움직임이 끝날 때 불러옴
    /// 그리고 선택된 elements는 despawned
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
