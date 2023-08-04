using PSW.Core.Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "Relic", menuName = "Blueprint/Relic")]
public class ItemBlueprintRelic : ItemBlueprint
{
    [Header("Relic")]
    [SerializeField] private string relicID;
    [SerializeField] private RelicType relicType;

    public string RelicID => this.relicID;
    public RelicType RelicType => this.relicType;
}
