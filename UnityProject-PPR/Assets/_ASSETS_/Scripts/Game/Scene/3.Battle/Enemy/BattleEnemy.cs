using PSW.Core.Enums;
using PSW.Core.Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleEnemy : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Button selected;
    [SerializeField] private Image enemyImage;
    [SerializeField] private TextMeshProUGUI enemyName;
    [SerializeField] private RectTransform enemyTable;
    
    [Header("Skill")]
    [SerializeField] private GameObject[] skills;
    [SerializeField] private Transform skillslot;

    [Header("Particle")]
    [SerializeField] private ParticleSystem weakWreckParticle;
    [SerializeField] private ParticleSystem wreckParticle;

    [Header("Scripts")]
    [SerializeField] private BattleEnemyUI enemyUI;

    private BattleSystem battleSystem;
    private BattleEnemySkill currentEnemySkill;

    public int MaxHP { get; set; }
    public int CurrentHP { get; set; }
    public int CurrentAP { get; set; }
    public int CurrentSP { get; set; }

    public float GetPercentHP() => this.CurrentHP / (float)this.MaxHP;

    /// <summary>
    /// ������ => EnemyBlueprint�� ������� ����
    /// </summary>
    /// <param name="enemyBlueprint"></param>
    public void Set(BattleSystem battleSystem, EnemyBlueprint enemyBlueprint)
    {
        this.battleSystem = battleSystem;

        this.enemyImage.sprite = enemyBlueprint.EnemyImage;
        this.enemyName.text = enemyBlueprint.EnemyName;
        this.MaxHP = enemyBlueprint.HP;
        this.skills = enemyBlueprint.Skills;

        this.CurrentHP = this.MaxHP;
        this.CurrentAP = 0;
        this.CurrentSP = 0;

        this.enemyUI.SetHPText();
    }

    private void OnEnable()
    {
        this.selected.onClick.AddListener(Selected);
    }

    /// <summary>
    /// ���ý� �� Ÿ�� ����
    /// </summary>
    public void Selected()
    {
        this.battleSystem.SelectedEnemy = this;
    }

    /// <summary>
    /// �� ���� ����
    /// </summary>
    public void ShieldPoint(int shield)
    {
        Debug.Log(shield);

        this.CurrentSP = shield;
        this.enemyUI.ShieldOn();
    }

    /// <summary>
    /// �� ����
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage)
    {
        DamagedEffect(damage);

        // �� ������ 0 �ʰ� �ΰ�?
        if (this.CurrentSP > 0) ShieldDamaged(damage);
        // �� ������ 0 ���� �̸� health damaged
        else HealthDamaged(damage);
    }

    /// <summary>
    /// ������ ���� �� => RectTransform ��鸲 ȿ��
    /// </summary>
    /// <param name="damage"></param>
    private void DamagedEffect(int damage)
    {
        if (damage < 50)
            StartCoroutine(this.enemyTable.ShakeCoroutine(2, 8, this.weakWreckParticle));
        else
            StartCoroutine(this.enemyTable.ShakeCoroutine(5, 15, this.wreckParticle));
    }

    /// <summary>
    /// �� ü�� ���� ���
    /// </summary>
    /// <param name="damage"></param>
    private void HealthDamaged(int damage)
    {
        if (damage <= 0) return;

        var damagedHp = this.CurrentHP;
        damagedHp -= damage;
        
        // �� ���
        if (damagedHp <= 0)
        {
            this.CurrentHP = 0;
            this.enemyUI.DeadText();

            this.battleSystem.BattlePlay = BattleType.EnemyDead;

            // ���� ����
            return;
        }

        this.CurrentHP = damagedHp;
        this.enemyUI.SetHPText();
    }

    /// <summary>
    /// �� ���� ���� ���
    /// </summary>
    /// <param name="damage"></param>
    private void ShieldDamaged(int damage)
    {
        if (damage <= 0) return;

        var damagedShield = this.CurrentSP;
        damagedShield -= damage;
        
        if (damagedShield <= 0)
        {
            this.CurrentSP = 0;
            this.enemyUI.ShieldOff();
            
            var remainDamage = damagedShield * -1;
            HealthDamaged(remainDamage);

            return;
        }

        this.CurrentSP = damagedShield;
        this.enemyUI.SetSPText();
    }

    /// <summary>
    /// �� ��ų UI ����
    /// </summary>
    public void EnemySkillInstance()
    {
        if (this.skills.Length == 0) return;

        var random = Random.Range(0, this.skills.Length);
        var clone = Instantiate(this.skills[random], this.skillslot);
        this.currentEnemySkill = clone.GetComponent<BattleEnemySkill>();
        this.currentEnemySkill.Enable(this);
    }

    /// <summary>
    /// �� ��ų ���
    /// </summary>
    /// <returns></returns>
    public IEnumerator EnemyUseSkill()
    {
        if (this.currentEnemySkill == null) yield return null;

        // ��ų ���
        this.currentEnemySkill.Use(this);
        yield return new WaitForSeconds(0.3f);
        // ��ų ��� �� �ı�
        this.currentEnemySkill.Disable();
        yield return new WaitForSeconds(0.3f);
    }

    /// <summary>
    /// �÷��̾� �ʱ�ȭ
    /// </summary>
    public void Init()
    {
        // ���� �ʱ�ȭ
        this.CurrentSP = 0;
        this.enemyUI.ShieldOff();
    }
}
