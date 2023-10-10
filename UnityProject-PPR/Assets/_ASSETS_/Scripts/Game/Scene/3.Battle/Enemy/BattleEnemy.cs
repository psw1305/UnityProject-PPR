using PSW.Core.Enums;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleEnemy : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Toggle toggle;
    [SerializeField] private Image enemyImage;
    [SerializeField] private TextMeshProUGUI enemyName;
    
    [Header("Skill")]
    [SerializeField] private GameObject[] skills;
    [SerializeField] private Transform skillslot;

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
    /// Enemy Blueprint ������� ����
    /// </summary>
    /// <param name="enemyBlueprint"></param>
    public void Set(BattleSystem battleSystem, ToggleGroup toggleGroup, EnemyBlueprint enemyBlueprint)
    {
        this.battleSystem = battleSystem;
        this.toggle.group = toggleGroup;

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
        this.toggle.onValueChanged.AddListener(Targeting);
    }

    /// <summary>
    /// ���ý� �� Ÿ�� ����
    /// </summary>
    private void Targeting(bool isOn)
    {
        if (isOn)
            this.battleSystem.TargetEnemy = this;
        else
            this.battleSystem.TargetEnemy = null;
    }

    /// <summary>
    /// �� ���� ����
    /// </summary>
    public void ShieldPoint(int shield)
    {
        this.CurrentSP = shield;
        this.enemyUI.ShieldOn();
    }

    /// <summary>
    /// �� ����
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage)
    {
        this.enemyUI.TakeDamageEffect(damage);

        // �� ������ 0 �ʰ� �ΰ�?
        if (this.CurrentSP > 0) ShieldDamaged(damage);
        // �� ������ 0 ���� �̸� health damaged
        else HealthDamaged(damage);
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
            this.enemyUI.Dead();
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
    public void EnemySkillInstance(GameBoard gameBoard)
    {
        if (this.skills.Length == 0) return;

        var random = Random.Range(0, this.skills.Length);
        var clone = Instantiate(this.skills[random], this.skillslot);
        this.currentEnemySkill = clone.GetComponent<BattleEnemySkill>();
        this.currentEnemySkill.Set(this, gameBoard);
    }

    /// <summary>
    /// �� ��ų ���
    /// </summary>
    /// <returns></returns>
    public IEnumerator EnemyUseSkill()
    {
        if (this.currentEnemySkill == null) yield return null;
    
        yield return StartCoroutine(this.currentEnemySkill.UseSkill());
    }

    /// <summary>
    /// �� ���� üũ
    /// </summary>
    /// <returns></returns>
    public void EnemyCheckState()
    {
        if (this.currentEnemySkill == null) return;

        this.currentEnemySkill.CheckSkill();
    }

    public void EnemyDead()
    {
        this.battleSystem.BattleEnemys.Remove(this);

        if (this.battleSystem.BattleEnemys.Count == 0)
            this.battleSystem.BattleCheck(BattlePlay.EnemyAllDead);
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
