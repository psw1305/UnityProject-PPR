using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player Card Deck Partial Class
/// </summary>
public partial class Player : BehaviourSingleton<Player>
{
    [Header("Inventory")]
    public List<InventoryItemCard> cardSlots = new();
    public List<InventoryItemCard> cardDeck = new();

    public List<InventoryItemCard> GetCardDeck()
    {
        return this.cardDeck;
    }

    public void Test_SetDeck()
    {
        for (int i = 0; i < 8; i++) 
        {
            AddCard();
        }
    }

    public void AddCard()
    {
        var card = GameManager.Instance.ItemLootCard(this.playerUI.GetCardSlotList());
        this.cardSlots.Add(card);
    }

    /// <summary>
    /// 덱에 카드 장착
    /// </summary>
    public void EquipCardToDeck(InventoryItemCard card)
    {
        card.IsEquip = true;
        this.cardDeck.Add(card);
    }

    /// <summary>
    /// 덱에서 카드 제거
    /// </summary>
    public void RemoveCardFromDeck(InventoryItemCard card)
    {
        card.IsEquip = false;
        this.cardDeck.Remove(card);
    }

    //public void PotionItemLoad(InventoryItemPotion invenItem, int num)
    //{
    //    // 소모품칸 아이템 존재시 => 교체
    //    if (this.potionList[num] != null) PotionItemUnload(this.potionList[num], num);

    //    this.potionList[num] = invenItem;
    //    invenItem.IsEquip = true;
    //    //this.playerUI.LoadPotionBelt(num, invenItem);
    //}

    //public void PotionItemUnload(InventoryItemPotion invenItem, int num)
    //{
    //    this.potionList[num] = null;
    //    invenItem.IsEquip = false;
    //    //this.playerUI.CardUnload(invenItem);
    //}

    //public void PotionItemMove(InventoryItemPotion invenItem, int prefer, int current)
    //{
    //    PotionItemUnload(this.potionList[prefer], prefer);
    //    PotionItemLoad(invenItem, current);
    //}

    //public void CardChange(int origin, int change)
    //{
    //    // 포션 교체
    //    (this.potionList[origin], this.potionList[change]) = (this.potionList[change], this.potionList[origin]);
    //    //this.playerUI.LoadPotionBelt(origin, this.potionBelt[origin]);
    //    //this.playerUI.LoadPotionBelt(change, this.potionBelt[change]);
    //}
}