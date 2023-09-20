using PSW.Core.Enums;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BehaviourSingleton<GameManager>
{
    public EnemyEncounter EnemyEncounter { get; set; }
    public MysteryConfig MysteryConfig { get; set; }

    [Header("Camera")]
    [SerializeField] private Camera mainCamera;
    public Camera MainCamera => this.mainCamera;

    [Header("Item Table")]
    [SerializeField] private ItemLootTable cardLootTable;
    [SerializeField] private ItemLootTable relicLootTable;
    [SerializeField] private ItemLootTable potionLootTable;

    protected override void Awake()
    {
        base.Awake();

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = DataFrame.Instance.dFPS;
    }

    #region Camera
    public void CameraChange(Canvas canvas)
    {
        canvas.worldCamera = this.MainCamera;
    }

    public void CameraChange(Camera preCamra, Canvas canvas)
    {
        canvas.worldCamera = this.MainCamera;
        preCamra.enabled = false;
    }
    #endregion

    #region Item Loot
    public ItemBlueprint GetRandomCard(int itemGradeID)
    {
        return this.cardLootTable.GetItemAdd(itemGradeID);
    }

    public ItemBlueprint GetRandomRelic(int itemGradeID)
    {
        return this.relicLootTable.GetItemAdd(itemGradeID);
    }

    public ItemBlueprint GetRandomPotion(int itemGradeID)
    {
        return this.potionLootTable.GetItemAdd(itemGradeID);
    }
    #endregion

    #region Item Table
    public ItemBlueprint[] GetRandomItemTable(ItemType itemType, int amount)
    {
        switch (itemType)
        {
            case ItemType.Relic:
                return this.relicLootTable.GetItemTable(amount);
            case ItemType.Potion:
                return this.potionLootTable.GetItemTable(amount);
            case ItemType.Card:
                return this.cardLootTable.GetItemTable(amount);
        }

        throw new System.Exception("Item Table Generate Failed");
    }
    #endregion
}
