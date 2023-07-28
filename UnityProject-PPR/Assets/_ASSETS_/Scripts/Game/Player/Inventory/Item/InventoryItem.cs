using PSW.Core.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public bool IsEquip { get; set; }

    [SerializeField] private ItemType itemType;
    [SerializeField] private Button button;
    [SerializeField] private Image image;
    protected ItemBlueprint itemData;

    /// <summary>
    /// �������� �߰��Ͽ� �����ϰ� UI�� ǥ��
    /// </summary>
    /// <param name="data">������ ������</param>
    public virtual void Set(ItemBlueprint data)
    {
        this.IsEquip = false;
        this.itemData = data;
        this.image.sprite = data.ItemImage;
        this.button.onClick.AddListener(ItemTooltipShow);

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

    /// <summary>
    /// ������ Ŭ���� ���� ǥ��
    /// </summary>
    protected virtual void ItemTooltipShow() { }
}
