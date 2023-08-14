using PSW.Core.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemCard : InventoryItem, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image plate;
    private Transform parentAfterDrag;

    public void SetParentAfterDrag(Transform dropTransform)
    {
        this.parentAfterDrag = dropTransform;
    }

    public ItemBlueprintCard GetCardData()
    {
        return (ItemBlueprintCard)this.blueprint;
    }

    public CardType GetCardType()
    {
        var equipData = (ItemBlueprintCard)this.blueprint;
        return equipData.CardType;
    }

    /// <summary>
    /// 카드 덱에 등록
    /// </summary>
    public void CardLoad()
    {
        if (this.IsEquip == true) return;

        if (GetItemType() == ItemType.Card)
        {
            Player.Instance.EquipSkillCard(this);
        }
    }

    /// <summary>
    /// 플레이어 장비창에 아이템 해제 [기본값 = 0]
    /// </summary>
    public void CardUnload()
    {
        if (this.IsEquip == false) return;

        if (GetItemType() == ItemType.Card)
        {
            Player.Instance.UnequipSkillCard(this);
        }
    }

    /// <summary>
    /// 덱에서 카드 이동
    /// </summary>
    /// <param name="changSlotNumber">바꿀 슬롯 자리 숫자</param>
    public void CardMove(int changSlotNumber)
    {
        //Player.Instance.PotionItemMove(this, this.slotNumber, changSlotNumber);
    }

    /// <summary>
    /// 덱에서 카드 교체
    /// </summary>
    /// <param name="changSlotNumber">바꿀 슬롯 자리 숫자</param>
    public void CardChange(int changSlotNumber)
    {
        //Player.Instance.PotionItemChange(this.slotNumber, changSlotNumber);
    }

    /// <summary>
    /// 아이템 드래그 시작시
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        UISFX.Instance.Play(UISFX.Instance.itemDrag);

        this.parentAfterDrag = this.transform.parent;
        this.transform.SetParent(PlayerUI.Instance.GetCardDragParent());
        this.transform.SetAsLastSibling();
        this.plate.raycastTarget = false;

        PlayerUI.Instance.ItemOnBeginDrag(this);
    }

    /// <summary>
    /// 아이템 드래그 중
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = currentPosition;
    }

    /// <summary>
    /// 아이템 드래그 끝날시
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        UISFX.Instance.ItemDropSFX(this);

        this.transform.SetParent(this.parentAfterDrag);
        this.plate.raycastTarget = true;

        PlayerUI.Instance.ItemOnEndDrag(this);
    }
}
