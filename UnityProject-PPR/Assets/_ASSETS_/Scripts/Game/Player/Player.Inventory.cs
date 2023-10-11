using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player Inventory Partial Class
/// </summary>
public partial class Player : BehaviourSingleton<Player>
{
    [Header("Inventory")]
    [SerializeField] private GameObject relicPrefab;
    [SerializeField] private GameObject potionPrefab;
    [SerializeField] private List<InventoryItemRelic> relicList;
    [SerializeField] private List<InventoryItemPotion> potionList;

    #region Relic
    /// <summary>
    /// �κ��丮�� ���� �߰� �� ȿ�� �ο�
    /// </summary>
    public void AddItemRelic(ItemBlueprint blueprint)
    {
        var relic = Instantiate(this.relicPrefab, this.playerUI.GetRelicSlot()).GetComponent<InventoryItemRelic>();
        relic.Set(blueprint);
        this.SetRelic(this.relicList, relic);
    }
    #endregion

    #region Potion
    /// <summary>
    /// �κ��丮�� ���� �߰�
    /// </summary>
    public void AddItemPotion(ItemBlueprint blueprint)
    {
        var potion = Instantiate(this.potionPrefab, this.playerUI.GetPotionSlot()).GetComponent<InventoryItemPotion>();
        potion.Set(blueprint);
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

    public void SetPlayerPotions(BattlePlayerPotion[] playerPotions)
    {
        for (int i = 0; i < this.potionList.Count; i++)
        {
            playerPotions[i].Set(this.potionList[i]);
        }
    }
    #endregion
}
