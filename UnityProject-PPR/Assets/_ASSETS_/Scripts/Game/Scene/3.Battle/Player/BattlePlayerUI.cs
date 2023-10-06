using PSW.Core.Extensions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BattlePlayerUI : MonoBehaviour
{
    [Header("Table")]
    [SerializeField] private RectTransform statTable;
    [SerializeField] private RectTransform playerTable;

    [Header("Health")]
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Shield")]
    [SerializeField] private TextMeshProUGUI shieldText;
    [SerializeField] private CanvasGroup shieldCanvas;

    [Header("Stat")]
    [SerializeField] private TextMeshProUGUI actText;
    [SerializeField] private TextMeshProUGUI deckText;

    [Header("Damage Text")]
    [SerializeField] private PoolableObject damageTextPrefab;
    [SerializeField] private Transform damageTextParent;

    [Header("Particle")]
    [SerializeField] private ParticleSystem weakWreckParticle;
    [SerializeField] private ParticleSystem wreckParticle;

    private BattlePlayer battlePlayer;
    private ObjectPool damageTextPool;


    private void Awake()
    {
        this.battlePlayer = GetComponent<BattlePlayer>();
        this.damageTextPool = ObjectPool.CreateInstance(this.damageTextPrefab, this.damageTextParent, 50);
    }

    private void OnEnable()
    {
        GameBoardEvents.OnPlayerHealthChanged.AddListener(OnHealthPointChanged);
        GameBoardEvents.OnPlayerShieldChanged.AddListener(OnShieldPointChanged);
    }

    private void OnDisable()
    {
        GameBoardEvents.OnPlayerHealthChanged.RemoveListener(OnHealthPointChanged);
        GameBoardEvents.OnPlayerShieldChanged.RemoveListener(OnShieldPointChanged);
    }

    public void SetHpText(int value, int maxValue)
    {
        this.healthText.text = value + "/" + maxValue;
        this.healthBar.fillAmount = value / (float)maxValue;
    }

    public void SetStatText()
    {
        this.actText.text = "0";
        this.deckText.text = "0";
    }

    public void SetActText(int act)
    {
        this.actText.text = act.ToString();
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
            StartCoroutine(this.statTable.ShakeCoroutine(2, 8, this.weakWreckParticle));
        else
            StartCoroutine(this.playerTable.ShakeCoroutine(5, 15, this.wreckParticle));
    }
    #endregion

    public void UpdateAnimateUI(int oldPoint, int newPoint)
    {
        StartCoroutine(this.actText.UpdateTextCoroutine(oldPoint, newPoint, 1.0f));
    }

    private void OnHealthPointChanged(int oldPoint, int newPoint)
    {
        // health text 카운팅 효과
        string addHealthText = "/" + this.battlePlayer.HP;
        StartCoroutine(this.healthText.UpdateTextCoroutine(oldPoint, newPoint, 1.0f, addHealthText));

        // health bar 이미지 fillAmount 체력 비례 조정
        this.healthBar.fillAmount = this.battlePlayer.GetPercentHP();
    }

    private void OnShieldPointChanged(int oldPoint, int newPoint)
    {
        if (oldPoint == 0) this.shieldCanvas.DOFade(1, 0.25f);

        StartCoroutine(this.shieldText.UpdateTextCoroutine(oldPoint, newPoint, 1.0f));

        if (newPoint == 0) this.shieldCanvas.DOFade(0, 0.25f);
    }

    /// <summary>
    /// 사망시 UI 표시
    /// </summary>
    public void Dead()
    {
        this.healthText.text = "죽음";
        this.healthBar.fillAmount = 0;
    }
}
