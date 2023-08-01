using PSW.Core.Enums;
using UnityEngine;

public class ItemBlueprint : ScriptableObject
{
    [SerializeField] private Sprite image;
    [SerializeField] private ItemType itemType;
    [SerializeField] private ItemGrade itemGrade;
    [SerializeField] private string itemName;

    public Sprite ItemImage => this.image;
    public ItemType ItemType => this.itemType;
    public ItemGrade ItemGrade => this.itemGrade;
    public string ItemName => this.itemName;
}
