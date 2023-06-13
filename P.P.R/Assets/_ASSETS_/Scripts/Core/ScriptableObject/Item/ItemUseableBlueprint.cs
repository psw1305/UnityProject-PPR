using PSW.Core.Structs;
using UnityEngine;

[CreateAssetMenu(fileName = "Useable", menuName = "Blueprint/Useable")]
public class ItemUseableBlueprint : ItemBlueprint
{
    [SerializeField] private AbilityData ability;
    [SerializeField] private string abilityDesc;

    public AbilityData Ability => this.ability;
    public string AbilityDesc => this.abilityDesc;
    public ElementBlueprint ChangeElement => this.ability.element;
}