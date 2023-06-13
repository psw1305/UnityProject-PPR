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
        // �巡���� ������ ���� ��������
        GameObject dropped = eventData.pointerDrag;
        var tmpItem = dropped.GetComponent<InventoryItem>();

        // �巡���� �������� ��� �ƴ� ��� => return
        if (tmpItem.GetItemType() != ItemType.Equipment) return;

        // �巡���� �������ϰ� �����ڸ� Ÿ���� �ٸ� ��� => return
        if (tmpItem.GetEquipmentType() != this.equipmentType) return;

        // ���������� ���� ����
        tmpItem.SetParentAfterDrag(this.dropSlot);
        tmpItem.ItemLoad();
    }
}
