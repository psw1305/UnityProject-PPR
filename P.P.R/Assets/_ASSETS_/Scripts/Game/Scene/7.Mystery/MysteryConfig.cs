using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "Blueprint/Event")]
public class MysteryConfig : ScriptableObject
{
    [Header("Event Settings")]
    public GameObject[] selections;
    public Sprite eventPicture;
    public string stringTable;
}
