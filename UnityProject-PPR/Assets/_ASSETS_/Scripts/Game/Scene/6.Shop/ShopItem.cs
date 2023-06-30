using PSW.Core.Enums;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    private bool IsSell { get; set; }
    private bool IsSale { get; set; }

    [Header("Product")]
    [SerializeField] private Image productImage;
    [SerializeField] private TextMeshProUGUI productPriceText;
    [SerializeField] private Button buyButton;
    [SerializeField] private GameObject saleRibbon;

    [Header("Plate")]
    [SerializeField] private GameObject sellPlate;
    [SerializeField] private GameObject soldPlate;

    private ItemBlueprint itemData;
    private float productPrice = 0;

    /// <summary>
    /// 아이템을 추가하여 상점 UI로 표시
    /// </summary>
    /// <param name="data">아이템 데이터</param>
    /// <param name="isSale">할인 품목 체크</param>
    public void Set(ItemBlueprint data, bool isSale = false)
    {
        this.IsSell = false;
        this.IsSale = isSale;

        this.itemData = data;
        this.productImage.sprite = data.ItemImage;
        SetPrice();

        // 프리팹 네임 => 데이터 네임으로 변경
        this.name = "Shop_" + data.name;

        if (this.IsSale == true) saleRibbon.SetActive(true);

        this.buyButton.onClick.AddListener(Buy);
    }

    private void SetPrice()
    {
        float itemOriginPrice = 0;

        // 아이템 타입에 따른 가격 조정
        ItemType itemType = this.itemData.ItemType;
        if (itemType == ItemType.Equipment)
        {
            itemOriginPrice = SetPriceToEquipment();
        }
        else if (itemType == ItemType.Useable)
        {
            itemOriginPrice = SetPriceToUseable();
        }

        // 레어도에 따른 가격 조정
        ItemRare rareType = this.itemData.ItemRareType; 

        switch (rareType)
        {
            case ItemRare.Uncommon:
                itemOriginPrice *= ItemPrice.PRICE_WEIGHT_UNCOMMON;
                break;
            case ItemRare.Rare:
                itemOriginPrice *= ItemPrice.PRICE_WEIGHT_RARE;
                break;
        }

        // 소수점 정리
        Mathf.RoundToInt(itemOriginPrice);

        // 할인 품목일 경우, Rich Text 적용
        if (this.IsSale == true)
        {
            this.productPrice = Mathf.FloorToInt(itemOriginPrice * ItemPrice.PRICE_SALE_BIG);
            this.productPriceText.text = this.productPrice.ToString();
        }
        // 할인 품목이 아닐 경우
        else
        {
            this.productPrice = Mathf.FloorToInt(itemOriginPrice);
            this.productPriceText.text = this.productPrice.ToString();
        }
    }

    private int SetPriceToEquipment()
    {
        int priceMin = 0, priceMax = 1;
        var eqipmentData = (ItemEquipmentBlueprint)this.itemData;

        switch (eqipmentData.EquipmentType)
        {
            case EquipmentType.Helmet:
                priceMin = ItemPrice.PRICE_MIN_HELMET;
                priceMax = ItemPrice.PRICE_MAX_HELMET;
                break;
            case EquipmentType.Armor:
                priceMin = ItemPrice.PRICE_MIN_ARMOR;
                priceMax = ItemPrice.PRICE_MAX_ARMOR;
                break;
            case EquipmentType.Weapon:
                priceMin = ItemPrice.PRICE_MIN_WEAPON;
                priceMax = ItemPrice.PRICE_MAX_WEAPON;
                break;
            case EquipmentType.Trinket:
                priceMin = ItemPrice.PRICE_MIN_TRINKET;
                priceMax = ItemPrice.PRICE_MAX_TRINKET;
                break;
        }

        return Random.Range(priceMin, priceMax);
    }

    private int SetPriceToUseable()
    {
        var priceMin = ItemPrice.PRICE_MIN_POTION;
        var priceMax = ItemPrice.PRICE_MAX_POTION;
        return Random.Range(priceMin, priceMax);
    }

    /// <summary>
    /// 아이템 구매
    /// </summary>
    private void Buy()
    {
        if (this.IsSell == true) return;

        UISFX.Instance.Play(UISFX.Instance.shopBuy);

        IsSell = true;
        this.buyButton.interactable = false;

        if(InventorySystem.Instance != null) 
            InventorySystem.Instance.AddItem(this.itemData);

        this.sellPlate.SetActive(false);
        this.soldPlate.SetActive(true);
    }
}
