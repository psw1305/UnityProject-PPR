using PSW.Core.Extensions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealthUI : MonoBehaviour
{

    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private BattleEnemy battleEnemy;

    private void OnEnable()
    {
        GameBoardEvents.OnEnemyHealthChanged.AddListener(OnHealthPointChanged);
    }

    private void OnDisable()
    {
        GameBoardEvents.OnEnemyHealthChanged.RemoveListener(OnHealthPointChanged);
    }

    public void SetText(int value, int maxValue)
    {
        this.healthText.text = value + " / " + maxValue;
        this.healthBar.fillAmount = value / (float)maxValue;
    }

    /// <summary>
    /// 사망시 UI 표시
    /// </summary>
    public void DeadUI()
    {
        this.healthText.text = "죽음";
        this.healthBar.fillAmount = 0;
    }

    private void OnHealthPointChanged(int oldPoint, int newPoint)
    {
        // health text 카운팅 효과
        string addHealthText = " / " + this.battleEnemy.MaxHP;
        StartCoroutine(this.healthText.UpdateTextCoroutine(oldPoint, newPoint, 1.0f, addHealthText));

        // health bar 이미지 fillAmount 체력 비례 조정
        this.healthBar.fillAmount = this.battleEnemy.GetPercentHP();
    }
}
