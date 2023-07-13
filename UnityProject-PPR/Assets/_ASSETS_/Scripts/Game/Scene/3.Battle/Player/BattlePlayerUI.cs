using PSW.Core.Extensions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BattlePlayerUI : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Shield")]
    [SerializeField] private TextMeshProUGUI shieldText;
    [SerializeField] private CanvasGroup shieldCanvas;

    [Header("Stat")]
    [SerializeField] private TextMeshProUGUI actText;
    [SerializeField] private TextMeshProUGUI atkText;
    [SerializeField] private TextMeshProUGUI defText;

    private BattlePlayer battlePlayer;

    private void Awake()
    {
        this.battlePlayer = GetComponent<BattlePlayer>();
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
        this.healthText.text = value + " / " + maxValue;
        this.healthBar.fillAmount = value / (float)maxValue;
    }

    public void SetStatText(int act, int atk, int def)
    {
        this.actText.text = act.ToString();
        this.atkText.text = atk.ToString();
        this.defText.text = def.ToString();
    }

    public void SetActText(int act)
    {
        this.actText.text = act.ToString();
    }

    public void UpdateAnimateUI(int oldPoint, int newPoint)
    {
        StartCoroutine(this.actText.UpdateTextCoroutine(oldPoint, newPoint, 1.0f));
    }

    private void OnHealthPointChanged(int oldPoint, int newPoint)
    {
        // health text ī���� ȿ��
        string addHealthText = " / " + this.battlePlayer.HP;
        StartCoroutine(this.healthText.UpdateTextCoroutine(oldPoint, newPoint, 1.0f, addHealthText));

        // health bar �̹��� fillAmount ü�� ��� ����
        this.healthBar.fillAmount = this.battlePlayer.GetPercentHP();
    }

    private void OnShieldPointChanged(int oldPoint, int newPoint)
    {
        if (oldPoint == 0) this.shieldCanvas.DOFade(1, 0.25f);

        StartCoroutine(this.shieldText.UpdateTextCoroutine(oldPoint, newPoint, 1.0f));

        if (newPoint == 0) this.shieldCanvas.DOFade(0, 0.25f);
    }

    /// <summary>
    /// ����� UI ǥ��
    /// </summary>
    public void Dead()
    {
        this.healthText.text = "����";
        this.healthBar.fillAmount = 0;
    }
}
