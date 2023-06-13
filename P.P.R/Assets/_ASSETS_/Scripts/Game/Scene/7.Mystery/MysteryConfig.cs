using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "Blueprint/Event")]
public class MysteryConfig : ScriptableObject
{
    [Header("Event Selection")]
    public List<MysterySelection> eventSelections;

    [Header("Event Picture")]
    public Sprite eventPicture;

    [Header("Locale")]
    public string stringTable;
}
