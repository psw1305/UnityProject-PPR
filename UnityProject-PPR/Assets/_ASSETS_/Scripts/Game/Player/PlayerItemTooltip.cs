using PSW.Core.Enums;
using PSW.Core.Extensions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerItemTooltip : BehaviourSingleton<PlayerItemTooltip>
{
    public bool IsShow { get; private set; }

    [Header("Relic")]
    [SerializeField] private CanvasGroup relicTooltipCanvas;
    [SerializeField] private Button relicTooltipClose;
    [SerializeField] private Image relicIcon;
    [SerializeField] private TextMeshProUGUI relicName;
    [SerializeField] private TextMeshProUGUI relicRare;
    [SerializeField] private TextMeshProUGUI relicAbility;
    [SerializeField] private TextMeshProUGUI relicDesc;

    [Header("Potion")]
    [SerializeField] private CanvasGroup potionTooltipCanvas;
    [SerializeField] private Button potionTooltipClose;
    [SerializeField] private Image potionIcon;
    [SerializeField] private TextMeshProUGUI potionName;
    [SerializeField] private TextMeshProUGUI potionDesc;

    [Header("Card")]
    [SerializeField] private CanvasGroup cardTooltipCanvas;
    [SerializeField] private Button cardTooltipClose;
    [SerializeField] private Image cardIcon;
    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private TextMeshProUGUI cardDesc;

    protected override void Awake()
    {
        base.Awake();

        this.IsShow = false;

        this.relicTooltipCanvas.CanvasInit();
        this.potionTooltipCanvas.CanvasInit();

        this.relicTooltipClose.onClick.AddListener(RelicTooltipHide);
        this.potionTooltipClose.onClick.AddListener(PotionTooltipHide);
    }

    /// <summary>
    /// 해당 아이템 데이터에 맞게 UI 부여
    /// </summary>
    /// <param name="invenItem">유물 아이템</param>
    private void SetTooltip(InventoryItem invenItem)
    {
        var itemData = invenItem.GetItemData();

        // 아이템 타입에 따른 정보 구분
        if (itemData.ItemType == ItemType.Relic)
        {
            this.relicIcon.sprite = itemData.ItemImage;
            this.relicName.text = itemData.ItemName;
        }
        else if (itemData.ItemType == ItemType.Potion)
        {
            this.potionIcon.sprite = itemData.ItemImage;
            this.potionName.text = itemData.ItemName;
        }
    }

    /// <summary>
    /// 유물 아이템 툴팁 표시
    /// </summary>
    /// <param name="invenItem">인벤토리 아이템</param>
    public void RelicTooltipShow(InventoryItem invenItem)
    {
        if (this.IsShow == true) return;

        UISFX.Instance.Play(UISFX.Instance.itemOpens);

        SetTooltip(invenItem);

        this.IsShow = true;
        this.relicTooltipClose.interactable = true;
        this.relicTooltipCanvas.CanvasFadeIn(Fade.CANVAS_FADE_TIME);
    }

    /// <summary>
    /// 유물 아이템 툴팁 숨김
    /// </summary>
    private void RelicTooltipHide()
    {
        if (this.IsShow == false) return;

        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        this.IsShow = false;
        this.relicTooltipClose.interactable = false;
        this.relicTooltipCanvas.CanvasFadeOut(Fade.CANVAS_FADE_TIME);
    }

    /// <summary>
    /// 포션 아이템 툴팁 표시
    /// </summary>
    /// <param name="invenItem">인벤토리 아이템</param>
    public void PotionTooltipShow(InventoryItem invenItem)
    {
        if (this.IsShow == true) return;

        UISFX.Instance.Play(UISFX.Instance.itemOpens);

        SetTooltip(invenItem);

        this.IsShow = true;
        this.potionTooltipClose.interactable = true;
        this.potionTooltipCanvas.CanvasFadeIn(Fade.CANVAS_FADE_TIME);
    }

    /// <summary>
    /// 포션 아이템 툴팁 숨김
    /// </summary>
    public void PotionTooltipHide()
    {
        if (this.IsShow == false) return;

        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        this.IsShow = false;
        this.potionTooltipClose.interactable = false;
        this.potionTooltipCanvas.CanvasFadeOut(Fade.CANVAS_FADE_TIME);
    }
}
