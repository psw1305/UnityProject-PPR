using PSW.Core.Enums;
using System.Collections;
using UnityEngine;

public class BattleSystem : BehaviourSingleton<BattleSystem>
{
    public BattleType BattlePlay { get; set; } // 현재 게임이 진행되고 있는 PlayType
    public ElementType PlayedElementType { get; set; } // 게임 플레이 시 선택되고 있는 ElementType

    [SerializeField] private GameBoard board;

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
    /// Start 주기에서 게임 진행
    /// </summary>
    /// <returns></returns>
    private IEnumerator Start()
    {
        // 게임 초기화
        InitGameBattle();

        yield return new WaitForSeconds(1.0f);

        while (true)
        {
            // 플레이어 행동 턴 
            yield return StartCoroutine(PlayerTurn());

            // 적 사망 시 => 전투 끝, 루프 탈출
            if (this.BattlePlay == BattleType.EnemyDead) break;

            // 적 턴 시작
            yield return StartCoroutine(EnemyTurn());

            // 적 턴 끝
            yield return StartCoroutine(EnemyTurnEnd());

            // 플레이어 사망 시 => 게임 오버, 루프 탈출
            if (this.BattlePlay == BattleType.PlayerDead) break;
        }

        // 전투 끝, 플레이어 먼저 체크
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
    /// 게임 전투 초기화
    /// </summary>
    public void InitGameBattle()
    {
        // board 셋업
        this.board.SetBoard();
    }

    /// <summary>
    /// 플레이어 행동
    /// </summary>
    /// <returns></returns>
    public IEnumerator PlayerTurn()
    {
        // 플레이어 초기화 event
        GameBoardEvents.OnPlayerTurnInit.Invoke();

        yield return StartCoroutine(BattleNotice.Instance.UpdateNotice("Player Turn"));

        // 적 스킬 생성
        this.battleEnemy.EnemySkillInstance();

        // 플레이어 행동 반복
        while (BattlePlayer.CurrentACT > 0 
            && this.BattlePlay != BattleType.EnemyDead 
            && this.BattlePlay != BattleType.PlayerDead)
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
    /// 적 행동 시작
    /// </summary>
    /// <returns></returns>
    public IEnumerator EnemyTurn()
    {
        yield return StartCoroutine(BattleNotice.Instance.UpdateNotice("Enemy Turn"));

        // 적 스킬 사용
        yield return StartCoroutine(this.battleEnemy.EnemyUseSkill());
    }

    /// <summary>
    /// 적 행동 끝
    /// </summary>
    /// <returns></returns>
    public IEnumerator EnemyTurnEnd()
    {
        yield return StartCoroutine(BattleNotice.Instance.UpdateNotice("Enemy End"));

        // 적 스킬 파괴
        yield return StartCoroutine(this.battleEnemy.EnemySkillDestroy());
    }

    /// <summary>
    /// 전투 중 플레이어 사망
    /// </summary>
    /// <returns></returns>
    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.5f);

        if (Player.Instance != null)
        {
            // 플레이어 결과 창 생성
            Player.Instance.GameOver();
        }
        else
        {
            SceneLoader.Instance.LoadScene(SceneNames.Lobby);
        }
    }

    /// <summary>
    /// 전투 끝 => 보상 창 확인
    /// </summary>
    /// <returns></returns>
    public IEnumerator GameClear()
    {
        yield return new WaitForSeconds(0.5f);

        BattleReward.Instance.Show();
    }

    /// <summary>
    /// 전투 끝 => 다시 Stage Scene
    /// </summary>
    /// <returns></returns>
    public IEnumerator StageEnd()
    {
        // Stage Scene 으로 복귀 
        if (StageSystem.Instance != null)
        {
            yield return new WaitForSeconds(0.5f);
            SceneLoader.Instance.UnLoadAdditiveScene(SceneNames.Battle);
        }
        // 테스트용 Battle Scene 반복
        else
        {
            yield return new WaitForSeconds(0.5f);
            SceneLoader.Instance.LoadScene(SceneNames.Battle);
        }
    }

    /// <summary>
    /// 플레이가 선택한 elements 움직임이 끝날 때 불러옴
    /// 그리고 선택된 elements는 despawned
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
    /// 플레이어 행동이 끝난 다음 적 행동 시작
    /// </summary>
    /// <returns></returns>
    private IEnumerator EnemyAction()
    {
        yield return StartCoroutine(this.battleEnemy.EnemyUseSkill());
    }
}
