using PSW.Core.Enums;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [Header("Item")]
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
        this.blueprint = blueprint;
        this.name = blueprint.name;
        this.image.sprite = blueprint.ItemImage;

        this.button.onClick.AddListener(ItemTooltipShow);
    }

    public ItemBlueprint GetBlueprint()
    {
        return this.blueprint;
    }

    /// <summary>
    /// ������ Ŭ���� ���� ǥ��
    /// </summary>
    protected virtual void ItemTooltipShow() { }
}
