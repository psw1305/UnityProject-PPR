using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player Card Deck Partial Class
/// </summary>
public partial class Player : BehaviourSingleton<Player>
{
    [Header("Deck")]
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private List<InventoryItemCard> cardDeck;
    [SerializeField] private List<ItemBlueprint> startCards;

    public List<InventoryItemCard> GetCardDeck()
    {
        return this.cardDeck;
    }

    private void StarterCardPack()
    {
        foreach (var startCard in this.startCards)
        {
            AddItemCard(startCard);
        }
    }

    /// <summary>
    /// ���� ī�� �߰�
    /// </summary>
    /// <param name="blueprint">������ ���赵</param>
    public void AddItemCard(ItemBlueprint blueprint)
    {
        var card = Instantiate(this.cardPrefab, this.playerUI.GetDeckSlot()).GetComponent<InventoryItemCard>();
        card.Set(blueprint);
        this.cardDeck.Add(card);
    }

    /// <summary>
    /// ���� ī�� ����
    /// </summary>
    public void InsertCardToDeck(InventoryItemCard card)
    {
        this.cardDeck.Add(card);
    }

    /// <summary>
    /// ������ ī�� ����
    /// </summary>
    public void RemoveCardFromDeck(InventoryItemCard card)
    {
        this.cardDeck.Remove(card);
    }
}