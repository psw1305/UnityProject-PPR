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

        this.productRelic = GameManager.Instance.GetRandomItemTable(ItemType.Relic, 3);
        this.productPotion = GameManager.Instance.GetRandomItemTable(ItemType.Potion, 3);
        this.productCard = GameManager.Instance.GetRandomItemTable(ItemType.Card, 3);

        DisplayProduct(this.productRelic);
        DisplayProduct(this.productPotion);
        DisplayProduct(this.productCard);

        AudioBGM.Instance.BGMChange(AudioBGM.Instance.shop);
        GameManager.Instance.CameraChange(this.shopCamera, this.shopCanvas);
    }

    /// <summary>
    /// 상품 전시
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
