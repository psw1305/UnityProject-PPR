using PSW.Core.Enums;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    private bool IsSell { get; set; }
    private bool IsSale { get; set; }

    [Header("Product")]
    [SerializeField] private Button productButton;
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
        this.productNameText.text = data.ItemName;
        this.productButton.onClick.AddListener(Detail);

        // 상품 가격 설정
        SetPrice();

        // 프리팹 네임 => 데이터 네임으로 변경
        this.name = "Shop_" + data.name;
    }

    /// <summary>
    /// 상품 타입과 등급에 따른 가격 설정
    /// </summary>
    private void SetPrice()
    {
        var itemOriginPrice = 0;

        // 아이템 타입과 등급에 따른 가격 조정
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

        // 할인 상품 가격 조정
        if (this.IsSale == true)
        {
            this.saleRibbon.SetActive(true);
            this.productPrice = Mathf.FloorToInt(itemOriginPrice * CASH.PRICE_SALE_BIG);
            this.productPriceText.text = this.productPrice.ToString();
        }
        else
        {
            this.productPrice = Mathf.RoundToInt(itemOriginPrice);
            this.productPriceText.text = this.productPrice.ToString();
        }

        // 구매 버튼 리스너 추가
        this.buyButton.onClick.AddListener(Buy);
    }

    private int SetPriceToRelic(ItemGrade itemGrade)
    {
        switch (itemGrade)
        {
            case ItemGrade.Common:
                return Random.Range(CASH.PRICE_MIN_RELIC_COMMON, CASH.PRICE_MAX_RELIC_COMMON);
            case ItemGrade.Uncommon:
                return Random.Range(CASH.PRICE_MIN_RELIC_UNCOMMON, CASH.PRICE_MAX_RELIC_UNCOMMON);
            case ItemGrade.Rare:
                return Random.Range(CASH.PRICE_MIN_RELIC_RARE, CASH.PRICE_MAX_RELIC_RARE);
        }

        throw new System.Exception("There is no item price for this grade");
    }

    private int SetPriceToPotion(ItemGrade itemGrade)
    {
        switch (itemGrade)
        {
            case ItemGrade.Common:
                return Random.Range(CASH.PRICE_MIN_POTION_COMMON, CASH.PRICE_MAX_POTION_COMMON);
            case ItemGrade.Uncommon:
                return Random.Range(CASH.PRICE_MIN_POTION_UNCOMMON, CASH.PRICE_MAX_POTION_UNCOMMON);
            case ItemGrade.Rare:
                return Random.Range(CASH.PRICE_MIN_POTION_RARE, CASH.PRICE_MAX_POTION_RARE);
        }

        throw new System.Exception("There is no item price for this grade");
    }

    private int SetPriceToCard(ItemGrade itemGrade)
    {
        switch (itemGrade)
        {
            case ItemGrade.Common:
                return Random.Range(CASH.PRICE_MIN_CARD_COMMON, CASH.PRICE_MAX_CARD_COMMON);
            case ItemGrade.Uncommon:
                return Random.Range(CASH.PRICE_MIN_CARD_UNCOMMON, CASH.PRICE_MAX_CARD_UNCOMMON);
            case ItemGrade.Rare:
                return Random.Range(CASH.PRICE_MIN_CARD_RARE, CASH.PRICE_MAX_CARD_RARE);
        }

        throw new System.Exception("There is no item price for this grade");
    }

    /// <summary>
    /// 버튼 클릭 시 상품 상세 설명
    /// </summary>
    private void Detail()
    {
        if (PlayerItemTooltip.Instance == null) return;

        switch (this.itemData.ItemType)
        {
            case ItemType.Card:
                PlayerItemTooltip.Instance.CardTooltipShow(this.itemData);
                break;
            case ItemType.Relic:
                PlayerItemTooltip.Instance.RelicTooltipShow(this.itemData);
                break;
            case ItemType.Potion:
                PlayerItemTooltip.Instance.PotionTooltipShow(this.itemData);
                break;
        }
    }

    /// <summary>
    /// 버튼 클릭 시 상품 구매
    /// </summary>
    private void Buy()
    {
        if (this.IsSell == true) return;    
        if (Player.Instance == null) return;

        // 거래 금액이 플레이어 재화보다 적은가?
        if (Player.Instance.UseCash(this.productPrice))
        {
            UISFX.Instance.Play(UISFX.Instance.shopBuy);

            switch (this.itemData.ItemType)
            {
                case ItemType.Card:
                    Player.Instance.AddItemCard(this.itemData);
                    break;
                case ItemType.Relic:
                    Player.Instance.AddItemRelic(this.itemData);
                    break;
                case ItemType.Potion:
                    Player.Instance.AddItemPotion(this.itemData);
                    break;
            }

            this.IsSell = true;
            this.buyButton.interactable = false;

            this.sellPlate.SetActive(false);
            this.soldPlate.SetActive(true);
        }
        else
        {
            // 거래 실패 시 클릭 사운드만 출력
            UISFX.Instance.Play(UISFX.Instance.buttonClick);
        }
    }
}
