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
    /// 아이템을 추가하여 장착하고 UI로 표시
    /// </summary>
    /// <param name="data">아이템 데이터</param>
    public virtual void Set(ItemBlueprint data)
    {
        this.IsEquip = false;
        this.itemData = data;
        this.image.sprite = data.ItemImage;
        this.button.onClick.AddListener(ItemTooltipShow);

        // 프리팹 네임 => 데이터 네임으로 변경
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
    /// 아이템 클릭시 툴팁 표시
    /// </summary>
    protected virtual void ItemTooltipShow() { }
}
