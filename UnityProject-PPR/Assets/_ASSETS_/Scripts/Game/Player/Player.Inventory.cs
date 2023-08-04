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
    /// 인벤토리에 유물 추가 및 효과 부여
    /// </summary>
    public void AddRelic(int id)
    {
        var relic = GameManager.Instance.ItemLootRelic(id, this.playerUI.GetGridRelic());
        this.SetRelic(this.relicList, relic);
    }

    #endregion

    #region Potion
    /// <summary>
    /// 인벤토리에 포션 추가
    /// </summary>
    public void AddPotion()
    {
        var potion = GameManager.Instance.ItemLootPotion(this.playerUI.GetGridPotion());
        this.potionList.Add(potion);
    }

    /// <summary>
    /// 인벤토리에 포션 버리기
    /// </summary>
    /// <param name="potion">버릴 포션</param>
    public void RemovePotion(InventoryItemPotion potion)
    {
        this.potionList.Remove(potion);
    }

    #endregion
}
