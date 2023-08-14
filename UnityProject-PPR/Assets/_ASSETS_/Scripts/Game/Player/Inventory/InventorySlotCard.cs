using PSW.Core.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class InventorySlotCard : MonoBehaviour, IDropHandler
{
    [SerializeField] private Transform dropSlot;

    public void AnimatePlateImage(float endValue)
    {
        this.dropSlot.DOScale(endValue, 0.1f);
    }

    public void OnDrop(PointerEventData eventData)
    {
        // 드래그한 아이템 정보 가져오기
        GameObject dropped = eventData.pointerDrag;
        var tmpCard = dropped.GetComponent<InventoryItemCard>();

        // 드래그한 아이템이 장비가 아닐 경우 => return
        if (tmpCard.GetItemType() != ItemType.Card) return;

        // 성공적으로 슬롯 장착
        tmpCard.SetParentAfterDrag(this.dropSlot);
        tmpCard.CardLoad();
    }
}
