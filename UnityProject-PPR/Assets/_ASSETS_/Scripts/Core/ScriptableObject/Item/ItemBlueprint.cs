using PSW.Core.Enums;
using UnityEngine;

public class ItemBlueprint : ScriptableObject
{
    [SerializeField] private Sprite image;
    [SerializeField] private ItemType itemType;
    [SerializeField] private ItemGrade itemGrade;
    [SerializeField] private string itemName;
    [SerializeField] private string itemAbility;
    [SerializeField] private string itemDesc;

    public Sprite ItemImage => this.image;
    public ItemType ItemType => this.itemType;
    public ItemGrade ItemGrade => this.itemGrade;
    public string ItemName => this.itemName;
    public string ItemAbility => this.itemAbility;
    public string ItemDesc => this.itemDesc;
}
