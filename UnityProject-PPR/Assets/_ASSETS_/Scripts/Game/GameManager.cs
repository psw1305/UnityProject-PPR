using UnityEngine;

public class GameManager : BehaviourSingleton<GameManager>
{
    public EnemyEncounter EnemyEncounter { get; set; }

    [Header("Camera")]
    [SerializeField] private Camera mainCamera;
    public Camera MainCamera => this.mainCamera;

    [Header("Item Loot Tables")]
    [SerializeField] private InventoryItemRelic relicPrefab;
    [SerializeField] private InventoryItemPotion potionPrefab;
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
    public InventoryItemRelic ItemLootRelic(int id, Transform parent)
    {
        var blueprint = this.relicLootTable.GetRandomItemBlueprint(id);
        var relic = Instantiate(this.relicPrefab, parent);
        relic.Set(blueprint);

        return relic;
    }

    public InventoryItemPotion ItemLootPotion(Transform parent)
    {
        var blueprint = this.potionLootTable.GetRandomItemBlueprint(0);
        var potion = Instantiate(this.potionPrefab, parent);
        potion.Set(blueprint);

        return potion;
    }
    #endregion
}
