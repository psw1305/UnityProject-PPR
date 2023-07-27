using PSW.Core.Enums;
using PSW.Core.Structs;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Blueprint/Card")]
public class ItemBlueprintCard : ItemBlueprint
{
    [SerializeField] private CardType cardType;
    [SerializeField] private StatModifierData[] stats;

    public CardType CardType => this.cardType;
    public int StatCount => this.stats.Length;
    public StatType ItemStatType(int num) => this.stats[num].type;
    public int ItemStatValue(int num) => this.stats[num].value;
}
