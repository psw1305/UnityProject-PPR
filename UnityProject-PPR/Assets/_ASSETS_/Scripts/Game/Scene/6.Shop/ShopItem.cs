using PSW.Core.Enums;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ShopItem : MonoBehaviour
{
    private bool IsSell { get; set; }
    private bool IsSale { get; set; }

    [Header("Product")]
    [SerializeField] private Image productImage;
    [SerializeField] private TextMeshProUGUI productNameText;
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
        this.productNameText.text = data.ItemName;

        // ��ǰ ���� ����
        SetPrice();

        // ������ ���� => ������ �������� ����
        this.name = "Shop_" + data.name;
    }

    /// <summary>
    /// ��ǰ Ÿ�԰� ��޿� ���� ���� ����
    /// </summary>
    private void SetPrice()
    {
        var itemOriginPrice = 0;

        // ������ Ÿ�԰� ��޿� ���� ���� ����
        switch (this.itemData.ItemType)
        {
            case ItemType.Relic:
                itemOriginPrice = SetPriceToRelic(this.itemData.ItemGrade);
                break;
            case ItemType.Potion:
                itemOriginPrice = SetPriceToPotion(this.itemData.ItemGrade);
                break;
            case ItemType.Card:
                itemOriginPrice = SetPriceToCard(this.itemData.ItemGrade);
                break;
        }

        // ���� ��ǰ ���� ����
        if (this.IsSale == true)
        {
            this.saleRibbon.SetActive(true);
            this.productPrice = Mathf.FloorToInt(itemOriginPrice * ItemPrice.PRICE_SALE_BIG);
            this.productPriceText.text = this.productPrice.ToString();
        }
        else
        {
            this.productPrice = Mathf.RoundToInt(itemOriginPrice);
            this.productPriceText.text = this.productPrice.ToString();
        }

        // ���� ��ư ������ �߰�
        this.buyButton.onClick.AddListener(Buy);
    }

    private int SetPriceToRelic(ItemGrade itemGrade)
    {
        switch (itemGrade)
        {
            case ItemGrade.Common:
                return Random.Range(ItemPrice.PRICE_MIN_RELIC_COMMON, ItemPrice.PRICE_MAX_RELIC_COMMON);
            case ItemGrade.Uncommon:
                return Random.Range(ItemPrice.PRICE_MIN_RELIC_UNCOMMON, ItemPrice.PRICE_MAX_RELIC_UNCOMMON);
            case ItemGrade.Rare:
                return Random.Range(ItemPrice.PRICE_MIN_RELIC_RARE, ItemPrice.PRICE_MAX_RELIC_RARE);
            default:
                Debug.Log("�ش� ��޿� ������ ����");
                return 0;
        }
    }

    private int SetPriceToPotion(ItemGrade itemGrade)
    {
        switch (itemGrade)
        {
            case ItemGrade.Common:
                return Random.Range(ItemPrice.PRICE_MIN_POTION_COMMON, ItemPrice.PRICE_MAX_POTION_COMMON);
            case ItemGrade.Uncommon:
                return Random.Range(ItemPrice.PRICE_MIN_POTION_UNCOMMON, ItemPrice.PRICE_MAX_POTION_UNCOMMON);
            case ItemGrade.Rare:
                return Random.Range(ItemPrice.PRICE_MIN_POTION_RARE, ItemPrice.PRICE_MAX_POTION_RARE);
            default:
                Debug.Log("�ش� ��޿� ������ ����");
                return 0;
        }
    }

    private int SetPriceToCard(ItemGrade itemGrade)
    {
        switch (itemGrade)
        {
            case ItemGrade.Common:
                return Random.Range(ItemPrice.PRICE_MIN_CARD_COMMON, ItemPrice.PRICE_MAX_CARD_COMMON);
            case ItemGrade.Uncommon:
                return Random.Range(ItemPrice.PRICE_MIN_CARD_UNCOMMON, ItemPrice.PRICE_MAX_CARD_UNCOMMON);
            case ItemGrade.Rare:
                return Random.Range(ItemPrice.PRICE_MIN_CARD_RARE, ItemPrice.PRICE_MAX_CARD_RARE);
            default:
                Debug.Log("�ش� ��޿� ������ ����");
                return 0;
        }
    }

    /// <summary>
    /// ��ǰ ����
    /// </summary>
    private void Buy()
    {
        if (this.IsSell == true) return;

        UISFX.Instance.Play(UISFX.Instance.shopBuy);

        IsSell = true;
        this.buyButton.interactable = false;

        this.sellPlate.SetActive(false);
        this.soldPlate.SetActive(true);
    }
}
