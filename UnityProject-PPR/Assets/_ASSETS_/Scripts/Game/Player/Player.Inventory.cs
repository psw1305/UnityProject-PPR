using UnityEngine;

/// <summary>
/// Player Inventory Partial Class
/// </summary>
public partial class Player : BehaviourSingleton<Player>
{
    [Header("Inventory")]
    // ��� ����Ʈ => [0.����][1.����][2.����][3.�׼�����]
    public InventoryItem[] equipments;
    // ���� ��Ʈ => �⺻ 3��, �ִ� 5��
    public InventoryItem[] potionBelt;

    public void SetInventory()
    {
        this.equipments = new InventoryItem[4];
        this.potionBelt = new InventoryItem[5];
    }

    public void PotionItemLoad(InventoryItem invenItem, int num)
    {
        // �Ҹ�ǰĭ ������ ����� => ��ü
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
        // ���� ��ü
        (this.potionBelt[origin], this.potionBelt[change]) = (this.potionBelt[change], this.potionBelt[origin]);
        this.playerUI.LoadPotionBelt(origin, this.potionBelt[origin]);
        this.playerUI.LoadPotionBelt(change, this.potionBelt[change]);
    }
}
