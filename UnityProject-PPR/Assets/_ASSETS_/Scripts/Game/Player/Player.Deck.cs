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
    /// ��ų ī�� ����
    /// </summary>
    /// <param name="skillCard"></param>
    public void EquipmentLoad(InventoryItem skillCard)
    {
        var equipmentData = skillCard.GetCardData();

        // ��� ���� => �������� ����
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

        // ��� ���� => �ο��� statCount��ŭ ���� ����
        PlayerAddStatModify(skillCard);
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
        this.playerUI.LoadCardDeck(num, invenItem);
    }

    private void PlayerAddStatModify(InventoryItem invenItem)
    {
        var equipmentData = invenItem.GetCardData();

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
            }
        }

        // ���� ��ȭ UI ǥ��
        this.playerUI.SetHealthUI();
    }

    /// <summary>
    /// ���(Equipment) ��ü
    /// </summary>
    /// <param name="invenItem">�κ��丮 ������</param>
    public void EquipmentUnload(InventoryItem invenItem)
    {
        var equipmentData = invenItem.GetCardData();

        // ��� ���� => �������� ����
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

        // ��� ���� => �ο��� statCount��ŭ ���� ����
        PlayerRemoveStatModify(invenItem);
    }

    private void PlayerRemoveStatModify(InventoryItem invenItem)
    {
        var equipmentData = invenItem.GetCardData();

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
            }
        }

        // ���� ��ȭ UI ǥ��
        this.playerUI.SetHealthUI();
    }

    private void Unequipping(int num, InventoryItem invenItem)
    {
        // �ش� ���â ���
        this.equipments[num] = null;
        // ���� false üũ
        invenItem.IsEquip = false;
        // ������ �̵�: ���â => �κ��丮
        this.playerUI.CardUnload(invenItem);
    }
}