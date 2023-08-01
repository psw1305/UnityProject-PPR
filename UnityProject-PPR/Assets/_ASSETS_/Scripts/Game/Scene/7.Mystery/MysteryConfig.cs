using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "Blueprint/Event")]
public class MysteryConfig : ScriptableObject
{
    [Header("Event Settings")]
    [SerializeField] private GameObject[] selections;
    [SerializeField] private Sprite eventPicture;
    [SerializeField] private string stringTable;

    public GameObject[] Selections => this.selections;
    public Sprite EventPicture => this.eventPicture;
    public string StringTable => this.stringTable;
}
