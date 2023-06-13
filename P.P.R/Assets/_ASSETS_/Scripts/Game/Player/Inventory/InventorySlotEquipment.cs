using PSW.Core.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class InventorySlotEquipment : MonoBehaviour, IDropHandler
{
    [SerializeField] private EquipmentType equipmentType;
    [SerializeField] private Transform dropSlot;
    [SerializeField] private RectTransform plate;

    public void AnimatePlateImage(float endValue)
    {
        this.plate.DOScale(endValue, 0.1f);
    }

    public void OnDrop(PointerEventData eventData)
    {
        // 드래그한 아이템 정보 가져오기
        GameObject dropped = eventData.pointerDrag;
        var tmpItem = dropped.GetComponent<InventoryItem>();

        // 드래그한 아이템이 장비가 아닐 경우 => return
        if (tmpItem.GetItemType() != ItemType.Equipment) return;

        // 드래그한 아이템하고 슬롯자리 타입이 다를 경우 => return
        if (tmpItem.GetEquipmentType() != this.equipmentType) return;

        // 성공적으로 슬롯 장착
        tmpItem.SetParentAfterDrag(this.dropSlot);
        tmpItem.ItemLoad();
    }
}
