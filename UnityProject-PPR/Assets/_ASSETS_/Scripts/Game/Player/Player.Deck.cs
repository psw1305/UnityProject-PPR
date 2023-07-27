using PSW.Core.Enums;
using PSW.Core.Stat;
using UnityEngine;

/// <summary>
/// Player Card Deck Partial Class
/// </summary>
public partial class Player : BehaviourSingleton<Player>
{
    [Header("Cards Deck")]
    public InventoryItem[] cardDeck;

    public void SetDeck()
    {
        this.cardDeck = new InventoryItem[16];
    }

    /// <summary>
    /// 스킬 카드 장착
    /// </summary>
    /// <param name="skillCard"></param>
    public void EquipmentLoad(InventoryItem skillCard)
    {
        var equipmentData = skillCard.GetCardData();

        // 장비 세팅 => 유형별로 구분
        var equipmentType = equipmentData.CardType;

        switch (equipmentType)
        {
            case CardType.Attack:
                Equipping(0, skillCard);
                break;
            case CardType.Defense:
                Equipping(1, skillCard);
                break;
            case CardType.Special:
                Equipping(2, skillCard);
                break;
            case CardType.Joker:
                Equipping(3, skillCard);
                break;
        }

        // 장비 세팅 => 부여된 statCount만큼 스탯 조정
        PlayerAddStatModify(skillCard);
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
        this.playerUI.LoadCardDeck(num, invenItem);
    }

    private void PlayerAddStatModify(InventoryItem invenItem)
    {
        var equipmentData = invenItem.GetCardData();

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
            }
        }

        // 스탯 변화 UI 표시
        this.playerUI.SetHealthUI();
    }

    /// <summary>
    /// 장비(Equipment) 해체
    /// </summary>
    /// <param name="invenItem">인벤토리 아이템</param>
    public void EquipmentUnload(InventoryItem invenItem)
    {
        var equipmentData = invenItem.GetCardData();

        // 장비 세팅 => 유형별로 구분
        var equipmentType = equipmentData.CardType;

        switch (equipmentType)
        {
            case CardType.Attack:
                Unequipping(0, invenItem);
                break;
            case CardType.Defense:
                Unequipping(1, invenItem);
                break;
            case CardType.Special:
                Unequipping(2, invenItem);
                break;
            case CardType.Joker:
                Unequipping(3, invenItem);
                break;
        }

        // 장비 세팅 => 부여된 statCount만큼 스탯 조정
        PlayerRemoveStatModify(invenItem);
    }

    private void PlayerRemoveStatModify(InventoryItem invenItem)
    {
        var equipmentData = invenItem.GetCardData();

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
            }
        }

        // 스탯 변화 UI 표시
        this.playerUI.SetHealthUI();
    }

    private void Unequipping(int num, InventoryItem invenItem)
    {
        // 해당 장비창 비움
        this.equipments[num] = null;
        // 장착 false 체크
        invenItem.IsEquip = false;
        // 아이템 이동: 장비창 => 인벤토리
        this.playerUI.CardUnload(invenItem);
    }
}