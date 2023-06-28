using PSW.Core.Extensions;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using PSW.Core.Enums;

public class BattleEnemy : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Image enemyImage;
    [SerializeField] private TextMeshProUGUI enemyName;
    [SerializeField] private RectTransform enemyTable;
    
    [Header("Skill")]
    [SerializeField] private GameObject[] skills;
    [SerializeField] private Transform skillslot;
    private BattleEnemySkill currentEnemySkill;

    [Header("Particle")]
    [SerializeField] private ParticleSystem weakWreckParticle;
    [SerializeField] private ParticleSystem wreckParticle;

    [Header("Scripts")]
    [SerializeField] private EnemyHealthUI healthUI;
    [SerializeField] private EnemyShieldUI shieldUI;

    public int MaxHP { get; set; }
    public int CurrentHP { get; set; }
    public int CurrentAP { get; set; }
    public int CurrentSP { get; set; }

    public float GetPercentHP() => this.CurrentHP / (float)this.MaxHP;

    /// <summary>
    /// 적 설계도에 기반하여 세팅
    /// </summary>
    /// <param name="enemyBlueprint"></param>
    public void Set(EnemyBlueprint enemyBlueprint)
    {
        this.enemyImage.sprite = enemyBlueprint.EnemyImage;
        this.enemyName.text = enemyBlueprint.EnemyName;
        this.MaxHP = enemyBlueprint.HP;
        this.skills = enemyBlueprint.Skills;

        this.CurrentHP = this.MaxHP;
        this.CurrentAP = 0;
        this.CurrentSP = 0;

        this.healthUI.SetText(this.CurrentHP, this.MaxHP);
    }

    /// <summary>
    /// 적 방어력
    /// </summary>
    public void ShieldPoint(int shield)
    {
        this.CurrentSP = shield;
        GameBoardEvents.OnEnemyShieldChanged.Invoke(0, this.CurrentSP);
    }

    /// <summary>
    /// 적 피해
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage)
    {
        if (damage < 50)
            DamageEffect(2, 8, this.weakWreckParticle);
        else 
            DamageEffect(5, 15, this.wreckParticle);

        // 현 방어력이 0 초과 인가?
        if (this.CurrentSP > 0) 
            ShieldDamaged(damage);
        // 현 방어력이 0 이하 이면 health damaged
        else 
            HealthDamaged(damage);
    }

    /// <summary>
    /// 적 체력 피해 계산
    /// </summary>
    /// <param name="damage"></param>
    private void HealthDamaged(int damage)
    {
        if (damage <= 0) return;
        var damagedHp = this.CurrentHP - damage;
        
        // 적 사망
        if (damagedHp <= 0)
        {
            this.healthUI.DeadUI();
            BattleSystem.Instance.BattlePlay = BattleType.EnemyDead;

            // 전투 종료
            return;
        }

        this.CurrentHP = damagedHp;
        GameBoardEvents.OnEnemyHealthChanged.Invoke(this.CurrentHP, damagedHp);
    }

    /// <summary>
    /// 적 방어력 피해 계산
    /// </summary>
    /// <param name="damage"></param>
    private void ShieldDamaged(int damage)
    {
        if (damage <= 0) return;

        var damagedShield = this.CurrentSP;
        damagedShield -= damage;
        
        if (damagedShield <= 0)
        {
            int remainDamage = damagedShield * -1;
            HealthDamaged(remainDamage);
            damagedShield = 0;
        }

        this.CurrentSP = damagedShield;
        GameBoardEvents.OnEnemyShieldChanged.Invoke(this.CurrentSP, damagedShield);
    }

    /// <summary>
    /// 적 스킬 UI 생성
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
    /// 적 스킬 사용
    /// </summary>
    /// <returns></returns>
    public IEnumerator EnemyUseSkill()
    {
        if (this.currentEnemySkill == null) yield return null;

        this.currentEnemySkill.Use(this);
        yield return new WaitForSeconds(0.5f);
    }

    /// <summary>
    /// 적 스킬 UI 생성
    /// </summary>
    public IEnumerator EnemySkillDestroy()
    {
        if (this.currentEnemySkill == null) yield return null;

        this.currentEnemySkill.Disable();
        yield return new WaitForSeconds(0.5f);
    }

    /// <summary>
    /// 데미지를 받을 경우, Transform 흔들림 효과
    /// </summary>
    public void DamageEffect(float force, float amount, ParticleSystem wreck)
    {
        StartCoroutine(this.enemyTable.ShakeCoroutine(force, amount));
        wreck.Play();
    }
}
