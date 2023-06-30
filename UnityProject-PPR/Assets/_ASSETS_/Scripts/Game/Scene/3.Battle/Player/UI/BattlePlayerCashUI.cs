using PSW.Core.Extensions;
using UnityEngine;
using TMPro;

public class BattlePlayerCashUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cashText;

    private void Start()
    {
        this.cashText.text = BattlePlayer.Instance.EarnCash.ToString();
    }

    private void OnEnable()
    {
        GameBoardEvents.OnPlayerCashChanged.AddListener(OnPlayerCoinChanged);
    }

    private void OnDisable()
    {
        GameBoardEvents.OnPlayerCashChanged.RemoveListener(OnPlayerCoinChanged);
    }

    private void OnPlayerCoinChanged(int oldPoint, int newPoint)
    {
        StopAllCoroutines();
        StartCoroutine(this.cashText.UpdateTextCoroutine(oldPoint, newPoint, 1.0f));
    }
}
