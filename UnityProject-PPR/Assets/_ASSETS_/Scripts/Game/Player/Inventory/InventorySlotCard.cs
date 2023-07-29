using PSW.Core.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class InventorySlotCard : MonoBehaviour, IDropHandler
{
    [SerializeField] private CardType cardType;
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
        var tmpItem = dropped.GetComponent<InventoryItemCard>();

        // �巡���� �������� ��� �ƴ� ��� => return
        if (tmpItem.GetItemType() != ItemType.Card) return;

        // �巡���� �������ϰ� �����ڸ� Ÿ���� �ٸ� ��� => return
        if (tmpItem.GetCardType() != this.cardType) return;

        // ���������� ���� ����
        tmpItem.SetParentAfterDrag(this.dropSlot);
        tmpItem.CardLoad();
    }
}