using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemLootTable : MonoBehaviour
{
    // [ID] 0. All Random / 1. Common / 2. Uncommon / 3. Rare / 4. Boss
    [SerializeField] private List<ItemList> itemLists = new();

    /// <summary>
    /// 아이템 등급별 또는 모든 중류 랜덤 생성
    /// </summary>
    public ItemBlueprint GetItemAdd(int rareTypeID)
    {
        return rareTypeID switch
        {
            1 => itemLists[0].GetRandomItem(),
            2 => itemLists[1].GetRandomItem(),
            3 => itemLists[2].GetRandomItem(),
            4 => itemLists[3].GetRandomItem(),
            _ => GetRandomItemToAllList(),
        };
    }

    /// <summary>
    /// 아이템 등급별로 랜덤 중복 생성
    /// </summary>
    public ItemBlueprint GetItemAdd_Duplicate(int rareTypeID)
    {
        return rareTypeID switch
        {
            1 => itemLists[0].GetRandomItem_Duplicate(),
            2 => itemLists[1].GetRandomItem_Duplicate(),
            3 => itemLists[2].GetRandomItem_Duplicate(),
            4 => itemLists[3].GetRandomItem_Duplicate(),
            _ => GetRandomItemToAllList_Duplicate(),
        };
    }

    /// <summary>
    /// 모든 아이템 리스트 중에서 갯수 만큼 중복없이 생성
    /// </summary>
    public ItemBlueprint[] GetItemTable(int amount)
    {
        var tempLists = this.itemLists.ToList();
        var totalWeight = tempLists.Sum(tempList => tempList.weight);

        var randomItemBlueprints = new ItemBlueprint[amount];

        for (int i = 0; i < amount; i++)
        {
            randomItemBlueprints[i] = GetWeightedItem(tempLists, totalWeight);
        }

        return randomItemBlueprints;
    }

    /// <summary>
    /// 아이템 리스트 중에서 가중치에 따른 랜덤 아이템 생성 후 해당 아이템 삭제
    /// </summary>
    public ItemBlueprint GetRandomItemToAllList()
    {
        var totalWeight = this.itemLists.Sum(itemList => itemList.weight);

        return GetWeightedItem(this.itemLists, totalWeight);
    }

    /// <summary>
    /// 아이템 리스트 중에서 가중치에 따른 랜덤 아이템 생성 (중복 가능)
    /// </summary>
    public ItemBlueprint GetRandomItemToAllList_Duplicate()
    {
        var totalWeight = this.itemLists.Sum(itemList => itemList.weight);

        return GetWeightedItem_Duplicate(this.itemLists, totalWeight);
    }

    /// <summary>
    /// 아이템 리스트에서 랜덤으로 아이템 생성 후 해당 아이템 삭제
    /// </summary>
    /// <param name="itemLists"></param>
    /// <param name="totalWeight"></param>
    public ItemBlueprint GetWeightedItem(List<ItemList> itemLists, float totalWeight)
    {
        var randomWeight = Random.Range(0f, totalWeight);

        foreach (var itemList in itemLists)
        {
            if (itemList.weight >= randomWeight)
            {
                return itemList.GetRandomItem();
            }

            randomWeight -= itemList.weight;
        }

        throw new System.Exception("Item Generate Failed");
    }

    /// <summary>
    /// 아이템 리스트에서 랜덤으로 아이템 생성 (중복 가능)
    /// </summary>
    /// <param name="itemLists"></param>
    /// <param name="totalWeight"></param>
    public ItemBlueprint GetWeightedItem_Duplicate(List<ItemList> itemLists, float totalWeight)
    {
        var randomWeight = Random.Range(0f, totalWeight);

        foreach (var itemList in itemLists)
        {
            if (itemList.weight >= randomWeight)
            {
                return itemList.GetRandomItem_Duplicate();
            }

            randomWeight -= itemList.weight;
        }

        throw new System.Exception("Item_Duplicate Generate Failed");
    }
}

#region Item List Class
[System.Serializable]
public class ItemList
{
    public string listName;
    public List<ItemBlueprint> itemList;
    public float weight;

    /// <summary>
    /// 리스트에서 랜덤으로 데이터 가져온 후 해당 데이터 삭제
    /// </summary>
    /// <returns></returns>
    public ItemBlueprint GetRandomItem()
    {
        var random = Random.Range(0, this.itemList.Count);
        var randomItem = this.itemList[random];
        this.itemList.Remove(randomItem);
        return randomItem;
    }

    /// <summary>
    /// 리스트에서 랜덤으로 아이템 데이터 가져오기 (중첩 가능)
    /// </summary>
    /// <returns></returns>
    public ItemBlueprint GetRandomItem_Duplicate()
    {
        var random = Random.Range(0, this.itemList.Count);
        var randomItem = this.itemList[random];
        return randomItem;
    }
}
#endregion