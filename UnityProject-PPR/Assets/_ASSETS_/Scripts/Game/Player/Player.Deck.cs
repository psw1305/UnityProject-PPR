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
    /// ��ų ī�� ����
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
        // ���ĭ ������ ����� => ��ü
        if (this.cardDeck[num] != null) UnequipSkillCard(this.cardDeck[num]);

        // �ش� ���â ä��
        this.cardDeck[num] = invenItem;
        // ���� true üũ
        invenItem.IsEquip = true;
        // ������ �̵�: �κ��丮 => ���â
        this.playerUI.LoadCardDeck(num, invenItem);
    }

    /// <summary>
    /// ���(Equipment) ��ü
    /// </summary>
    /// <param name="invenItem">�κ��丮 ������</param>
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
        // �ش� ���â ���
        this.cardDeck[num] = null;
        // ���� false üũ
        invenItem.IsEquip = false;
        // ������ �̵�: ���â => �κ��丮
        //this.playerUI.CardUnload(invenItem);
    }

    public void PotionItemLoad(InventoryItemPotion invenItem, int num)
    {
        // �Ҹ�ǰĭ ������ ����� => ��ü
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
        // ���� ��ü
        (this.potionList[origin], this.potionList[change]) = (this.potionList[change], this.potionList[origin]);
        //this.playerUI.LoadPotionBelt(origin, this.potionBelt[origin]);
        //this.playerUI.LoadPotionBelt(change, this.potionBelt[change]);
    }
}