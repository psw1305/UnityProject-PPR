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

    public InventoryItemRelic ItemLootRelic(int id, Transform parent)
    {
        var blueprint = this.relicLootTable.GetRandomItemBlueprint(id);
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
}
