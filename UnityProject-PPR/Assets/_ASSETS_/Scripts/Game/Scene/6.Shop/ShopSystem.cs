using PSW.Core.Enums;
using UnityEngine;

public class ShopSystem : BehaviourSingleton<ShopSystem>
{
    [SerializeField] private Camera shopCamera;
    [SerializeField] private Canvas shopCanvas;

    [Header("Product List")]
    [SerializeField] private ItemBlueprint[] productRelic;
    [SerializeField] private ItemBlueprint[] productPotion;
    [SerializeField] private ItemBlueprint[] productCard;

    [Header("Shop List")]
    [SerializeField] private Transform shopItemList;
    [SerializeField] private GameObject shopItemPrefab;

    protected override void Awake()
    {
        base.Awake();

        this.productRelic = GameManager.Instance.GetRandomItemBlueprints(ItemType.Relic);
        this.productPotion = GameManager.Instance.GetRandomItemBlueprints(ItemType.Potion);
        this.productCard = GameManager.Instance.GetRandomItemBlueprints(ItemType.Card);

        DisplayProduct(this.productRelic);
        DisplayProduct(this.productPotion);
        DisplayProduct(this.productCard);

        AudioBGM.Instance.BGMChange(AudioBGM.Instance.shop);
        GameManager.Instance.CameraChange(this.shopCamera, this.shopCanvas);
    }

    /// <summary>
    /// 상품 3개씩 전시
    /// </summary>
    /// <param name="products"></param>
    private void DisplayProduct(ItemBlueprint[] products)
    {
        for (int i = 0; i < products.Length; i++)
        {
            var shopItem = Instantiate(this.shopItemPrefab, this.shopItemList).GetComponent<ShopItem>();
            
            // 맨 처음 상품은 할인 적용
            if (i == 0)
                shopItem.Set(products[i], true);
            else
                shopItem.Set(products[i]);
        }
    }
}
