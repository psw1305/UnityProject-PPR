using PSW.Core.Enums;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemLootTable : MonoBehaviour
{
    [SerializeField] private List<ItemList> itemLists = new();

    private float totalWeight;

    /// <summary>
    /// 아이템 등급별로 따로 랜덤 생성
    /// </summary>
    public ItemBlueprint GetRandomItemBlueprint(int id)
    {
        return id switch
        {
            1 => itemLists[0].GetItemBlueprint(),
            2 => itemLists[2].GetItemBlueprint(),
            3 => itemLists[2].GetItemBlueprint(),
            _ => GetAllRandomItemBlueprint(),
        };
    }

    /// <summary>
    /// 모든 아이템 리스트 중에서 랜덤 생성
    /// </summary>
    public ItemBlueprint GetAllRandomItemBlueprint()
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