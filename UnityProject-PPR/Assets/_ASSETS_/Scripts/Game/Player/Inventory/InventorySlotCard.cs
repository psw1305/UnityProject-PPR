using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class InventorySlotCard : MonoBehaviour, IDropHandler
{
    [SerializeField] private Transform plate;
    [SerializeField] private Transform dropSlot;

    public void AnimatePlate(float endValue)
    {
        this.plate.DOScale(endValue, 0.1f);
    }

    public void OnDrop(PointerEventData eventData)
    {
        // 드래그한 아이템 정보 가져오기
        var dropCard = eventData.pointerDrag.GetComponent<InventoryItemCard>();

        dropCard.SetParentAfterDrag(this.dropSlot);
        dropCard.CardLoad();
    }
}
