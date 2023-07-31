using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemLootTable : MonoBehaviour
{
    [SerializeField] private List<ItemList> itemLists = new();

    private float totalWeight;

    public ItemBlueprint GetRandomItemBlueprint()
    {
        this.totalWeight = itemLists.Sum(itemList => itemList.weight);

        var random = Random.Range(0f, this.totalWeight);

        foreach (var item in this.itemLists) 
        {
            if (item.weight >= random)
            {                
                return item.GetItemBlueprint();
            }

            random -= item.weight;
        }

        throw new System.Exception("Item Generate Failed");
    }
}

#region Item List Class
[System.Serializable]
public class ItemList
{
    public string listName;
    public List<ItemBlueprint> itemList = new();
    public float weight;

    public ItemBlueprint GetItemBlueprint()
    {
        var random = Random.Range(0, this.itemList.Count);
        var randomItem = this.itemList[random];
        this.itemList.Remove(randomItem);

        return randomItem;
    }
}
#endregion