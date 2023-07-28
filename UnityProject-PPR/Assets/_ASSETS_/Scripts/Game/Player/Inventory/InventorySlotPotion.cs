using UnityEngine;
using DG.Tweening;

public class InventorySlotPotion : MonoBehaviour
{
    [SerializeField] private int slotNumber;
    [SerializeField] private Transform dropSlot;
    [SerializeField] private RectTransform plate;

    public Transform GetDropSlot()
    {
        return this.dropSlot;
    }

    private void ClickPotion()
    {

    }

    private void Use()
    {

    }

    private void Discard()
    {

    }
}
