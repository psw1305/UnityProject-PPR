using PSW.Core.Enums;
using System.Collections;
using UnityEngine;

public class BattleSystem : BehaviourSingleton<BattleSystem>
{
    public GamePlayType GamePlay { get; set; } // 현재 게임이 진행되고 있는 PlayType
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

        while (this.GamePlay != GamePlayType.End)
        {
            // 플레이어 행동 턴 
            yield return StartCoroutine(PlayerTurn());

            // 적 사망시 => 전투 끝, 루프 탈출
            if (this.GamePlay == GamePlayType.EnemyDead) break;

            // 적 턴 시작
            yield return StartCoroutine(EnemyTurn());

            // 적 턴 끝
            yield return StartCoroutine(EnemyTurnEnd());

            // 플레이어 사망시 => 게임 오버, 루프 탈출
            if (this.GamePlay == GamePlayType.PlayerDead) break;
        }

        // 전투 끝
        if (this.GamePlay == GamePlayType.EnemyDead)
        {
            // 적 사망시 => 보상 받기
            yield return StartCoroutine(StageReward());
        }
        else if (this.GamePlay == GamePlayType.PlayerDead)
        {
            // 플레이어 사망시 => 게임 오버
            yield return StartCoroutine(GameOver());
        }
    }

    /// <summary>
    /// 백 버튼 누를시 => 씬 뒤로가기
    /// </summary>
    protected void OnBackButtonClick()
    {
        //SceneLoader.Instance.PlayerCheckSceneLoad(SceneNames.Battle);
    }

    private void Update()
    {
        // escape 버튼 체크
        if (Input.GetKey(KeyCode.Escape))
        {
            OnBackButtonClick();
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
            && this.GamePlay != GamePlayType.EnemyDead 
            && this.GamePlay != GamePlayType.PlayerDead)
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
    /// 전투 끝 => 보상 창 확인
    /// </summary>
    /// <returns></returns>
    public IEnumerator StageReward()
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
    /// 플레이어 사망
    /// </summary>
    /// <returns></returns>
    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.5f);

        // TODO => 특정 부분 저장하기
        // 로비 씬으로 되돌리기
        SceneLoader.Instance.LoadScene(SceneNames.Lobby);
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
