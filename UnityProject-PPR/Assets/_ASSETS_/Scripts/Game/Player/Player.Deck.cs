using PSW.Core.Enums;
using PSW.Core.Stat;
using UnityEngine;

/// <summary>
/// Player Card Deck Partial Class
/// </summary>
public partial class Player : BehaviourSingleton<Player>
{
    [Header("Inventory")]
    public InventoryItemCard[] cardDeck;

    public void SetDeck()
    {
        this.cardDeck = new InventoryItemCard[16];
    }

    /// <summary>
    /// ��ų ī�� ����
    /// </summary>
    /// <param name="skillCard"></param>
    public void EquipmentLoad(InventoryItemCard skillCard)
    {
        var cardData = skillCard.GetCardData();
        var cardType = cardData.CardType;

        switch (cardType)
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

        // ��� ���� => �ο��� statCount��ŭ ���� ����
        PlayerAddStatModify(skillCard);
    }

    private void Equipping(int num, InventoryItemCard invenItem)
    {
        // ���ĭ ������ ����� => ��ü
        if (this.cardDeck[num] != null) EquipmentUnload(this.cardDeck[num]);

        // �ش� ���â ä��
        this.cardDeck[num] = invenItem;
        // ���� true üũ
        invenItem.IsEquip = true;
        // ������ �̵�: �κ��丮 => ���â
        this.playerUI.LoadCardDeck(num, invenItem);
    }

    private void PlayerAddStatModify(InventoryItemCard invenItem)
    {
        var cardData = invenItem.GetCardData();

        // ��� ���� => �ο��� statCount��ŭ ���� ����
        for (int i = 0; i < cardData.StatCount; i++)
        {
            StatType statType = cardData.ItemStatType(i);
            int statValue = cardData.ItemStatValue(i);

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

        // ���� ��ȭ UI ǥ��
        this.playerUI.SetHPText();
    }

    /// <summary>
    /// ���(Equipment) ��ü
    /// </summary>
    /// <param name="invenItem">�κ��丮 ������</param>
    public void EquipmentUnload(InventoryItemCard invenItem)
    {
        var cardData = invenItem.GetCardData();
        var cardType = cardData.CardType;

        switch (cardType)
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

        // ��� ���� => �ο��� statCount��ŭ ���� ����
        PlayerRemoveStatModify(invenItem);
    }

    private void PlayerRemoveStatModify(InventoryItemCard invenItem)
    {
        var cardData = invenItem.GetCardData();

        // ��� ���� => �ο��� statCount��ŭ ���� ����
        for (int i = 0; i < cardData.StatCount; i++)
        {
            var statType = cardData.ItemStatType(i);

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
            }
        }

        // ���� ��ȭ UI ǥ��
        this.playerUI.SetHPText();
    }

    private void Unequipping(int num, InventoryItemCard invenItem)
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