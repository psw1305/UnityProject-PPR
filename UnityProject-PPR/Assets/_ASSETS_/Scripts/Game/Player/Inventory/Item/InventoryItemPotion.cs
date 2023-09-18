
public class InventoryItemPotion : InventoryItem
{
    public ItemBlueprintPotion GetPotionData()
    {
        return (ItemBlueprintPotion)this.blueprint;
    }

    protected override void ItemTooltipShow()
    {
        base.ItemTooltipShow();

        PlayerItemTooltip.Instance.PotionTooltipShow(this.GetItemData());
    }
}
