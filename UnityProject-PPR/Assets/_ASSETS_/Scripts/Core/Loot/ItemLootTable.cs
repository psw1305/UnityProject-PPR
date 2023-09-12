using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemLootTable : MonoBehaviour
{
    [SerializeField] private List<ItemList> itemLists = new();
    /// <summary>
    /// 아이템 등급별로 따로 랜덤 생성
    /// [ID] 0. All Random / 1. Common / 2. Uncommon / 3. Rare / 4. Boss
    /// </summary>
    public ItemBlueprint GetRandomItemBlueprint(int rareTypeID)
    {
        return rareTypeID switch
        {
            1 => itemLists[0].GetRandomItemBlueprint(),
            2 => itemLists[1].GetRandomItemBlueprint(),
            3 => itemLists[2].GetRandomItemBlueprint(),
            4 => itemLists[3].GetRandomItemBlueprint(),
            _ => GetAllRandomItemBlueprint(),
        };
    }

    /// <summary>
    /// 모든 아이템 리스트 중에서 랜덤 생성
    /// </summary>
    public ItemBlueprint GetAllRandomItemBlueprint()
    {
        var totalWeight = this.itemLists.Sum(itemList => itemList.weight);
        var randomWeight = Random.Range(0f, totalWeight);

        foreach (var itemList in this.itemLists)
        {
            if (itemList.weight >= randomWeight)
            {
                return itemList.GetRandomItemBlueprint();
            }

            randomWeight -= itemList.weight;
        }

        throw new System.Exception("Item Generate Failed");
    }

    public ItemBlueprint GetRandomItemBlueprintAndRemove(int rareTypeID)
    {
        return rareTypeID switch
        {
            1 => itemLists[0].GetRandomItemBlueprintAndRemove(),
            2 => itemLists[1].GetRandomItemBlueprintAndRemove(),
            3 => itemLists[2].GetRandomItemBlueprintAndRemove(),
            4 => itemLists[3].GetRandomItemBlueprintAndRemove(),
            _ => GetAllRandomItemBlueprintAndRemove(),
        };
    }

    /// <summary>
    /// 모든 아이템 리스트 중에서 랜덤으로 아이템 생성 후 해당 아이템 삭제
    /// </summary>
    public ItemBlueprint GetAllRandomItemBlueprintAndRemove()
    {
        var totalWeight = this.itemLists.Sum(itemList => itemList.weight);

        return GetItemBlueprintNoDuplicate(this.itemLists, totalWeight);
    }

    /// <summary>
    /// 모든 아이템 리스트 중에서 갯수 만큼 중복없이 생성
    /// </summary>
    public ItemBlueprint[] GetRandomItemTableNoDuplicate(int amount)
    {
        var randomItemBlueprints = new ItemBlueprint[amount];
        var tempLists = this.itemLists;
        var totalWeight = tempLists.Sum(tempList => tempList.weight);

        for (int i = 0; i < amount; i++)
        {
            randomItemBlueprints[i] = GetItemBlueprintNoDuplicate(tempLists, totalWeight);
        }

        return randomItemBlueprints;

        throw new System.Exception("Item Generate Failed");
    }

    /// <summary>
    /// 아이템 리스트에서 랜덤으로 아이템 생성 후 해당 아이템 삭제
    /// </summary>
    /// <param name="tempLists"></param>
    /// <param name="totalWeight"></param>
    public ItemBlueprint GetItemBlueprintNoDuplicate(List<ItemList> tempLists, float totalWeight)
    {
        var randomWeight = Random.Range(0f, totalWeight);

        foreach (var tempList in tempLists)
        {
            if (tempList.weight >= randomWeight)
            {
                return tempList.GetRandomItemBlueprintAndRemove();
            }

            randomWeight -= tempList.weight;
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

    /// <summary>
    /// 리스트에서 랜덤으로 아이템 데이터 가져오기
    /// </summary>
    /// <returns></returns>
    public ItemBlueprint GetRandomItemBlueprint()
    {
        var random = Random.Range(0, this.itemList.Count);
        var randomItem = this.itemList[random];

        return randomItem;
    }

    /// <summary>
    /// 리스트에서 랜덤으로 데이터 가져온 후 해당 데이터 삭제
    /// </summary>
    /// <returns></returns>
    public ItemBlueprint GetRandomItemBlueprintAndRemove()
    {
        var random = Random.Range(0, this.itemList.Count);
        var randomItem = this.itemList[random];
        this.itemList.Remove(randomItem);

        return randomItem;
    }
}
#endregion