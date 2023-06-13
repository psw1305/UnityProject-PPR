using PSW.Core.Extensions;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class BattlePlayerShieldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI shieldText = null;
    private CanvasGroup canvasGroup = null;

    private void Awake()
    {
        this.canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        GameBoardEvents.OnPlayerShieldChanged.AddListener(OnShieldPointChanged);
    }

    private void OnDisable()
    {
        GameBoardEvents.OnPlayerShieldChanged.RemoveListener(OnShieldPointChanged);
    }

    private void OnShieldPointChanged(int oldPoint, int newPoint)
    {
        if (oldPoint == 0) canvasGroup.DOFade(1, 0.25f);

        StartCoroutine(this.shieldText.UpdateTextCoroutine(oldPoint, newPoint, 1.0f));

        if (newPoint == 0) canvasGroup.DOFade(0, 0.25f);
    }
}
