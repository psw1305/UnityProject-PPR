using UnityEngine;

public class ItemRandomizer : MonoBehaviour
{
    [SerializeField] private ItemBlueprint[] commonList;
    [SerializeField] private ItemBlueprint[] uncommonList;
    [SerializeField] private ItemBlueprint[] rareList;

    private void ItemRandomGenerator()
    {
        int typeIndex = Random.Range(0, commonList.Length);
    }
}
