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
    /// ��� ���� ǥ��
    /// </summary>
    /// <param name="invenItem">�κ��丮 ������</param>
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
    /// ��� ���� ����
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
    /// �ش� ������ �����Ϳ� �°� UI �ο�
    /// </summary>
    /// <param name="invenItem">�κ��丮 ������</param>
    private void UIDataInput(InventoryItem invenItem)
    {
        this.invenItem = invenItem;
        ItemBlueprint itemData = this.invenItem.GetItemData();

        // ������, �̸�, ���� �ο�
        this.itemIcon.sprite = itemData.ItemImage;
        this.itemName.text = itemData.ItemName;
        this.itemDesc.text = itemData.ItemContents;

        // ������ Ÿ�Կ� ���� ���� ����
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
    /// ��� ������ ���
    /// </summary>
    /// <param name="equipmentData"></param>
    private void DataInputEquipment(ItemEquipmentBlueprint equipmentData)
    {
        if (this.useableTable.activeSelf)
        {
            this.equipmentTable.SetActive(true);
            this.useableTable.SetActive(false);
        }

        // ���� �ο�
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
    /// �Ҹ�ǰ ������ ���
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
    /// UI ������ �ʱ�ȭ
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
