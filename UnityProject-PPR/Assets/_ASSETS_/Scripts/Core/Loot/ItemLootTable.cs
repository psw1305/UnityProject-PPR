using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemLootTable : MonoBehaviour
{
    // [ID] 0. All Random / 1. Common / 2. Uncommon / 3. Rare / 4. Boss
    [SerializeField] private List<ItemList> itemLists = new();

    /// <summary>
    /// ������ ��޺� �Ǵ� ��� �߷� ���� ����
    /// </summary>
    public ItemBlueprint GetItemAdd(int rareTypeID)
    {
        return rareTypeID switch
        {
            1 => this.itemLists[0].GetRandomItem(),
            2 => this.itemLists[1].GetRandomItem(),
            3 => this.itemLists[2].GetRandomItem(),
            4 => this.itemLists[3].GetRandomItem(),
            _ => GetRandomItemToAllList(),
        };
    }

    /// <summary>
    /// ������ ��޺��� ���� �ߺ� ����
    /// </summary>
    public ItemBlueprint GetItemAdd_Duplicate(int rareTypeID)
    {
        return rareTypeID switch
        {
            1 => this.itemLists[0].GetRandomItem_Duplicate(),
            2 => this.itemLists[1].GetRandomItem_Duplicate(),
            3 => this.itemLists[2].GetRandomItem_Duplicate(),
            4 => this.itemLists[3].GetRandomItem_Duplicate(),
            _ => GetRandomItemToAllList_Duplicate(),
        };
    }

    /// <summary>
    /// ��� ������ ����Ʈ �߿��� ���� ��ŭ �ߺ����� ����
    /// </summary>
    public ItemBlueprint[] GetItemTable(int amount)
    {
        var cloneLists = this.itemLists.ConvertAll(list => list.Clone()).ToList();
        var totalWeight = cloneLists.Sum(cloneList => cloneList.weight);

        var randomItemBlueprints = new ItemBlueprint[amount];

        for (int i = 0; i < amount; i++)
        {
            randomItemBlueprints[i] = GetWeightedItem(cloneLists, totalWeight);
        }

        return randomItemBlueprints;
    }

    /// <summary>
    /// ������ ����Ʈ �߿��� ����ġ�� ���� ���� ������ ���� �� �ش� ������ ����
    /// </summary>
    public ItemBlueprint GetRandomItemToAllList()
    {
        var totalWeight = this.itemLists.Sum(itemList => itemList.weight);

        return GetWeightedItem(this.itemLists, totalWeight);
    }

    /// <summary>
    /// ������ ����Ʈ �߿��� ����ġ�� ���� ���� ������ ���� (�ߺ� ����)
    /// </summary>
    public ItemBlueprint GetRandomItemToAllList_Duplicate()
    {
        var totalWeight = this.itemLists.Sum(itemList => itemList.weight);

        return GetWeightedItem_Duplicate(this.itemLists, totalWeight);
    }

    /// <summary>
    /// ������ ����Ʈ���� �������� ������ ���� �� �ش� ������ ����
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

        throw new System.Exception("Weighted Item Generate Failed");
    }

    /// <summary>
    /// ������ ����Ʈ���� �������� ������ ���� (�ߺ� ����)
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

        throw new System.Exception("Weighted Item_Duplicate Generate Failed");
    }
}

#region Item List Class
[System.Serializable]
public class ItemList
{
    public string listName;
    public List<ItemBlueprint> itemList;
    public float weight;

    public ItemList Clone()
    {
        return new ItemList 
        { 
            listName = this.listName, 
            itemList = this.itemList.ToList(), // �� ����
            weight = this.weight 
        };
    }

    /// <summary>
    /// ����Ʈ���� �������� ������ ������ �� �ش� ������ ����
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
    /// ����Ʈ���� �������� ������ ������ �������� (��ø ����)
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