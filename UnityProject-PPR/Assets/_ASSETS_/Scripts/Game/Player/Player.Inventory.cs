using PSW.Core.Enums;
using PSW.Core.Stat;
using UnityEngine;

/// <summary>
/// Player Inventory Partial Class
/// </summary>
public partial class Player : BehaviourSingleton<Player>
{
    [Header("Inventory")]
    // ��� ����Ʈ => [0.����][1.����][2.����][3.�׼�����]
    public InventoryItem[] equipments;
    // �Ҹ�ǰ ����Ʈ => 5�� ����
    public InventoryItem[] useableItems;

    private void Start()
    {
        SetInventory();

        this.playerUI.SetUI();
    }

    public void SetInventory()
    {
        this.equipments = new InventoryItem[4];
        this.useableItems = new InventoryItem[5];
    }

    /// <summary>
    /// ���(Equipment) ����
    /// </summary>
    /// <param name="invenItem">�κ��丮 ������</param>
    public void EquipmentLoad(InventoryItem invenItem)
    {
        var equipmentData = invenItem.GetEquipmentData();

        // ��� ���� => �������� ����
        var equipmentType = equipmentData.EquipmentType;

        switch (equipmentType)
        {
            case EquipmentType.Helmet:
                Equipping(0, invenItem);
                break;
            case EquipmentType.Armor:
                Equipping(1, invenItem);
                break;
            case EquipmentType.Weapon:
                Equipping(2, invenItem);
                break;
            case EquipmentType.Trinket:
                Equipping(3, invenItem);
                break;
        }

        // ��� ���� => �ο��� statCount��ŭ ���� ����
        PlayerAddStatModify(invenItem);
    }

    private void Equipping(int num, InventoryItem invenItem)
    {
        // ���ĭ ������ ����� => ��ü
        if (this.equipments[num] != null) EquipmentUnload(this.equipments[num]);

        // �ش� ���â ä��
        this.equipments[num] = invenItem;
        // ���� true üũ
        invenItem.IsEquip = true;
        // ������ �̵�: �κ��丮 => ���â
        this.playerUI.SetUIEquipmentLoad(num, invenItem);
    }

    private void PlayerAddStatModify(InventoryItem invenItem)
    {
        var equipmentData = invenItem.GetEquipmentData();

        // ��� ���� => �ο��� statCount��ŭ ���� ����
        for (int i = 0; i < equipmentData.StatCount; i++)
        {
            StatType statType = equipmentData.ItemStatType(i);
            int statValue = equipmentData.ItemStatValue(i);

            switch (statType)
            {
                case StatType.HP:
                    this.HP.AddModifier(new StatModifier(statValue, StatModType.Int, invenItem));
                    CurrentHP += statValue;
                    break;
                case StatType.ACT:
                    this.ACT.AddModifier(new StatModifier(statValue, StatModType.Int, invenItem));
                    break;
                case StatType.ATK:
                    this.ATK.AddModifier(new StatModifier(statValue, StatModType.Int, invenItem));
                    break;
                case StatType.DEF:
                    this.DEF.AddModifier(new StatModifier(statValue, StatModType.Int, invenItem));
                    break;
            }
        }

        // ���� ��ȭ UI ǥ��
        this.playerUI.SetStatUI();
    }

    /// <summary>
    /// ���(Equipment) ��ü
    /// </summary>
    /// <param name="invenItem">�κ��丮 ������</param>
    public void EquipmentUnload(InventoryItem invenItem)
    {
        var equipmentData = invenItem.GetEquipmentData();

        // ��� ���� => �������� ����
        var equipmentType = equipmentData.EquipmentType;

        switch (equipmentType)
        {
            case EquipmentType.Helmet:
                Unequipping(0, invenItem);
                break;
            case EquipmentType.Armor:
                Unequipping(1, invenItem);
                break;
            case EquipmentType.Weapon:
                Unequipping(2, invenItem);
                break;
            case EquipmentType.Trinket:
                Unequipping(3, invenItem);
                break;
        }

        // ��� ���� => �ο��� statCount��ŭ ���� ����
        PlayerRemoveStatModify(invenItem);
    }

    private void PlayerRemoveStatModify(InventoryItem invenItem)
    {
        var equipmentData = invenItem.GetEquipmentData();

        // ��� ���� => �ο��� statCount��ŭ ���� ����
        for (int i = 0; i < equipmentData.StatCount; i++)
        {
            var statType = equipmentData.ItemStatType(i);

            switch (statType)
            {
                case StatType.HP:
                    this.HP.RemoveAllModifiersFromSource(invenItem);
                    // ��� ��ü �� �� ü���� �ִ�ü�º��� ���� ��� ����
                    if (CurrentHP >= this.HP.Value) CurrentHP = this.HP.Value;
                    break;
                case StatType.ACT:
                    this.ACT.RemoveAllModifiersFromSource(invenItem);
                    break;
                case StatType.ATK:
                    this.ATK.RemoveAllModifiersFromSource(invenItem);
                    break;
                case StatType.DEF:
                    this.DEF.RemoveAllModifiersFromSource(invenItem);
                    break;
            }
        }

        // ���� ��ȭ UI ǥ��
        this.playerUI.SetStatUI();
    }

    private void Unequipping(int num, InventoryItem invenItem)
    {
        // �ش� ���â ���
        this.equipments[num] = null;
        // ���� false üũ
        invenItem.IsEquip = false;
        // ������ �̵�: ���â => �κ��丮
        this.playerUI.SetUIItemUnload(invenItem);
    }

    public void UseableItemLoad(InventoryItem invenItem, int num)
    {
        // �Ҹ�ǰĭ ������ ����� => ��ü
        if (this.useableItems[num] != null) UseableItemUnload(this.useableItems[num], num);

        this.useableItems[num] = invenItem;
        invenItem.IsEquip = true;
        this.playerUI.SetUIUseableItemLoad(num, invenItem);
    }

    public void UseableItemUnload(InventoryItem invenItem, int num)
    {
        this.useableItems[num] = null;
        invenItem.IsEquip = false;
        this.playerUI.SetUIItemUnload(invenItem);
    }

    public void UseableItemMove(InventoryItem invenItem, int prefer, int current)
    {
        UseableItemUnload(this.useableItems[prefer], prefer);
        UseableItemLoad(invenItem, current);
    }

    public void UseableItemChange(int origin, int change)
    {
        // �Ҹ�ǰ ��ü
        (this.useableItems[origin], this.useableItems[change]) = (this.useableItems[change], this.useableItems[origin]);
        this.playerUI.SetUIUseableItemLoad(origin, this.useableItems[origin]);
        this.playerUI.SetUIUseableItemLoad(change, this.useableItems[change]);
    }
}
