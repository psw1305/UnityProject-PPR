using PSW.Core.Extensions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattlePlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthText;

    private void OnEnable()
    {
        GameBoardEvents.OnPlayerHealthChanged.AddListener(OnHealthPointChanged);
    }

    private void OnDisable()
    {
        GameBoardEvents.OnPlayerHealthChanged.RemoveListener(OnHealthPointChanged);
    }

    public void SetText(int value, int maxValue)
    {
        this.healthText.text = value + " / " + maxValue;
        this.healthBar.fillAmount = value / (float)maxValue;
    }

    private void OnHealthPointChanged(int oldPoint, int newPoint)
    {
        // health text 카운팅 효과
        string addHealthText = " / " + BattlePlayer.Instance.HP;
        StartCoroutine(this.healthText.UpdateTextCoroutine(oldPoint, newPoint, 1.0f, addHealthText));

        // health bar 이미지 fillAmount 체력 비례 조정
        this.healthBar.fillAmount = BattlePlayer.Instance.GetPercentHP();
    }
}
