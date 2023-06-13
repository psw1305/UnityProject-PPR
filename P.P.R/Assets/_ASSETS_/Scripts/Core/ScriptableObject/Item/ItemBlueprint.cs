using PSW.Core.Enums;
using UnityEngine;

public class ItemBlueprint : ScriptableObject
{
    [SerializeField] private Sprite image;
    [SerializeField] private ItemType itemType;
    [SerializeField] private ItemRare itemRareType;
    [SerializeField] private string itemName;
    [SerializeField] private string itemDesc;

    public Sprite ItemImage => this.image;
    public ItemType ItemType => this.itemType;
    public ItemRare ItemRareType => this.itemRareType;
    public string ItemName => this.itemName;
    public string ItemContents => this.itemDesc;
}
