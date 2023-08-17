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
    /// ���� ī�� ����
    /// </summary>
    public void EquipCardToDeck(InventoryItemCard card)
    {
        card.IsEquip = true;
        this.cardDeck.Add(card);
    }

    /// <summary>
    /// ������ ī�� ����
    /// </summary>
    public void RemoveCardFromDeck(InventoryItemCard card)
    {
        card.IsEquip = false;
        this.cardDeck.Remove(card);
    }

    //public void PotionItemLoad(InventoryItemPotion invenItem, int num)
    //{
    //    // �Ҹ�ǰĭ ������ ����� => ��ü
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
    //    // ���� ��ü
    //    (this.potionList[origin], this.potionList[change]) = (this.potionList[change], this.potionList[origin]);
    //    //this.playerUI.LoadPotionBelt(origin, this.potionBelt[origin]);
    //    //this.playerUI.LoadPotionBelt(change, this.potionBelt[change]);
    //}
}