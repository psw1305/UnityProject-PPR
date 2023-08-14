using PSW.Core.Enums;
using PSW.Core.Extensions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerItemTooltip : BehaviourSingleton<PlayerItemTooltip>
{
    [Header("Card")]
    [SerializeField] private CanvasGroup cardTooltipCanvas;
    [SerializeField] private Button cardTooltipClose;
    [SerializeField] private Image cardIcon;
    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private TextMeshProUGUI cardDesc;

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
    [SerializeField] private TextMeshProUGUI potionRare;
    [SerializeField] private TextMeshProUGUI potionAbility;
    [SerializeField] private TextMeshProUGUI potionDesc;

    protected override void Awake()
    {
        base.Awake();

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
            this.relicRare.ItemGradeColor(itemData.ItemGrade);
            this.relicName.text = itemData.ItemName;
            this.relicAbility.text = itemData.ItemAbility;
            this.relicDesc.text = itemData.ItemDesc;
        }
        else if (itemData.ItemType == ItemType.Potion)
        {
            this.potionIcon.sprite = itemData.ItemImage;
            this.potionRare.ItemGradeColor(itemData.ItemGrade);
            this.potionName.text = itemData.ItemName;
            this.potionAbility.text = itemData.ItemAbility;
            this.potionDesc.text = itemData.ItemDesc;
        }
    }

    /// <summary>
    /// 유물 아이템 툴팁 표시
    /// </summary>
    /// <param name="invenItem">인벤토리 아이템</param>
    public void RelicTooltipShow(InventoryItem invenItem)
    {
        UISFX.Instance.Play(UISFX.Instance.itemOpens);

        SetTooltip(invenItem);

        this.relicTooltipClose.interactable = true;
        this.relicTooltipCanvas.CanvasFadeIn(DUR.CANVAS_FADE_TIME);
    }

    /// <summary>
    /// 유물 아이템 툴팁 숨김
    /// </summary>
    private void RelicTooltipHide()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        this.relicTooltipClose.interactable = false;
        this.relicTooltipCanvas.CanvasFadeOut(DUR.CANVAS_FADE_TIME);
    }

    /// <summary>
    /// 포션 아이템 툴팁 표시
    /// </summary>
    /// <param name="invenItem">인벤토리 아이템</param>
    public void PotionTooltipShow(InventoryItem invenItem)
    {
        UISFX.Instance.Play(UISFX.Instance.itemOpens);

        SetTooltip(invenItem);

        this.potionTooltipClose.interactable = true;
        this.potionTooltipCanvas.CanvasFadeIn(DUR.CANVAS_FADE_TIME);
    }

    /// <summary>
    /// 포션 아이템 툴팁 숨김
    /// </summary>
    public void PotionTooltipHide()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        this.potionTooltipClose.interactable = false;
        this.potionTooltipCanvas.CanvasFadeOut(DUR.CANVAS_FADE_TIME);
    }
}
