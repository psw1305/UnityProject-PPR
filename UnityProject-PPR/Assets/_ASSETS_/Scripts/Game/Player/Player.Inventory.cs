using System;
using System.Collections.Generic;

/// <summary>
/// Player Inventory Partial Class
/// </summary>
public partial class Player : BehaviourSingleton<Player>
{
    public List<InventoryItemRelic> relicAltar;
    public List<InventoryItemPotion> potionBelt;


    public void SetInventory()
    {
        // Empty
    }

    #region ADD ITEM
    /// <summary>
    /// �κ��丮�� ���� �߰�
    /// </summary>
    public void AddItemRelic(int id)
    {
        var relic = GameManager.Instance.ItemLootRelic(id, this.playerUI.GetGridRelic());
        this.relicAltar.Add(relic);
    }

    /// <summary>
    /// �κ��丮�� ���� �߰�
    /// </summary>
    public void AddItemPotion()
    {
        var potion = GameManager.Instance.ItemLootPotion(this.playerUI.GetGridPotion());
        this.potionBelt.Add(potion);
    }
    #endregion

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
