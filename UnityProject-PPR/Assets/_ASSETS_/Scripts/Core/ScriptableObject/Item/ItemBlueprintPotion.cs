using PSW.Core.Structs;
using UnityEngine;

[CreateAssetMenu(fileName = "Useable", menuName = "Blueprint/Useable")]
public class ItemBlueprintPotion : ItemBlueprint
{
    [Header("Potion")]
    [SerializeField] private AbilityData ability;

    public AbilityData Ability => this.ability;
    public ItemBlueprintCard ChangeCard => this.ability.card;
}