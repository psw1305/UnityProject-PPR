using UnityEngine;

[CreateAssetMenu(fileName = "Relic", menuName = "Blueprint/Relic")]
public class ItemBlueprintRelic : ItemBlueprint
{
    [SerializeField] private string relicAbility;
    [SerializeField] private string relicDesc;
}
