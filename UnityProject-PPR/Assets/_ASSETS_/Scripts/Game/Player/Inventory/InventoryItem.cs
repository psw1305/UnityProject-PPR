using PSW.Core.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool IsEquip { get; set; }

    [SerializeField] private Image plate;
    [SerializeField] private Image image;

    private Button button;
    private ItemBlueprint itemData;
    private int slotNumber = 0;
    private Transform parentAfterDrag;

    /// <summary>
    /// �������� �߰��Ͽ� �����ϰ� UI�� ǥ��
    /// </summary>
    /// <param name="data">������ ������</param>
    public void Set(ItemBlueprint data)
    {
        this.IsEquip = false;
        this.itemData = data;
        this.image.sprite = data.ItemImage;

        this.button = GetComponent<Button>();
        this.button.onClick.AddListener(ItemClick);

        // ������ ���� => ������ �������� ����
        this.name = data.name;
    }

    public ItemBlueprint GetItemData()
    {
        return this.itemData;
    }

    public ItemType GetItemType()
    {
        return this.itemData.ItemType;
    }

    public ItemBlueprintArtifact GetEquipmentData()
    {
        return (ItemBlueprintArtifact)this.itemData;
    }

    public CardType GetEquipmentType()
    {
        var equipData = (ItemBlueprintArtifact)this.itemData;
        return equipData.EquipmentType;
    }

    public ItemUseableBlueprint GetUseableData()
    {
        return (ItemUseableBlueprint)this.itemData;
    }

    public void SetSlotNumber(int slotNumber)
    {
        this.slotNumber = slotNumber;
    }

    public void SetParentAfterDrag(Transform dropTransform)
    {
        this.parentAfterDrag = dropTransform;
    }

    /// <summary>
    /// �κ��丮���� ������ Ŭ���� ���� ǥ��
    /// </summary>
    private void ItemClick()
    {
        InventoryItemTooltip.Instance.Show(this);
    }

    /// <summary>
    /// �÷��̾� ���â�� ������ ��� [�⺻�� = 0]
    /// </summary>
    /// /// <param name="slotNumber">���â ���� �ڸ� ����</param>
    public void ItemLoad(int slotNumber = 0)
    {
        if (this.IsEquip == true) return;

        if (GetItemType() == ItemType.Artifact)
        {
            Player.Instance.EquipmentLoad(this);
        }
        else if (GetItemType() == ItemType.Potion)
        {
            this.slotNumber = slotNumber;
            Player.Instance.PotionItemLoad(this, this.slotNumber);
        }
    }

    /// <summary>
    /// �÷��̾� ���â�� ������ ���� [�⺻�� = 0]
    /// </summary>
    public void ItemUnload()
    {
        if (this.IsEquip == false) return;

        if (GetItemType() == ItemType.Artifact)
        {
            Player.Instance.EquipmentUnload(this);
        }
        else if (GetItemType() == ItemType.Potion)
        {
            Player.Instance.PotionItemUnload(this, this.slotNumber);
        }
    }

    /// <summary>
    /// ���â���� �Ҹ�ǰ �̵�
    /// </summary>
    /// <param name="changSlotNumber">�ٲ� ���� �ڸ� ����</param>
    public void ItemMove(int changSlotNumber)
    {
        Player.Instance.PotionItemMove(this, this.slotNumber, changSlotNumber);
    }

    /// <summary>
    /// ���â���� �Ҹ�ǰ ��ȯ
    /// </summary>
    /// <param name="changSlotNumber">�ٲ� ���� �ڸ� ����</param>
    public void ItemChange(int changSlotNumber)
    {
        Player.Instance.PotionItemChange(this.slotNumber, changSlotNumber);
    }

    /// <summary>
    /// �巡�� ���۽�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        UISFX.Instance.Play(UISFX.Instance.itemDrag);

        this.parentAfterDrag = this.transform.parent;
        this.transform.SetParent(InventorySystem.Instance.transform);
        this.transform.SetAsLastSibling();
        this.plate.raycastTarget = false;

        PlayerUI.Instance.ItemOnBeginDrag(this);
    }

    /// <summary>
    /// �巡�� ��
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = currentPosition;
    }

    /// <summary>
    /// �巡�� ������
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
