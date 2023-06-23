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
        // �巡���� ������ ���� ��������
        GameObject dropped = eventData.pointerDrag;
        var tmpItem = dropped.GetComponent<InventoryItem>();

        // �巡���� �������� �Ҹ�ǰ�� �ƴ� ��� => return
        if (tmpItem.GetItemType() != ItemType.Useable) return;

        // ������ �Ҹ�ǰ �ƴ� ��� => ���������� ���� ����, return
        if (!tmpItem.IsEquip)
        {
            tmpItem.ItemLoad(this.slotNumber); return;
        }

        // ���� �ڸ��� �̹� �������� �ִ� ��� => change
        if (this.dropSlot.childCount != 0)
        {
            tmpItem.ItemChange(this.slotNumber);
        }
        // ���� �ڸ��� �̹� �������� ���� ��� => move
        else
        {
            tmpItem.ItemMove(this.slotNumber);
        }
    }
}
