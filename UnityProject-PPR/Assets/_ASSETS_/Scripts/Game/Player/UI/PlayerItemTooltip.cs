using PSW.Core.Enums;
using PSW.Core.Extensions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerItemTooltip : BehaviourSingleton<PlayerItemTooltip>
{
    [Header("Card")]
    [SerializeField] private CanvasGroup cardTooltipCanvas;
    [SerializeField] private Button cardTooltipClose;
    [SerializeField] private Image cardImage;
    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private TextMeshProUGUI cardRare;
    [SerializeField] private TextMeshProUGUI cardAbility;
    [SerializeField] private TextMeshProUGUI cardDesc;

    [Header("Relic")]
    [SerializeField] private CanvasGroup relicTooltipCanvas;
    [SerializeField] private Button relicTooltipClose;
    [SerializeField] private Image relicImage;
    [SerializeField] private TextMeshProUGUI relicName;
    [SerializeField] private TextMeshProUGUI relicRare;
    [SerializeField] private TextMeshProUGUI relicAbility;
    [SerializeField] private TextMeshProUGUI relicDesc;

    [Header("Potion")]
    [SerializeField] private CanvasGroup potionTooltipCanvas;
    [SerializeField] private Button potionTooltipClose;
    [SerializeField] private Image potionImage;
    [SerializeField] private TextMeshProUGUI potionName;
    [SerializeField] private TextMeshProUGUI potionRare;
    [SerializeField] private TextMeshProUGUI potionAbility;
    [SerializeField] private TextMeshProUGUI potionDesc;

    protected override void Awake()
    {
        base.Awake();

        this.cardTooltipCanvas.CanvasInit();
        this.relicTooltipCanvas.CanvasInit();
        this.potionTooltipCanvas.CanvasInit();

        this.cardTooltipClose.onClick.AddListener(CardTooltipHide);
        this.relicTooltipClose.onClick.AddListener(RelicTooltipHide);
        this.potionTooltipClose.onClick.AddListener(PotionTooltipHide);
    }

    /// <summary>
    /// �ش� ������ �����Ϳ� �°� UI �ο�
    /// </summary>
    /// <param name="invenItem">���� ������</param>
    private void SetTooltip(InventoryItem invenItem)
    {
        var itemData = invenItem.GetItemData();

        // ������ Ÿ�Կ� ���� ���� ����
        if (itemData.ItemType == ItemType.Card)
        {
            this.cardImage.sprite = itemData.ItemImage;
            this.cardRare.ItemGradeColor(itemData.ItemGrade);
            this.cardName.text = itemData.ItemName;
            this.cardAbility.text = itemData.ItemAbility;
            this.cardDesc.text = itemData.ItemDesc;
        }
        else if (itemData.ItemType == ItemType.Relic)
        {
            this.relicImage.sprite = itemData.ItemImage;
            this.relicRare.ItemGradeColor(itemData.ItemGrade);
            this.relicName.text = itemData.ItemName;
            this.relicAbility.text = itemData.ItemAbility;
            this.relicDesc.text = itemData.ItemDesc;
        }
        else if (itemData.ItemType == ItemType.Potion)
        {
            this.potionImage.sprite = itemData.ItemImage;
            this.potionRare.ItemGradeColor(itemData.ItemGrade);
            this.potionName.text = itemData.ItemName;
            this.potionAbility.text = itemData.ItemAbility;
            this.potionDesc.text = itemData.ItemDesc;
        }
    }

    /// <summary>
    /// ī�� ���� ǥ��
    /// </summary>
    /// <param name="invenItem">�κ��丮 ������</param>
    public void CardTooltipShow(InventoryItem invenItem)
    {
        UISFX.Instance.Play(UISFX.Instance.itemOpens);

        SetTooltip(invenItem);

        this.cardTooltipClose.interactable = true;
        this.cardTooltipCanvas.CanvasFadeIn(DUR.CANVAS_FADE_TIME);
    }

    /// <summary>
    /// ī�� ���� ����
    /// </summary>
    private void CardTooltipHide()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        this.cardTooltipClose.interactable = false;
        this.cardTooltipCanvas.CanvasFadeOut(DUR.CANVAS_FADE_TIME);
    }

    /// <summary>
    /// ���� ���� ǥ��
    /// </summary>
    /// <param name="invenItem">�κ��丮 ������</param>
    public void RelicTooltipShow(InventoryItem invenItem)
    {
        UISFX.Instance.Play(UISFX.Instance.itemOpens);

        SetTooltip(invenItem);

        this.relicTooltipClose.interactable = true;
        this.relicTooltipCanvas.CanvasFadeIn(DUR.CANVAS_FADE_TIME);
    }

    /// <summary>
    /// ���� ���� ����
    /// </summary>
    private void RelicTooltipHide()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        this.relicTooltipClose.interactable = false;
        this.relicTooltipCanvas.CanvasFadeOut(DUR.CANVAS_FADE_TIME);
    }

    /// <summary>
    /// ���� ���� ǥ��
    /// </summary>
    /// <param name="invenItem">�κ��丮 ������</param>
    public void PotionTooltipShow(InventoryItem invenItem)
    {
        UISFX.Instance.Play(UISFX.Instance.itemOpens);

        SetTooltip(invenItem);

        this.potionTooltipClose.interactable = true;
        this.potionTooltipCanvas.CanvasFadeIn(DUR.CANVAS_FADE_TIME);
    }

    /// <summary>
    /// ���� ���� ����
    /// </summary>
    public void PotionTooltipHide()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        this.potionTooltipClose.interactable = false;
        this.potionTooltipCanvas.CanvasFadeOut(DUR.CANVAS_FADE_TIME);
    }
}
