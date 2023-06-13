using PSW.Core.Extensions;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class EnemyShieldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI shieldText;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        this.canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        GameBoardEvents.OnEnemyShieldChanged.AddListener(OnShieldPointChanged);
    }

    private void OnDisable()
    {
        GameBoardEvents.OnEnemyShieldChanged.RemoveListener(OnShieldPointChanged);
    }

    private void OnShieldPointChanged(int oldPoint, int newPoint)
    {
        if (oldPoint == 0) canvasGroup.DOFade(1, 0.25f);

        StartCoroutine(this.shieldText.UpdateTextCoroutine(oldPoint, newPoint, 1.0f));

        if (newPoint == 0) canvasGroup.DOFade(0, 0.25f);
    }
}
