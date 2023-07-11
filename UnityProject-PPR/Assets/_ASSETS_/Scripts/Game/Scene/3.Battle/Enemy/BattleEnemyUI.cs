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
    /// 사망시 UI 표시
    /// </summary>
    public void DeadText()
    {
        this.healthText.text = "죽음";
        this.healthBar.fillAmount = 0;
    }

    /// <summary>
    /// 쉴드 생성 시 UI 애니메이션 및 shield text 세팅
    /// </summary>
    public void ShieldOn()
    {
        this.shieldCanvas
            .DOFade(1, 0.25f)
            .OnStart(() => SetSPText());
    }

    /// <summary>
    /// 쉴드 소진 시 UI 애니메이션 및 shield text 세팅
    /// </summary>
    public void ShieldOff()
    {
        this.shieldCanvas
            .DOFade(0, 0.25f)
            .OnStart(() => SetSPText());
    }
}
