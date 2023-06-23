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

        // 상품 ID [0.투구, 1.갑옷, 2.무기, 3.액세서리, 4.소모품]
        List<int> itemIDList = new() { 0, 1, 2, 3, 4 };

        // 총 아이템 판매 갯수 => 9개
        for (int i = 0; i < 9; i++)
        {
            var shopItem = Instantiate(this.shopItemPrefab, this.shopItemList).GetComponent<ShopItem>();

            // 할인 품목 3개 전시
            if (i < 3)
            {
                // 중복없이 상품 뽑기
                int random = Random.Range(0, itemIDList.Count);
                // 상품 할인가 적용
                shopItem.Set(DisplayItem(random), true);         
                itemIDList.RemoveAt(random);
            }
            // 장비 품목 3개 전시
            else if (i < 6)
            {
                int random = Random.Range(0, 4);
                shopItem.Set(DisplayItem(random));
            }
            // 나머지 소모품 품목 3개 전시
            else
            {
                shopItem.Set(DisplayItem(4));
            }
        }

        AudioBGM.Instance.BGMChange(AudioBGM.Instance.shop);
    }

    /// <summary>
    /// 상점에 전시할 품목 선택
    /// </summary>
    /// <param name="id">상품 리스트 ID</param>
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
    /// 아이템 배열에서 랜덤으로 하나 뽑기
    /// </summary>
    /// <param name="items"></param>
    private ItemBlueprint GetRandomItem(ItemBlueprint[] items)
    {
        int random = Random.Range(0, items.Length);
        return items[random];
    }
}
