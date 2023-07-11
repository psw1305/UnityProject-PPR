using PSW.Core.Extensions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BattleEnemyUI : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Shield")]
    [SerializeField] private TextMeshProUGUI shieldText;
    [SerializeField] private CanvasGroup shieldCanvas;

    private BattleEnemy battleEnemy;

    private void Awake()
    {
        this.battleEnemy = GetComponent<BattleEnemy>();
    }

    public void SetHPText()
    {
        this.healthText.text = this.battleEnemy.CurrentHP + "/" + this.battleEnemy.MaxHP;
        this.healthBar.fillAmount = this.battleEnemy.GetPercentHP();
    }

    public void SetSPText()
    {
        this.shieldText.text = this.battleEnemy.CurrentSP.ToString();
    }

    /// <summary>
    /// ����� UI ǥ��
    /// </summary>
    public void DeadText()
    {
        this.healthText.text = "����";
        this.healthBar.fillAmount = 0;
    }

    /// <summary>
    /// ���� ���� �� UI �ִϸ��̼� �� shield text ����
    /// </summary>
    public void ShieldOn()
    {
        this.shieldCanvas
            .DOFade(1, 0.25f)
            .OnStart(() => SetSPText());
    }

    /// <summary>
    /// ���� ���� �� UI �ִϸ��̼� �� shield text ����
    /// </summary>
    public void ShieldOff()
    {
        this.shieldCanvas
            .DOFade(0, 0.25f)
            .OnStart(() => SetSPText());
    }
}
