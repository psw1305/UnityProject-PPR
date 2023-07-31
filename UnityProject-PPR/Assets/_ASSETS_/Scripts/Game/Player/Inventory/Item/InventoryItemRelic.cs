
public class InventoryItemRelic : InventoryItem
{
    protected override void ItemTooltipShow()
    {
        base.ItemTooltipShow();

        PlayerItemTooltip.Instance.RelicTooltipShow(this);
    }
}
