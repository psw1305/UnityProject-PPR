using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : BehaviourSingleton<InventorySystem>
{
    public List<InventoryItem> Inventory { get; private set; }
    private Dictionary<GameObject, InventoryItem> itemDictionary;

    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform inventoryTransform;

    private int invenMaxCount = 0;

    protected override void Awake()
    {
        base.Awake();

        this.itemDictionary = new Dictionary<GameObject, InventoryItem>();
        this.Inventory = new List<InventoryItem>();

        // �κ��丮 ������ ���� �ִ� ����
        this.invenMaxCount = 18;
    }

    /// <summary>
    /// Item Dictionary = {Key : Item ������Ʈ, value : InventoryItem Component}
    /// </summary>
    /// <param name="referenceData"></param>
    public void AddItem(ItemBlueprint referenceData)
    {
        if (this.Inventory.Count <= this.invenMaxCount)
        {
            InventoryItem itemClone = Instantiate(this.itemPrefab, this.inventoryTransform).GetComponent<InventoryItem>();
            itemClone.Set(referenceData);

            this.itemDictionary.Add(itemClone.gameObject, itemClone);
            this.Inventory.Add(itemClone);
        }
        else
        {
            // Notice => ��� �ִ� �� �ʰ� �˸�
        }
    }

    /// <summary>
    /// ������
    /// </summary>
    /// <param name="referenceData"></param>
    public void RemoveItem(GameObject itemObject)
    {
        if (this.itemDictionary.TryGetValue(itemObject, out InventoryItem value))
        {
            this.itemDictionary.Remove(itemObject);
        }
    }
}
