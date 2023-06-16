using PSW.Core.Enums;
using PSW.Core.Stat;
using System.Collections.Generic;
using UnityEngine;

public class Player : BehaviourSingleton<Player>
{
    [Header("Stat")]
    // Player Value => 장비에 영향 O
    public Stat HP;
    public Stat ACT;
    public Stat ATK;
    public Stat DEF;

    [Header("Inventory")]
    // 장비 리스트 => [0.투구][1.갑옷][2.무기][3.액세서리]
    public InventoryItem[] equipments;
    // 소모품 리스트 => 5개 한정
    public InventoryItem[] useableItems;
    // 인벤토리 리스트
    public List<ItemBlueprint> inventoryItems = new();

    public static int Cash { get; set; }
    public static int CurrentHP { get; set; }
    public static EnemyBlueprint BattleEnemy { get; set; }

    protected override void Awake()
    {
        base.Awake();

        SetStat();
        PlayerUI.Instance.SetUI();
    }

    private void Start()
    {
        this.equipments = new InventoryItem[4];
        this.useableItems = new InventoryItem[5];

        SetInventory();
    }

    public void SetStat(int hp = 40, int act = 12, int atk = 1, int def = 1, int cash = 0)
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
        PlayerUI.Instance.SetHealthUI(GetHpText());
    }

    public string GetHpText()
    {
        return CurrentHP + "/" + this.HP.Value;
    }

    public void SetInventory()
    {
        foreach(ItemBlueprint item in this.inventoryItems)
        {
            InventorySystem.Instance.AddItem(item);
        }
    }

    /// <summary>
    /// 장비(Equipment) 장착
    /// </summary>
    /// <param name="invenItem">인벤토리 아이템</param>
    public void EquipmentLoad(InventoryItem invenItem)
    {
        var equipmentData = invenItem.GetEquipmentData();

        // 장비 세팅 => 유형별로 구분
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

        // 장비 세팅 => 부여된 statCount만큼 스탯 조정
        PlayerAddStatModify(invenItem);
    }

    private void Equipping(int num, InventoryItem invenItem)
    {
        // 장비칸 아이템 존재시 => 교체
        if (this.equipments[num] != null) EquipmentUnload(this.equipments[num]);

        // 해당 장비창 채움
        this.equipments[num] = invenItem;
        // 장착 true 체크
        invenItem.IsEquip = true;
        // 아이템 이동: 인벤토리 => 장비창
        PlayerUI.Instance.SetUIEquipmentLoad(num, invenItem);
    }

    private void PlayerAddStatModify(InventoryItem invenItem)
    {
        var equipmentData = invenItem.GetEquipmentData();

        // 장비 세팅 => 부여된 statCount만큼 스탯 조정
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

        // 스탯 변화 UI 표시
        PlayerUI.Instance.SetStatUI();
    }

    /// <summary>
    /// 장비(Equipment) 해체
    /// </summary>
    /// <param name="invenItem">인벤토리 아이템</param>
    public void EquipmentUnload(InventoryItem invenItem)
    {
        var equipmentData = invenItem.GetEquipmentData();

        // 장비 세팅 => 유형별로 구분
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

        // 장비 세팅 => 부여된 statCount만큼 스탯 조정
        PlayerRemoveStatModify(invenItem);
    }

    private void PlayerRemoveStatModify(InventoryItem invenItem)
    {
        var equipmentData = invenItem.GetEquipmentData();

        // 장비 세팅 => 부여된 statCount만큼 스탯 조정
        for (int i = 0; i < equipmentData.StatCount; i++)
        {
            var statType = equipmentData.ItemStatType(i);

            switch (statType)
            {
                case StatType.HP:
                    this.HP.RemoveAllModifiersFromSource(invenItem);
                    // 장비 해체 시 현 체력이 최대체력보다 높을 경우 조정
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

        // 스탯 변화 UI 표시
        PlayerUI.Instance.SetStatUI();
    }

    private void Unequipping(int num, InventoryItem invenItem)
    {
        // 해당 장비창 비움
        this.equipments[num] = null;
        // 장착 false 체크
        invenItem.IsEquip = false;
        // 아이템 이동: 장비창 => 인벤토리
        PlayerUI.Instance.SetUIItemUnload(invenItem);
    }

    public void UseableItemLoad(InventoryItem invenItem, int num)
    {
        // 소모품칸 아이템 존재시 => 교체
        if (this.useableItems[num] != null) UseableItemUnload(this.useableItems[num], num);

        this.useableItems[num] = invenItem;
        invenItem.IsEquip = true;
        PlayerUI.Instance.SetUIUseableItemLoad(num, invenItem);
    }

    public void UseableItemUnload(InventoryItem invenItem, int num)
    {
        this.useableItems[num] = null;
        invenItem.IsEquip = false;
        PlayerUI.Instance.SetUIItemUnload(invenItem);
    }

    public void UseableItemMove(InventoryItem invenItem, int prefer, int current)
    {
        UseableItemUnload(this.useableItems[prefer], prefer);
        UseableItemLoad(invenItem, current);
    }

    public void UseableItemChange(int origin, int change)
    {
        // 소모품 교체
        (this.useableItems[origin], this.useableItems[change]) = (this.useableItems[change], this.useableItems[origin]);
        PlayerUI.Instance.SetUIUseableItemLoad(origin, this.useableItems[origin]);
        PlayerUI.Instance.SetUIUseableItemLoad(change, this.useableItems[change]);
    }
}
