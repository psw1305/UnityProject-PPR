using PSW.Core.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class InventorySlotUseable : MonoBehaviour, IDropHandler
{
    [SerializeField] private int slotNumber;
    [SerializeField] private Transform dropSlot;
    [SerializeField] private RectTransform plate;

    public Transform GetDropSlot()
    {
        return this.dropSlot;
    }

    public void AnimatePlateImage(float endValue)
    {
        this.plate.DOScale(endValue, 0.1f);
    }

    public void OnDrop(PointerEventData eventData)
    {
        // 드래그한 아이템 정보 가져오기
        GameObject dropped = eventData.pointerDrag;
        var tmpItem = dropped.GetComponent<InventoryItem>();

        // 드래그한 아이템이 소모품이 아닐 경우 => return
        if (tmpItem.GetItemType() != ItemType.Useable) return;

        // 장착된 소모품 아닐 경우 => 성공적으로 슬롯 장착, return
        if (!tmpItem.IsEquip)
        {
            tmpItem.ItemLoad(this.slotNumber); return;
        }

        // 슬롯 자리에 이미 아이템이 있는 경우 => change
        if (this.dropSlot.childCount != 0)
        {
            tmpItem.ItemChange(this.slotNumber);
        }
        // 슬롯 자리에 이미 아이템이 없는 경우 => move
        else
        {
            tmpItem.ItemMove(this.slotNumber);
        }
    }
}
