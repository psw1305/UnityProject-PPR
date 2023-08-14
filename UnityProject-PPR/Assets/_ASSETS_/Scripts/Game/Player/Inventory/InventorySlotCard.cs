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
        // �巡���� ������ ���� ��������
        GameObject dropped = eventData.pointerDrag;
        var tmpCard = dropped.GetComponent<InventoryItemCard>();

        // �巡���� �������� ��� �ƴ� ��� => return
        if (tmpCard.GetItemType() != ItemType.Card) return;

        // ���������� ���� ����
        tmpCard.SetParentAfterDrag(this.dropSlot);
        tmpCard.CardLoad();
    }
}
