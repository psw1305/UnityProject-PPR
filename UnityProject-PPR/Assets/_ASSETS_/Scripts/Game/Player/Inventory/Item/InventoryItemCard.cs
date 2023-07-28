using PSW.Core.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemCard : InventoryItem, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image plate;
    private int slotNumber = 0;
    private Transform parentAfterDrag;

    public override void Set(ItemBlueprint data)
    {

    }

    public void SetSlotNumber(int slotNumber)
    {
        this.slotNumber = slotNumber;
    }

    public void SetParentAfterDrag(Transform dropTransform)
    {
        this.parentAfterDrag = dropTransform;
    }

    public ItemBlueprintCard GetCardData()
    {
        return (ItemBlueprintCard)this.itemData;
    }

    public CardType GetCardType()
    {
        var equipData = (ItemBlueprintCard)this.itemData;
        return equipData.CardType;
    }

    /// <summary>
    /// 플레이어 장비창에 아이템 등록 [기본값 = 0]
    /// </summary>
    /// /// <param name="slotNumber">장비창 슬롯 자리 숫자</param>
    public void CardLoad(int slotNumber = 0)
    {
        if (this.IsEquip == true) return;

        if (GetItemType() == ItemType.Relic)
        {
            Player.Instance.EquipmentLoad(this);
        }
        else if (GetItemType() == ItemType.Potion)
        {
            this.slotNumber = slotNumber;
            //Player.Instance.PotionItemLoad(this, this.slotNumber);
        }
    }

    /// <summary>
    /// 플레이어 장비창에 아이템 해제 [기본값 = 0]
    /// </summary>
    public void ItemUnload()
    {
        if (this.IsEquip == false) return;

        if (GetItemType() == ItemType.Relic)
        {
            Player.Instance.EquipmentUnload(this);
        }
        else if (GetItemType() == ItemType.Potion)
        {
            //Player.Instance.PotionItemUnload(this, this.slotNumber);
        }
    }

    /// <summary>
    /// 장비창에서 소모품 이동
    /// </summary>
    /// <param name="changSlotNumber">바꿀 슬롯 자리 숫자</param>
    public void ItemMove(int changSlotNumber)
    {
        //Player.Instance.PotionItemMove(this, this.slotNumber, changSlotNumber);
    }

    /// <summary>
    /// 장비창에서 소모품 교환
    /// </summary>
    /// <param name="changSlotNumber">바꿀 슬롯 자리 숫자</param>
    public void ItemChange(int changSlotNumber)
    {
        //Player.Instance.PotionItemChange(this.slotNumber, changSlotNumber);
    }

    /// <summary>
    /// 드래그 시작시
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        UISFX.Instance.Play(UISFX.Instance.itemDrag);

        this.parentAfterDrag = this.transform.parent;
        this.transform.SetParent(InventorySystem.Instance.transform);
        this.transform.SetAsLastSibling();
        this.plate.raycastTarget = false;

        //PlayerUI.Instance.ItemOnBeginDrag(this);
    }

    /// <summary>
    /// 드래그 중
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = currentPosition;
    }

    /// <summary>
    /// 드래그 끝날시
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        UISFX.Instance.ItemDropSFX(this);

        this.transform.SetParent(this.parentAfterDrag);
        this.plate.raycastTarget = true;

        //PlayerUI.Instance.ItemOnEndDrag(this);
    }
}
