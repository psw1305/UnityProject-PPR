using PSW.Core.Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BattleEnemyUI : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private CanvasGroup enemyCanvas;
    [SerializeField] private RectTransform enemyTable;

    [Header("Health")]
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Shield")]
    [SerializeField] private TextMeshProUGUI shieldText;
    [SerializeField] private CanvasGroup shieldCanvas;

    [Header("Damage Text")]
    [SerializeField] private PoolableObject damageTextPrefab;
    [SerializeField] private Transform damageTextParent;

    [Header("Particle")]
    [SerializeField] private ParticleSystem weakWreckParticle;
    [SerializeField] private ParticleSystem wreckParticle;

    private BattleEnemy battleEnemy;
    private ObjectPool damageTextPool;

    private void Awake()
    {
        this.battleEnemy = GetComponent<BattleEnemy>();
        this.damageTextPool = ObjectPool.CreateInstance(this.damageTextPrefab, this.damageTextParent, 10);
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

    #region Take Damage
    private void FloatingDamageText(int damage)
    {
        var poolableObject = this.damageTextPool.GetObject();

        if (poolableObject != null)
        {
            var damageText = poolableObject.GetComponent<TextMeshProUGUI>();
            damageText.FloatingDamageText(damage.ToString());
        }
    }

    /// <summary>
    /// 데미지 받을 시 => RectTransform 흔들림 효과
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamageEffect(int damage)
    {
        FloatingDamageText(damage);

        if (damage < 50)
            StartCoroutine(this.enemyTable.ShakeCoroutine(2, 8, this.weakWreckParticle));
        else
            StartCoroutine(this.enemyTable.ShakeCoroutine(5, 15, this.wreckParticle));
    }
    #endregion

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

    /// <summary>
    /// 사망시 UI 표시
    /// </summary>
    public void Dead()
    {
        this.healthText.text = "죽음";
        this.healthBar.fillAmount = 0;
        this.battleEnemy.EnemyDead();

        StartCoroutine(DeadCoroutine());
    }

    private IEnumerator DeadCoroutine()
    {
        yield return YieldCache.WaitForSeconds(0.25f);

        this.enemyCanvas
            .DOFade(0, 1.0f)
            .OnComplete(() =>
            {
                Destroy(this.gameObject);
            });
    }
}
