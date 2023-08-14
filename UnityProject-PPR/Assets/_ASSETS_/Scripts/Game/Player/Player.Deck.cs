using PSW.Core.Enums;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player Card Deck Partial Class
/// </summary>
public partial class Player : BehaviourSingleton<Player>
{
    [Header("Inventory")]
    public List<InventoryItemCard> cardDeck;

    public void Test_SetDeck()
    {
        for (int i = 0; i < 6; i++) 
        {
            AddCard();
        }
    }

    public void AddCard()
    {
        var card = GameManager.Instance.ItemLootCard(this.playerUI.GetCardSlotList());
        this.cardDeck.Add(card);
    }

    /// <summary>
    /// 스킬 카드 장착
    /// </summary>
    /// <param name="skillCard"></param>
    public void EquipSkillCard(InventoryItemCard skillCard)
    {
        var cardData = skillCard.GetCardData();
        var cardType = cardData.CardType;

        switch (cardType)
        {
            case CardType.Attack:
                Equip(0, skillCard);
                break;
            case CardType.Defense:
                Equip(1, skillCard);
                break;
            case CardType.Synergy:
                Equip(2, skillCard);
                break;
            case CardType.Obstacle:
                Equip(3, skillCard);
                break;
        }
    }

    private void Equip(int num, InventoryItemCard invenItem)
    {
        // 장비칸 아이템 존재시 => 교체
        if (this.cardDeck[num] != null) UnequipSkillCard(this.cardDeck[num]);

        // 해당 장비창 채움
        this.cardDeck[num] = invenItem;
        // 장착 true 체크
        invenItem.IsEquip = true;
        // 아이템 이동: 인벤토리 => 장비창
        this.playerUI.LoadCardDeck(num, invenItem);
    }

    /// <summary>
    /// 장비(Equipment) 해체
    /// </summary>
    /// <param name="invenItem">인벤토리 아이템</param>
    public void UnequipSkillCard(InventoryItemCard invenItem)
    {
        var cardData = invenItem.GetCardData();
        var cardType = cardData.CardType;

        switch (cardType)
        {
            case CardType.Attack:
                Unequip(0, invenItem);
                break;
            case CardType.Defense:
                Unequip(1, invenItem);
                break;
            case CardType.Synergy:
                Unequip(2, invenItem);
                break;
            case CardType.Obstacle:
                Unequip(3, invenItem);
                break;
        }
    }

    private void Unequip(int num, InventoryItemCard invenItem)
    {
        // 해당 장비창 비움
        this.cardDeck[num] = null;
        // 장착 false 체크
        invenItem.IsEquip = false;
        // 아이템 이동: 장비창 => 인벤토리
        //this.playerUI.CardUnload(invenItem);
    }

    public void PotionItemLoad(InventoryItemPotion invenItem, int num)
    {
        // 소모품칸 아이템 존재시 => 교체
        if (this.potionList[num] != null) PotionItemUnload(this.potionList[num], num);

        this.potionList[num] = invenItem;
        invenItem.IsEquip = true;
        //this.playerUI.LoadPotionBelt(num, invenItem);
    }

    public void PotionItemUnload(InventoryItemPotion invenItem, int num)
    {
        this.potionList[num] = null;
        invenItem.IsEquip = false;
        //this.playerUI.CardUnload(invenItem);
    }

    public void PotionItemMove(InventoryItemPotion invenItem, int prefer, int current)
    {
        PotionItemUnload(this.potionList[prefer], prefer);
        PotionItemLoad(invenItem, current);
    }

    public void PotionItemChange(int origin, int change)
    {
        // 포션 교체
        (this.potionList[origin], this.potionList[change]) = (this.potionList[change], this.potionList[origin]);
        //this.playerUI.LoadPotionBelt(origin, this.potionBelt[origin]);
        //this.playerUI.LoadPotionBelt(change, this.potionBelt[change]);
    }
}