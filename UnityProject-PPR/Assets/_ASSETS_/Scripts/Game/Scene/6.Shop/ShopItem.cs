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
    /// �������� �߰��Ͽ� ���� UI�� ǥ��
    /// </summary>
    /// <param name="data">������ ������</param>
    /// <param name="isSale">���� ǰ�� üũ</param>
    public void Set(ItemBlueprint data, bool isSale = false)
    {
        this.IsSell = false;
        this.IsSale = isSale;

        this.itemData = data;
        this.productImage.sprite = data.ItemImage;
        SetPrice();

        // ������ ���� => ������ �������� ����
        this.name = "Shop_" + data.name;

        if (this.IsSale == true) saleRibbon.SetActive(true);

        this.buyButton.onClick.AddListener(Buy);
    }

    private void SetPrice()
    {
        float itemOriginPrice = 0;

        // ������ Ÿ�Կ� ���� ���� ����
        ItemType itemType = this.itemData.ItemType;
        if (itemType == ItemType.Equipment)
        {
            itemOriginPrice = SetPriceToEquipment();
        }
        else if (itemType == ItemType.Useable)
        {
            itemOriginPrice = SetPriceToUseable();
        }

        // ����� ���� ���� ����
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

        // �Ҽ��� ����
        Mathf.RoundToInt(itemOriginPrice);

        // ���� ǰ���� ���, Rich Text ����
        if (this.IsSale == true)
        {
            this.productPrice = Mathf.FloorToInt(itemOriginPrice * ItemPrice.PRICE_SALE_BIG);
            this.productPriceText.text = this.productPrice.ToString();
        }
        // ���� ǰ���� �ƴ� ���
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
    /// ������ ����
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
