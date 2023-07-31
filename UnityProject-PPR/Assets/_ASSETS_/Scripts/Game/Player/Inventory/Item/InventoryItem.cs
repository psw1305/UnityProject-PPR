using PSW.Core.Enums;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public bool IsEquip { get; set; }

    [SerializeField] private ItemType itemType;
    [SerializeField] private Button button;
    [SerializeField] private Image image;
    protected ItemBlueprint blueprint;

    /// <summary>
    /// �������� �߰��Ͽ� �����ϰ� UI�� ǥ��
    /// </summary>
    /// <param name="blueprint">������ ������</param>
    public virtual void Set(ItemBlueprint blueprint)
    {
        this.IsEquip = false;
        this.blueprint = blueprint;
        this.name = blueprint.name;
        this.image.sprite = blueprint.ItemImage;

        this.button.onClick.AddListener(ItemTooltipShow);
    }

    public ItemBlueprint GetItemData()
    {
        return this.blueprint;
    }

    public ItemType GetItemType()
    {
        return this.blueprint.ItemType;
    }

    /// <summary>
    /// ������ Ŭ���� ���� ǥ��
    /// </summary>
    protected virtual void ItemTooltipShow() { }
}
