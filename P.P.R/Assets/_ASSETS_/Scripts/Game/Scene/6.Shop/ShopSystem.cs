using PSW.Core.Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : BehaviourSingleton<ShopSystem>
{
    [Header("Item Blueprint List")]
    [SerializeField] private ItemBlueprint[] armors;
    [SerializeField] private ItemBlueprint[] helmets;
    [SerializeField] private ItemBlueprint[] weapons;
    [SerializeField] private ItemBlueprint[] trinkets;
    [SerializeField] private ItemBlueprint[] useables;

    [Header("Shop List")]
    [SerializeField] private Transform shopItemList;
    [SerializeField] private GameObject shopItemPrefab;

    protected override void Awake()
    {
        base.Awake();

        // ��ǰ ID [0.����, 1.����, 2.����, 3.�׼�����, 4.�Ҹ�ǰ]
        List<int> itemIDList = new() { 0, 1, 2, 3, 4 };

        // �� ������ �Ǹ� ���� => 9��
        for (int i = 0; i < 9; i++)
        {
            var shopItem = Instantiate(this.shopItemPrefab, this.shopItemList).GetComponent<ShopItem>();

            // ���� ǰ�� 3�� ����
            if (i < 3)
            {
                // �ߺ����� ��ǰ �̱�
                int random = Random.Range(0, itemIDList.Count);
                // ��ǰ ���ΰ� ����
                shopItem.Set(DisplayItem(random), true);         
                itemIDList.RemoveAt(random);
            }
            // ��� ǰ�� 3�� ����
            else if (i < 6)
            {
                int random = Random.Range(0, 4);
                shopItem.Set(DisplayItem(random));
            }
            // ������ �Ҹ�ǰ ǰ�� 3�� ����
            else
            {
                shopItem.Set(DisplayItem(4));
            }
        }

        AudioBGM.Instance.BGMChange(AudioBGM.Instance.shop);
    }

    /// <summary>
    /// ������ ������ ǰ�� ����
    /// </summary>
    /// <param name="id">��ǰ ����Ʈ ID</param>
    private ItemBlueprint DisplayItem(int id)
    {
        return id switch
        {
            0 => GetRandomItem(this.helmets),
            1 => GetRandomItem(this.armors),
            2 => GetRandomItem(this.weapons),
            3 => GetRandomItem(this.trinkets),
            4 => GetRandomItem(this.useables),
            _ => null,
        };
    }

    /// <summary>
    /// ������ �迭���� �������� �ϳ� �̱�
    /// </summary>
    /// <param name="items"></param>
    private ItemBlueprint GetRandomItem(ItemBlueprint[] items)
    {
        int random = Random.Range(0, items.Length);
        return items[random];
    }
}
