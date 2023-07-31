/// <summary>
/// Player Inventory Partial Class
/// </summary>
public partial class Player : BehaviourSingleton<Player>
{
    public InventoryItemRelic[] relicAltar;
    public InventoryItemPotion[] potionBelt;

    public void SetInventory()
    {
        for (int i = 0; i < 5; i++)
        {
            this.relicAltar[i] = GameManager.Instance.ItemLootRelic(this.playerUI.GetGridRelic());
        }

        for (int i = 0; i < 3; i++)
        {
            this.potionBelt[i] = GameManager.Instance.ItemLootPotion(this.playerUI.GetGridPotion());
        }
    }

    public void PotionItemLoad(InventoryItemPotion invenItem, int num)
    {
        // �Ҹ�ǰĭ ������ ����� => ��ü
        if (this.potionBelt[num] != null) PotionItemUnload(this.potionBelt[num], num);

        this.potionBelt[num] = invenItem;
        invenItem.IsEquip = true;
        //this.playerUI.LoadPotionBelt(num, invenItem);
    }

    public void PotionItemUnload(InventoryItemPotion invenItem, int num)
    {
        this.potionBelt[num] = null;
        invenItem.IsEquip = false;
        //this.playerUI.CardUnload(invenItem);
    }

    public void PotionItemMove(InventoryItemPotion invenItem, int prefer, int current)
    {
        PotionItemUnload(this.potionBelt[prefer], prefer);
        PotionItemLoad(invenItem, current);
    }

    public void PotionItemChange(int origin, int change)
    {
        // ���� ��ü
        (this.potionBelt[origin], this.potionBelt[change]) = (this.potionBelt[change], this.potionBelt[origin]);
        //this.playerUI.LoadPotionBelt(origin, this.potionBelt[origin]);
        //this.playerUI.LoadPotionBelt(change, this.potionBelt[change]);
    }
}
