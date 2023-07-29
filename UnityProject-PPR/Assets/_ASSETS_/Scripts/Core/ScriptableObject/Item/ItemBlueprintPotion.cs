using PSW.Core.Structs;
using UnityEngine;

[CreateAssetMenu(fileName = "Useable", menuName = "Blueprint/Useable")]
public class ItemBlueprintPotion : ItemBlueprint
{
    [SerializeField] private AbilityData ability;

    public AbilityData Ability => this.ability;
    public ElementBlueprint ChangeElement => this.ability.element;
}