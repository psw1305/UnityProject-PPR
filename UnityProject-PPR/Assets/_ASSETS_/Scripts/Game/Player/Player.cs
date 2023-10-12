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

    [Header("UI")]
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private PlayerItemTooltip tooltip;

    [Header("Base Stat")]
    public Stat HP;
    public Stat ACT;
    public Stat ATK;
    public Stat DEF;

    [Header("Battle Start Stat")]
    public Stat StartDEF;
    public Stat StartCard;

    [Header("Battle First Stat")]
    public Stat FirstDEF;
    public Stat FirstATK;

    public float Cash { get; set; }
    public int CurrentHP { get; set; }

    protected override void Awake()
    {
        base.Awake();

        foreach (Canvas canvas in this.canvases)
        {
            GameManager.Instance.CameraChange(canvas);
        }

        Set(80, 15, 100);
    }

    private void Start()
    {
        this.playerUI.SetUI();

        StarterCardPack();
    }

    private void Set(int hp, int act, int cash)
    {
        this.HP.BaseValue = hp;
        this.ACT.BaseValue = act;

        this.ATK.BaseValue = 1;
        this.DEF.BaseValue = 1;

        this.StartDEF.BaseValue = 0;
        this.StartCard.BaseValue = 0;

        this.FirstATK.BaseValue = 0;
        this.FirstDEF.BaseValue = 0;

        this.CurrentHP = hp;
        this.Cash = cash;
    }

    #region Player HP
    public void SetHP(int currentHp)
    {
        this.CurrentHP = currentHp;
        this.playerUI.SetHPText();
    }

    /// <summary>
    /// 플레이어의 최대 체력
    /// </summary>
    public int GetMaxHP()
    {
        return this.HP.Value;
    }

    /// <summary>
    /// 플레이어 체력 회복
    /// </summary>
    /// <param name="recoveryValue"></param>
    public void Recovery(int recoveryValue)
    {
        var recoveryHP = this.CurrentHP + recoveryValue;

        if (this.HP.Value <= recoveryHP)
        {
            recoveryHP = this.HP.Value;
        }

        SetHP(recoveryHP);
    }
    #endregion

    #region Trade
    /// <summary>
    /// 플레이어 재화 획득
    /// </summary>
    /// <param name="cash"></param>
    public void ObtainCash(int cash)
    {
        this.Cash += cash;
        this.playerUI.SetCashText();
    }

    /// <summary>
    /// 플레이어 재화 사용
    /// </summary>
    /// <param name="cash"></param>
    public bool UseCash(float cash)
    {
        var playerCash = this.Cash;
        playerCash -= cash;

        if (playerCash >= 0)
        {
            this.Cash = playerCash;
            this.playerUI.SetCashText();
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region Game Result
    /// <summary>
    /// 플레이어 게임 클리어 이벤트
    /// </summary>
    public void GameClear()
    {
        this.GameState = GameState.Victory;
        this.playerUI.GameResult();
    }

    /// <summary>
    /// 플레이어 게임 오버 이벤트
    /// </summary>
    public void GameOver()
    {
        this.GameState = GameState.Defeat;
        this.playerUI.GameResult();
    }
    #endregion
}
