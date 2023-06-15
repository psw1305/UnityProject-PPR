using PSW.Core.Enums;
using PSW.Core.Extensions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : BehaviourSingleton<PlayerUI>
{
    public bool IsOpen { private set; get; }

    [SerializeField] private CanvasGroup playerCanvas;

    [Header("UI")]
    [SerializeField] private Button inventory;
    [SerializeField] private Button settings;
    [SerializeField] private TextMeshProUGUI health;

    [Header("Stat Text")]
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI actText;
    [SerializeField] private TextMeshProUGUI atkText;
    [SerializeField] private TextMeshProUGUI defText;
    [SerializeField] private TextMeshProUGUI cashText;

    [Header("Inventory")]
    [SerializeField] private Button inventoryClose;
    [SerializeField] private Transform invenList;
    [SerializeField] private InventorySlotEquipment[] equipmentLists;
    [SerializeField] private InventorySlotUseable[] useableItemLists;

    protected override void Awake()
    {
        base.Awake();

        this.inventory.onClick.AddListener(InventoryOpen);
        this.inventoryClose.onClick.AddListener(InventoryClose);
        this.settings.onClick.AddListener(SettingsOpen);
    }

    private void InventoryOpen()
    {
        UISFX.Instance.Play(UISFX.Instance.inventoryOpen);
        this.playerCanvas.CanvasFadeIn(0.25f);
    }

    private void InventoryClose()
    {
        UISFX.Instance.Play(UISFX.Instance.inventoryClose);
        this.playerCanvas.CanvasFadeOut(0.25f, new Vector3(300, 0, 0));
    }

    private void SettingsOpen()
    {
        SettingsSystem.Instance.Show();
    }

    /// <summary>
    /// Player UI ����
    /// </summary>
    public void SetUI()
    {
        SetCashUI();
        SetStatUI();
    }

    /// <summary>
    /// Player ��ȭ UI ǥ��
    /// </summary>
    private void SetCashUI()
    {
        this.cashText.text = Player.Cash.ToString();
    }

    /// <summary>
    /// Player ���� UI ǥ��
    /// </summary>
    public void SetStatUI()
    {
        this.hpText.text = Player.Instance.HP.Value.ToString();
        this.actText.text = Player.Instance.ACT.Value.ToString();
        this.atkText.text = Player.Instance.ATK.Value.ToString();
        this.defText.text = Player.Instance.DEF.Value.ToString();

        SetHealthUI(Player.Instance.GetHpText());
    }

    public void SetHealthUI(string healthText)
    {
        this.health.text = healthText;
    }

    /// <summary>
    /// ��� ���� => [0.����][1.����][2.����][3.�׼�����]
    /// </summary>
    /// <param name="num">���� ��ȣ</param>
    /// <param name="invenItem">������ ������</param>
    public void SetUIEquipmentLoad(int num, InventoryItem invenItem)
    {
        invenItem.transform.SetParent(this.equipmentLists[num].transform);
    }

    /// <summary>
    /// �Ҹ�ǰ ����
    /// </summary>
    /// <param name="num">���� ��ȣ</param>
    /// <param name="invenItem">������ ������</param>
    public void SetUIUseableItemLoad(int num, InventoryItem invenItem)
    {
        invenItem.SetSlotNumber(num);
        invenItem.SetParentAfterDrag(this.useableItemLists[num].GetDropSlot());
        invenItem.transform.SetParent(this.useableItemLists[num].GetDropSlot());
    }

    /// <summary>
    /// ���� ������ ���� => �ٽ� �κ��丮 â����
    /// </summary>
    /// <param name="invenItem">������ ������</param>
    public void SetUIItemUnload(InventoryItem invenItem)
    {
        invenItem.SetSlotNumber(0);
        invenItem.transform.SetParent(this.invenList);
    }

    /// <summary>
    /// �κ��丮 ������ �巡�� ���۽� => �ش� ���� �ִϸ��̼�
    /// </summary>
    /// <param name="invenItem">�巡�� ������</param>
    public void ItemOnBeginDrag(InventoryItem invenItem)
    {
        if (invenItem.GetItemType() == ItemType.Equipment)
        {
            AnimateBeginEquipmentSlot(invenItem.GetEquipmentType());
        }
        else if (invenItem.GetItemType() == ItemType.Useable)
        {
            AnimateBeginUseableSlot();
        }
    }

    private void AnimateBeginEquipmentSlot(EquipmentType equipmentType)
    {
        switch (equipmentType)
        {
            case EquipmentType.Helmet:
                this.equipmentLists[0].AnimatePlateImage(1.1f);
                break;
            case EquipmentType.Armor:
                this.equipmentLists[1].AnimatePlateImage(1.1f);
                break;
            case EquipmentType.Weapon:
                this.equipmentLists[2].AnimatePlateImage(1.1f);
                break;
            case EquipmentType.Trinket:
                this.equipmentLists[3].AnimatePlateImage(1.1f);
                break;
        }
    }

    private void AnimateBeginUseableSlot()
    {
        foreach (InventorySlotUseable useableSlot in useableItemLists)
        {
            useableSlot.AnimatePlateImage(1.1f);
        }
    }

    /// <summary>
    /// �κ��丮 ������ �巡�� ���۽� => �ش� ���� �ִϸ��̼�
    /// </summary>
    /// <param name="invenItem">�巡�� ������</param>
    public void ItemOnEndDrag(InventoryItem invenItem)
    {
        if (invenItem.GetItemType() == ItemType.Equipment)
        {
            AnimateEndEquipmentSlot(invenItem.GetEquipmentType());
        }
        else if (invenItem.GetItemType() == ItemType.Useable)
        {
            AnimateEndUseableSlot();
        }
    }

    private void AnimateEndEquipmentSlot(EquipmentType equipmentType)
    {
        switch (equipmentType)
        {
            case EquipmentType.Helmet:
                this.equipmentLists[0].AnimatePlateImage(1f);
                break;
            case EquipmentType.Armor:
                this.equipmentLists[1].AnimatePlateImage(1f);
                break;
            case EquipmentType.Weapon:
                this.equipmentLists[2].AnimatePlateImage(1f);
                break;
            case EquipmentType.Trinket:
                this.equipmentLists[3].AnimatePlateImage(1f);
                break;
        }
    }

    private void AnimateEndUseableSlot()
    {
        foreach (InventorySlotUseable useableSlot in useableItemLists)
        {
            useableSlot.AnimatePlateImage(1f);
        }
    }
}
