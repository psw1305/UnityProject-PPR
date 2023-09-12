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

    [Header("Item - Card")]
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private ItemLootTable cardLootTable;

    [Header("Item - Relic")]
    [SerializeField] private GameObject relicPrefab;
    [SerializeField] private ItemLootTable relicLootTable;

    [Header("Item - Potion")]
    [SerializeField] private GameObject potionPrefab;
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
    public InventoryItemCard ItemLootCard(Transform parent)
    {
        var blueprint = this.cardLootTable.GetRandomItemBlueprint(0);
        var card = Instantiate(this.cardPrefab, parent).GetComponent<InventoryItemCard>();
        card.Set(blueprint);

        return card;
    }

    /// <summary>
    /// 유물 아이템 루팅 [중첩 불가]
    /// </summary>
    /// <param name="rareTypeID"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public InventoryItemRelic ItemLootRelic(int rareTypeID, Transform parent)
    {
        var blueprint = this.relicLootTable.GetRandomItemBlueprintAndRemove(rareTypeID);
        var relic = Instantiate(this.relicPrefab, parent).GetComponent<InventoryItemRelic>();
        relic.Set(blueprint);

        return relic;
    }

    public InventoryItemPotion ItemLootPotion(Transform parent)
    {
        var blueprint = this.potionLootTable.GetRandomItemBlueprint(0);
        var potion = Instantiate(this.potionPrefab, parent).GetComponent<InventoryItemPotion>();
        potion.Set(blueprint);

        return potion;
    }
    #endregion

    #region RandomBlueprint
    public ItemBlueprint[] GetRandomProducts(ItemType itemType, int amount)
    {
        switch (itemType)
        {
            case ItemType.Relic:
                return this.relicLootTable.GetRandomItemTableNoDuplicate(amount);
            case ItemType.Potion:
                return this.potionLootTable.GetRandomItemTableNoDuplicate(amount);
            case ItemType.Card:
                return this.cardLootTable.GetRandomItemTableNoDuplicate(amount);
        }

        throw new System.Exception("Products Table Generate Failed");
    }
    #endregion
}
