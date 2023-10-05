using PSW.Core.Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Blueprint/Card")]
public class ItemBlueprintCard : ItemBlueprint
{
    [Header("Card - Type")]
    [SerializeField] private CardType cardType;
    [SerializeField] private CardDetailType cardDetailType;

    [Header("Card - Param")]
    [SerializeField] private string cardName;
    [SerializeField] private float cardWeighted;

    [Header("Card - UI")]
    [SerializeField] private Sprite cardFrame;
    [SerializeField] private Color cardColor;

    public CardType CardType => this.cardType;
    public CardDetailType CardDetailType => this.cardDetailType;
    
    public Sprite CardFrame => this.cardFrame;
    public Color CardColor => this.cardColor;

    public string CardName => this.cardName;
    public float CardWeighted => this.cardWeighted;
}
