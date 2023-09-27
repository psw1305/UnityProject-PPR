using PSW.Core.Enums;
using PSW.Core.Extensions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerUI : BehaviourSingleton<PlayerUI>
{
    [Header("Front UI - Stat")]
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI actText;
    [SerializeField] private TextMeshProUGUI cashText;

    [Header("Front UI - Button")]
    [SerializeField] private Button settings;
    [SerializeField] private Button inventory;
    [SerializeField] private Button cardDeck;

    [Header("Inventory")]
    [SerializeField] private CanvasGroup inventoryCanvas;
    [SerializeField] private Button inventoryClose;
    [SerializeField] private Transform relicSlot;
    [SerializeField] private Transform potionSlot;

    [Header("Deck")]
    [SerializeField] private CanvasGroup cardDeckCanvas;
    [SerializeField] private Button cardDeckClose;
    [SerializeField] private Transform cardDragParent;
    [SerializeField] private Transform deckSlot;

    [Header("Game Result")]
    [SerializeField] private CanvasGroup resultCanvas;
    [SerializeField] private GameObject gameClearTitle;
    [SerializeField] private GameObject gameOverTitle;
    [SerializeField] private Button gameEndButton;

    protected override void Awake()
    {
        base.Awake();

        this.inventoryCanvas.CanvasInit();
        this.inventory.onClick.AddListener(InventoryShow);
        this.inventoryClose.onClick.AddListener(InventoryHide);

        this.cardDeckCanvas.CanvasInit();
        this.cardDeck.onClick.AddListener(CardDeckShow);
        this.cardDeckClose.onClick.AddListener(CardDeckHide);

        this.settings.onClick.AddListener(SettingShow);

        this.resultCanvas.CanvasInit();
        this.gameEndButton.onClick.AddListener(GameEnd);
    }

    #region Get
    public Transform GetRelicSlot()
    {
        return this.relicSlot;
    }

    public Transform GetPotionSlot()
    {
        return this.potionSlot;
    }

    public Transform GetDeckSlot()
    {
        return this.deckSlot;
    }

    public Transform GetCardDragParent()
    {
        return this.cardDragParent;
    }
    #endregion

    #region Canvas Show, Hide
    /// <summary>
    /// 인벤토리 창 열기
    /// </summary>
    private void InventoryShow()
    {
        UISFX.Instance.Play(UISFX.Instance.inventoryOpen);
        this.inventoryCanvas.CanvasFadeIn(DUR.CANVAS_FADE_TIME);
    }

    /// <summary>
    /// 인벤토리 창 닫기
    /// </summary>
    private void InventoryHide()
    {
        UISFX.Instance.Play(UISFX.Instance.inventoryClose);
        this.inventoryCanvas.CanvasFadeOut(DUR.CANVAS_FADE_TIME);
    }

    private void CardDeckShow()
    {
        UISFX.Instance.Play(UISFX.Instance.cardDeckOpen);
        this.cardDeckCanvas.CanvasFadeIn(DUR.CANVAS_FADE_TIME);
    }

    /// <summary>
    /// 인벤토리 창 닫기
    /// </summary>
    private void CardDeckHide()
    {
        UISFX.Instance.Play(UISFX.Instance.cardDeckClose);
        this.cardDeckCanvas.CanvasFadeOut(DUR.CANVAS_FADE_TIME);
    }

    private void SettingShow()
    {
        SettingsSystem.Instance.Show();
    }
    #endregion

    /// <summary>
    /// 게임 오버시 => 결과 창 열기
    /// </summary>
    public void GameResult()
    {
        if (Player.Instance.GameState == GameState.Victory)
        {
            AudioBGM.Instance.BGMEnd(AudioBGM.Instance.victory);

            this.gameClearTitle.SetActive(true);
        }
        else if (Player.Instance.GameState == GameState.Defeat)
        {
            AudioBGM.Instance.BGMEnd(AudioBGM.Instance.defeat);

            this.gameOverTitle.SetActive(true);
        }

        this.resultCanvas.CanvasFadeIn(DUR.CANVAS_FADE_TIME);
    }

    /// <summary>
    /// 결과 창 내에 게임 종료 버튼 클릭시 이벤트
    /// </summary>
    public void GameEnd()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);
        this.gameEndButton.interactable = false;

        SceneLoader.Instance.LoadScene(SceneNames.Lobby);
    }

    /// <summary>
    /// Front UI Stat 세팅
    /// </summary>
    public void SetUI()
    {
        SetHPText();
        SetACTText();
        SetCashText();
    }

    public void SetHPText()
    {
        this.hpText.text = Player.Instance.GetHPText();
    }

    public void SetACTText()
    {
        this.actText.text = Player.Instance.GetACTText();
    }

    public void SetCashText()
    {
        this.cashText.text = Player.Instance.Cash.ToString();
    }
}
