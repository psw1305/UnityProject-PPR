using UnityEngine;

public class GameManager : BehaviourSingleton<GameManager>
{
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
    public InventoryItemRelic ItemLootRelic(Transform parent)
    {
        var blueprint = this.relicLootTable.GetRandomItemBlueprint();
        var relic = Instantiate(this.relicPrefab, parent);
        relic.Set(blueprint);

        return relic;
    }

    public InventoryItemPotion ItemLootPotion(Transform parent)
    {
        var blueprint = this.potionLootTable.GetRandomItemBlueprint();
        var potion = Instantiate(this.potionPrefab, parent);
        potion.Set(blueprint);

        return potion;
    }
    #endregion
}
