using PSW.Core.Extensions;
using UnityEngine;
using TMPro;

public class BattlePlayerAttackUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI attackText = null;

    private void OnEnable()
    {
        GameBoardEvents.OnPlayerAttackChanged.AddListener(OnAttackPointChanged);
    }

    private void OnDisable()
    {
        GameBoardEvents.OnPlayerAttackChanged.RemoveListener(OnAttackPointChanged);
    }

    private void OnAttackPointChanged(int oldPoint, int newPoint)
    {
        StartCoroutine(this.attackText.UpdateTextCoroutine(oldPoint, newPoint, 1.0f));
    }
}
