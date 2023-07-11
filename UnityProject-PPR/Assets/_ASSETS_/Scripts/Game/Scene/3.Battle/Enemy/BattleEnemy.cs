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
    /// 생성자 => EnemyBlueprint에 기반으로 세팅
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
    /// 선택시 적 타겟 설정
    /// </summary>
    public void Selected()
    {
        this.battleSystem.SelectedEnemy = this;
    }

    /// <summary>
    /// 적 방어력 생성
    /// </summary>
    public void ShieldPoint(int shield)
    {
        Debug.Log(shield);

        this.CurrentSP = shield;
        this.enemyUI.ShieldOn();
    }

    /// <summary>
    /// 적 피해
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage)
    {
        DamagedEffect(damage);

        // 현 방어력이 0 초과 인가?
        if (this.CurrentSP > 0) ShieldDamaged(damage);
        // 현 방어력이 0 이하 이면 health damaged
        else HealthDamaged(damage);
    }

    /// <summary>
    /// 데미지 받을 시 => RectTransform 흔들림 효과
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
    /// 적 체력 피해 계산
    /// </summary>
    /// <param name="damage"></param>
    private void HealthDamaged(int damage)
    {
        if (damage <= 0) return;

        var damagedHp = this.CurrentHP;
        damagedHp -= damage;
        
        // 적 사망
        if (damagedHp <= 0)
        {
            this.CurrentHP = 0;
            this.enemyUI.DeadText();

            this.battleSystem.BattlePlay = BattleType.EnemyDead;

            // 전투 종료
            return;
        }

        this.CurrentHP = damagedHp;
        this.enemyUI.SetHPText();
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

        // 스킬 사용
        this.currentEnemySkill.Use(this);
        yield return new WaitForSeconds(0.3f);
        // 스킬 사용 후 파괴
        this.currentEnemySkill.Disable();
        yield return new WaitForSeconds(0.3f);
    }

    /// <summary>
    /// 플레이어 초기화
    /// </summary>
    public void Init()
    {
        // 쉴드 초기화
        this.CurrentSP = 0;
        this.enemyUI.ShieldOff();
    }
}
