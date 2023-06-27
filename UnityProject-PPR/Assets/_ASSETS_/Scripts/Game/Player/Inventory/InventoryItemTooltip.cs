using PSW.Core.Enums;
using PSW.Core.Extensions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemTooltip : BehaviourSingleton<InventoryItemTooltip>
{
    public bool IsShow { get; private set; }

    [Header("UI")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDesc;
    [SerializeField] private Button tooltipClose;

    [Header("Equipment")]
    [SerializeField] private GameObject equipmentTable;
    [SerializeField] private TextMeshProUGUI statHP;
    [SerializeField] private TextMeshProUGUI statMP;
    [SerializeField] private TextMeshProUGUI statSTR;
    [SerializeField] private TextMeshProUGUI statDEF;

    [Header("Useable")]
    [SerializeField] private GameObject useableTable;
    [SerializeField] private TextMeshProUGUI useableAbility;

    private CanvasGroup tooltipCanvas;
    private InventoryItem invenItem;

    protected override void Awake()
    {
        base.Awake();

        this.tooltipCanvas = GetComponent<CanvasGroup>();
        this.tooltipCanvas.CanvasInit();

        this.IsShow = false;
        this.tooltipClose.onClick.AddListener(Hide);
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
        this.tooltipClose.interactable = true;

        UIDataInput(invenItem);

        this.tooltipCanvas.CanvasFadeIn(0.25f);
    }

    /// <summary>
    /// 장비 툴팁 숨김
    /// </summary>
    public void Hide()
    {
        if (this.IsShow == false) return;

        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        this.IsShow = false;
        this.tooltipClose.interactable = false;

        //UIDataInit();

        this.tooltipCanvas.CanvasFadeOut(0.25f);
    }

    /// <summary>
    /// 해당 아이템 데이터에 맞게 UI 부여
    /// </summary>
    /// <param name="invenItem">인벤토리 아이템</param>
    private void UIDataInput(InventoryItem invenItem)
    {
        this.invenItem = invenItem;
        ItemBlueprint itemData = this.invenItem.GetItemData();

        // 아이콘, 이름, 설명 부여
        this.itemIcon.sprite = itemData.ItemImage;
        this.itemName.text = itemData.ItemName;
        this.itemDesc.text = itemData.ItemContents;

        // 아이템 타입에 따른 정보 구분
        if (itemData.ItemType == ItemType.Equipment)
        {
            DataInputEquipment((ItemEquipmentBlueprint)itemData);
        }
        else if (itemData.ItemType == ItemType.Useable)
        {
            DataInputUseable((ItemUseableBlueprint)itemData);
        }
    }

    /// <summary>
    /// 장비 데이터 등록
    /// </summary>
    /// <param name="equipmentData"></param>
    private void DataInputEquipment(ItemEquipmentBlueprint equipmentData)
    {
        if (this.useableTable.activeSelf)
        {
            this.equipmentTable.SetActive(true);
            this.useableTable.SetActive(false);
        }

        // 스탯 부여
        for (int i = 0; i < equipmentData.StatCount; i++)
        {
            StatType statType = equipmentData.ItemStatType(i);
            int statValue = equipmentData.ItemStatValue(i);

            switch (statType)
            {
                case StatType.HP:
                    this.statHP.text = statValue.ToString();
                    break;
                case StatType.ACT:
                    this.statMP.text = statValue.ToString();
                    break;
                case StatType.ATK:
                    this.statSTR.text = statValue.ToString();
                    break;
                case StatType.DEF:
                    this.statDEF.text = statValue.ToString();
                    break;
            }
        }
    }

    /// <summary>
    /// 소모품 데이터 등록
    /// </summary>
    /// <param name="useableData"></param>
    private void DataInputUseable(ItemUseableBlueprint useableData)
    {
        if (this.equipmentTable.activeSelf)
        {
            this.useableTable.SetActive(true);
            this.equipmentTable.SetActive(false);
        }

        this.useableAbility.text = useableData.AbilityDesc;
    }

    /// <summary>
    /// UI 데이터 초기화
    /// </summary>
    private void UIDataInit()
    {
        this.invenItem = null;

        this.statHP.text = "0";
        this.statMP.text = "0";
        this.statSTR.text = "0";
        this.statDEF.text = "0";

        this.useableAbility.text = "";
    }
}
