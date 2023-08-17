using PSW.Core.Enums;

public class InventoryItemRelic : InventoryItem
{
    public string RelicID { get; private set; }
    public RelicType RelicType { get; private set; }

    public override void Set(ItemBlueprint blueprint)
    {
        base.Set(blueprint);

        var relicBlueprint = (ItemBlueprintRelic)blueprint;
        this.RelicID = relicBlueprint.RelicID;
        this.RelicType = relicBlueprint.RelicType;
    }

    protected override void ItemTooltipShow()
    {
        base.ItemTooltipShow();

        PlayerItemTooltip.Instance.RelicTooltipShow(this);
    }
}
