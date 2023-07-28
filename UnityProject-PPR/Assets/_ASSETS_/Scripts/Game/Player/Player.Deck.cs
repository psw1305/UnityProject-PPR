using PSW.Core.Enums;
using PSW.Core.Stat;

/// <summary>
/// Player Card Deck Partial Class
/// </summary>
public partial class Player : BehaviourSingleton<Player>
{
    public InventoryItemCard[] cardDeck;

    public void SetDeck()
    {
        this.cardDeck = new InventoryItemCard[16];
    }

    /// <summary>
    /// 스킬 카드 장착
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

        // 장비 세팅 => 부여된 statCount만큼 스탯 조정
        PlayerAddStatModify(skillCard);
    }

    private void Equipping(int num, InventoryItemCard invenItem)
    {
        // 장비칸 아이템 존재시 => 교체
        if (this.cardDeck[num] != null) EquipmentUnload(this.cardDeck[num]);

        // 해당 장비창 채움
        this.cardDeck[num] = invenItem;
        // 장착 true 체크
        invenItem.IsEquip = true;
        // 아이템 이동: 인벤토리 => 장비창
        this.playerUI.LoadCardDeck(num, invenItem);
    }

    private void PlayerAddStatModify(InventoryItemCard invenItem)
    {
        var cardData = invenItem.GetCardData();

        // 장비 세팅 => 부여된 statCount만큼 스탯 조정
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

        // 스탯 변화 UI 표시
        this.playerUI.SetHPText();
    }

    /// <summary>
    /// 장비(Equipment) 해체
    /// </summary>
    /// <param name="invenItem">인벤토리 아이템</param>
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

        // 장비 세팅 => 부여된 statCount만큼 스탯 조정
        PlayerRemoveStatModify(invenItem);
    }

    private void PlayerRemoveStatModify(InventoryItemCard invenItem)
    {
        var cardData = invenItem.GetCardData();

        // 장비 세팅 => 부여된 statCount만큼 스탯 조정
        for (int i = 0; i < cardData.StatCount; i++)
        {
            var statType = cardData.ItemStatType(i);

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
        this.playerUI.SetHPText();
    }

    private void Unequipping(int num, InventoryItemCard invenItem)
    {
        // 해당 장비창 비움
        this.cardDeck[num] = null;
        // 장착 false 체크
        invenItem.IsEquip = false;
        // 아이템 이동: 장비창 => 인벤토리
        //this.playerUI.CardUnload(invenItem);
    }
}