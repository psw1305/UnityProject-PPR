
public class InventoryItemPotion : InventoryItem
{
    protected override void ItemTooltipShow()
    {
        PlayerItemTooltip.Instance.PotionTooltipShow(this.GetBlueprint());
    }
}
