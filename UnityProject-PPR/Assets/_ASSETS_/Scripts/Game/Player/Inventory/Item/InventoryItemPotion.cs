
public class InventoryItemPotion : InventoryItem
{
    
    public override void Set(ItemBlueprint data)
    {

    }


    public ItemBlueprintPotion GetPotionData()
    {
        return (ItemBlueprintPotion)this.itemData;
    }
}
