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

    private InventoryItem invenItem;

    protected override void Awake()
    {
        base.Awake();

        this.IsShow = false;

        this.relicTooltipCanvas.CanvasInit();
    }

    /// <summary>
    /// 장비 툴팁 표시
    /// </summary>
    /// <param name="invenItem">인벤토리 아이템</param>
    public void Show(InventoryItem invenItem)
    {
        if (this.IsShow == true) return;

        UISFX.Instance.Play(UISFX.Instance.itemOpens);

        this.IsShow = true;
        //this.tooltipClose.interactable = true;

        SetTooltip(invenItem);

        //this.tooltipCanvas.CanvasFadeIn(0.25f);
    }

    /// <summary>
    /// 장비 툴팁 숨김
    /// </summary>
    public void RelicTooltipHide()
    {
        if (this.IsShow == false) return;

        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        this.IsShow = false;
    }

    /// <summary>
    /// 해당 아이템 데이터에 맞게 UI 부여
    /// </summary>
    /// <param name="relicItem">유물 아이템</param>
    private void SetTooltip(InventoryItem relicItem)
    {
        this.invenItem = relicItem;
        ItemBlueprint itemData = this.invenItem.GetItemData();

        // 아이콘, 이름, 설명 부여
        this.relicIcon.sprite = itemData.ItemImage;
        this.relicName.text = itemData.ItemName;

        // 아이템 타입에 따른 정보 구분
        if (itemData.ItemType == ItemType.Relic)
        {
            //DataInputEquipment((ItemBlueprintArtifact)itemData);
        }
        else if (itemData.ItemType == ItemType.Potion)
        {
            //DataInputPotion(itemData);
        }
        else if (itemData.ItemType == ItemType.Potion)
        {
            //DataInputPotion(itemData);
        }
    }
}
