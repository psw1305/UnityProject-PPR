using UnityEngine;
using UnityEngine.UI;

public class InventoryItemCard : InventoryItem
{
    [Header("Item - Card")]
    [SerializeField] private Image cardFrame;

    public override void Set(ItemBlueprint blueprint)
    {
        base.Set(blueprint);

        var cardBlueprint = (ItemBlueprintCard)blueprint;
        this.cardFrame.sprite = cardBlueprint.CardFrame;
    }

    public ItemBlueprintCard GetCardData()
    {
        return (ItemBlueprintCard)this.blueprint;
    }

    protected override void ItemTooltipShow()
    {
        PlayerItemTooltip.Instance.CardTooltipShow(this.GetItemData());
    }
}
