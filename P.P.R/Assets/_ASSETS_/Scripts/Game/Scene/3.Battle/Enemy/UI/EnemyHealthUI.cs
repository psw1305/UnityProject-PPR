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
    /// ����� UI ǥ��
    /// </summary>
    public void DeadUI()
    {
        this.healthText.text = "����";
        this.healthBar.fillAmount = 0;
    }

    private void OnHealthPointChanged(int oldPoint, int newPoint)
    {
        // health text ī���� ȿ��
        string addHealthText = " / " + this.battleEnemy.MaxHP;
        StartCoroutine(this.healthText.UpdateTextCoroutine(oldPoint, newPoint, 1.0f, addHealthText));

        // health bar �̹��� fillAmount ü�� ��� ����
        this.healthBar.fillAmount = this.battleEnemy.GetPercentHP();
    }
}
