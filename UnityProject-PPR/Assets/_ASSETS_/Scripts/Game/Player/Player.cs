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

    [Header("Script")]
    [SerializeField] private PlayerUI playerUI;

    [Header("Stat")]
    // Player Value => 장비에 영향 O
    public Stat HP;
    public Stat ACT;
    public Stat ATK;
    public Stat DEF;

    public static int Cash { get; set; }
    public static int CurrentHP { get; set; }
    public static EnemyBlueprint BattleEnemy { get; set; }

    protected override void Awake()
    {
        base.Awake();

        foreach (Canvas canvas in this.canvases)
        {
            GameManager.Instance.CameraChange(canvas);
        }

        Set();
    }

    public void Set(int hp = 40, int act = 20, int atk = 1, int def = 1, int cash = 0)
    {
        this.HP.BaseValue = hp;
        this.ACT.BaseValue = act;
        this.ATK.BaseValue = atk;
        this.DEF.BaseValue = def;

        CurrentHP = hp;
        Cash = cash;
    }

    public void SetHp(int currentHp)
    {
        CurrentHP = currentHp;
        this.playerUI.SetHealthUI(GetHpText());
    }

    public string GetHpText()
    {
        return CurrentHP + "/" + this.HP.Value;
    }

    public void SetCash(int cash)
    {
        Cash += cash;
        this.playerUI.SetCashUI();
    }

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
}
