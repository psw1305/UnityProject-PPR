using System.Collections.Generic;

/// <summary>
/// Player Inventory Partial Class
/// </summary>
public partial class Player : BehaviourSingleton<Player>
{
    public List<InventoryItemRelic> relicList;
    public List<InventoryItemPotion> potionList;

    #region Relic
    /// <summary>
    /// �κ��丮�� ���� �߰� �� ȿ�� �ο�
    /// </summary>
    public void AddRelic(int id)
    {
        var relic = GameManager.Instance.ItemLootRelic(id, this.playerUI.GetGridRelic());
        this.SetRelic(this.relicList, relic);
    }

    #endregion

    #region Potion
    /// <summary>
    /// �κ��丮�� ���� �߰�
    /// </summary>
    public void AddPotion()
    {
        var potion = GameManager.Instance.ItemLootPotion(this.playerUI.GetGridPotion());
        this.potionList.Add(potion);
    }

    /// <summary>
    /// �κ��丮�� ���� ������
    /// </summary>
    /// <param name="potion">���� ����</param>
    public void RemovePotion(InventoryItemPotion potion)
    {
        this.potionList.Remove(potion);
    }

    #endregion
}
