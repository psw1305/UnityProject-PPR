using PSW.Core.Enums;
using PSW.Core.Structs;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Blueprint/Equipment")]
public class ItemBlueprintArtifact : ItemBlueprint
{
    [SerializeField] private CardType equipmentType;
    [SerializeField] private StatModifierData[] stats;

    public CardType EquipmentType => this.equipmentType;
    public int StatCount => this.stats.Length;
    public StatType ItemStatType(int num) => this.stats[num].type;
    public int ItemStatValue(int num) => this.stats[num].value;
}
