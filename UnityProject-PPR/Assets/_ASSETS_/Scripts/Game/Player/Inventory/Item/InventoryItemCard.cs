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
    /// ī�� ���� ���
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
    /// �÷��̾� ���â�� ������ ���� [�⺻�� = 0]
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
    /// ������ ī�� �̵�
    /// </summary>
    /// <param name="changSlotNumber">�ٲ� ���� �ڸ� ����</param>
    public void CardMove(int changSlotNumber)
    {
        //Player.Instance.PotionItemMove(this, this.slotNumber, changSlotNumber);
    }

    /// <summary>
    /// ������ ī�� ��ü
    /// </summary>
    /// <param name="changSlotNumber">�ٲ� ���� �ڸ� ����</param>
    public void CardChange(int changSlotNumber)
    {
        //Player.Instance.PotionItemChange(this.slotNumber, changSlotNumber);
    }

    /// <summary>
    /// ������ �巡�� ���۽�
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
    /// ������ �巡�� ��
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = currentPosition;
    }

    /// <summary>
    /// ������ �巡�� ������
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
