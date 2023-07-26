using UnityEngine;

/// <summary>
/// Player Inventory Partial Class
/// </summary>
public partial class Player : BehaviourSingleton<Player>
{
    [Header("Inventory")]
    // 장비 리스트 => [0.투구][1.갑옷][2.무기][3.액세서리]
    public InventoryItem[] equipments;
    // 포션 벨트 => 기본 3개, 최대 5개
    public InventoryItem[] potionBelt;

    public void SetInventory()
    {
        this.equipments = new InventoryItem[4];
        this.potionBelt = new InventoryItem[5];
    }

    public void PotionItemLoad(InventoryItem invenItem, int num)
    {
        // 소모품칸 아이템 존재시 => 교체
        if (this.potionBelt[num] != null) PotionItemUnload(this.potionBelt[num], num);

        this.potionBelt[num] = invenItem;
        invenItem.IsEquip = true;
        this.playerUI.LoadPotionBelt(num, invenItem);
    }

    public void PotionItemUnload(InventoryItem invenItem, int num)
    {
        this.potionBelt[num] = null;
        invenItem.IsEquip = false;
        this.playerUI.CardUnload(invenItem);
    }

    public void PotionItemMove(InventoryItem invenItem, int prefer, int current)
    {
        PotionItemUnload(this.potionBelt[prefer], prefer);
        PotionItemLoad(invenItem, current);
    }

    public void PotionItemChange(int origin, int change)
    {
        // 포션 교체
        (this.potionBelt[origin], this.potionBelt[change]) = (this.potionBelt[change], this.potionBelt[origin]);
        this.playerUI.LoadPotionBelt(origin, this.potionBelt[origin]);
        this.playerUI.LoadPotionBelt(change, this.potionBelt[change]);
    }
}
