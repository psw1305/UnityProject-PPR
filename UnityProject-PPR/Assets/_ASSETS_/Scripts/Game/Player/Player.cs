using PSW.Core.Enums;
using PSW.Core.Stat;
using UnityEngine;

/// <summary>
/// Player Stat Partial Class
/// </summary>
public partial class Player : BehaviourSingleton<Player>
{
    public GameState GameState { get; set; }

    [Header("Canvas")]
    [SerializeField] private Canvas[] canvases;

    [Header("Player UI")]
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private PlayerItemTooltip tooltip;

    [Header("Player Stat")]
    public Stat HP;
    public Stat ACT;
    public Stat ATK;
    public Stat DEF;

    [Header("Player Battle Stat")]
    public Stat startATK;
    public Stat startDEF;

    public int Cash { get; set; }
    public int CurrentHP { get; set; }
    public EnemyBlueprint BattleEnemy { get; set; }

    protected override void Awake()
    {
        base.Awake();

        foreach (Canvas canvas in this.canvases)
        {
            GameManager.Instance.CameraChange(canvas);
        }

        Setting(80, 20, 99);
    }

    private void Start()
    {
        SetDeck();

        this.playerUI.SetUI();
    }


    private void Setting(int hp, int act, int cash)
    {
        this.HP.BaseValue = hp;
        this.ACT.BaseValue = act;

        this.ATK.BaseValue = 1;
        this.DEF.BaseValue = 1;

        CurrentHP = hp;
        Cash = cash;
    }

    #region Set, Get
    public void SetHP(int currentHp)
    {
        CurrentHP = currentHp;
        this.playerUI.SetHPText();
    }

    public void SetCash(int cash)
    {
        Cash += cash;
        this.playerUI.SetCashText();
    }

    public string GetHPText()
    {
        return CurrentHP.ToString();
    }

    public string GetACTText()
    {
        return ACT.Value.ToString();
    }
    #endregion

    #region 게임 결과 관련
    /// <summary>
    /// 플레이어 게임 클리어 이벤트
    /// </summary>
    public void GameClear()
    {
        this.GameState = GameState.Victory;

        this.playerUI.EndCanvasShow();
    }

    /// <summary>
    /// 플레이어 게임 오버 이벤트
    /// </summary>
    public void GameOver()
    {
        this.GameState = GameState.Defeat;

        this.playerUI.EndCanvasShow();
    }
    #endregion
}
