using PSW.Core.Enums;
using PSW.Core.Extensions;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer : BehaviourSingleton<BattlePlayer>
{
    [Header("Settings")]
    [SerializeField] private RectTransform playerTable;
    [SerializeField] private RectTransform statTable;

    [Header("Potion")]
    [SerializeField] private Transform[] potionSlots;

    [Header("Particle")]
    [SerializeField] private ParticleSystem weakWreckParticle;
    [SerializeField] private ParticleSystem wreckParticle;

    [Header("Scripts")]
    [SerializeField] private BattleSystem battleSystem;
    [SerializeField] private BattlePlayerUI battlePlayerUI;

    public int HP { get; set; }
    public int ACT { get; set; }

    // 타일 하나당 주어지는 일반 포인트
    public int ATK { get; set; }
    public int DEF { get; set; }

    // 첫 공격 시 주어지는 시작 포인트
    public int FirstATK { get; set; }
    public int FirstDEF { get; set; }

    public bool OnStart { get; private set; }
    public bool OnFirst { get; private set; }

    public int CurrentHP { get; private set; } // 현 체력
    public int CurrentSP { get; private set; } // 현 방어력
    public int CurrentACT { get; private set; } // 현 행동력

    public float GetPercentHP() => CurrentHP / (float)this.HP;
    private static Player player;

    protected override void Awake()
    {
        base.Awake();

        player = Player.Instance;

        this.OnStart = true;

        if (player != null)
        {
            this.HP = player.HP.Value;
            this.ACT = player.ACT.Value;
            this.ATK = player.ATK.Value;
            this.DEF = player.DEF.Value;

            this.FirstATK = player.FirstATK.Value;
            this.FirstDEF = player.FirstDEF.Value;

            this.CurrentHP = player.CurrentHP;
        }
        // 플레이어 스탯 데이터 없을 시 더미 데이터
        else
        {
            this.HP = 999;
            this.ACT = 20;
            this.ATK = 1;
            this.DEF = 1;

            this.FirstATK = 1;
            this.FirstDEF = 1;

            this.CurrentHP = 999;
        }

        this.battlePlayerUI.SetHpText(this.CurrentHP, this.HP);
        this.battlePlayerUI.SetStatText(this.ATK, this.DEF);
    }

    #region Init
    /// <summary>
    /// 전투 시작 전 세팅
    /// </summary>
    public void StartSetting()
    {
        this.OnStart = false;

        if (player.StartDEF.Value >= 1)
        {
            this.CurrentSP = player.StartDEF.Value;
            GameBoardEvents.OnPlayerShieldChanged.Invoke(0, this.CurrentSP);
        }

        InitAct();
    }

    /// <summary>
    /// 플레이어 턴 초기화
    /// </summary>
    public void TurnInit()
    {
        // 스탯 초기화
        InitAct();
        InitShield();
    }

    /// <summary>
    /// 플레이어 행동 관련 초기화
    /// </summary>
    private void InitAct()
    {
        this.OnFirst = true;

        this.battlePlayerUI.UpdateAnimateUI(this.CurrentACT, this.ACT);
        this.CurrentACT = this.ACT;
    }

    /// <summary>
    /// 플레이어 방어력 초기화
    /// </summary>
    private void InitShield()
    {
        var oldPoint = this.CurrentSP;
        this.CurrentSP = 0;
        GameBoardEvents.OnPlayerShieldChanged.Invoke(oldPoint, this.CurrentSP);
    }
    #endregion

    #region Player Action
    /// <summary>
    /// 플레이어 행동 횟수 계산
    /// </summary>
    /// <param name="count"></param>
    public void ActionCounter(int count)
    {
        CurrentACT += count;

        // 0 이하 표시 금지
        if (CurrentACT <= 0) CurrentACT = 0;

        this.battlePlayerUI.SetActText(CurrentACT);
    }

    /// <summary>
    /// Player 공격
    /// </summary>
    /// <param name="elements"></param>
    /// <param name="enemy"></param>
    public void PlayerAttack(List<GameBoardElement> elements, BattleEnemy enemy)
    {
        int attackPoint;

        if (this.OnFirst)
        {
            this.OnFirst = false;
            attackPoint = this.GetElementPoint(elements, this.FirstATK, this.ATK);
        }
        else
        {
            attackPoint = this.GetElementPoint(elements, this.ATK, this.ATK);
        }

        if (attackPoint < 50) 
            BattleSFX.Instance.Play(BattleSFX.Instance.playerAttackNormal);
        else
            BattleSFX.Instance.Play(BattleSFX.Instance.playerAttackHeavy);

        enemy.Damage(attackPoint);
    }

    /// <summary>
    /// Player 방어
    /// </summary>
    /// <param name="elements"></param>
    public void PlayerShield(List<GameBoardElement> elements)
    {
        int shieldPoint;

        if (this.OnFirst)
        {
            this.OnFirst = false;
            shieldPoint = this.GetElementPoint(elements, this.FirstDEF, this.DEF);
        }
        else
        {
            shieldPoint = this.GetElementPoint(elements, this.DEF, this.DEF);
        }

        BattleSFX.Instance.Play(BattleSFX.Instance.defense);

        var oldPoint = this.CurrentSP;
        this.CurrentSP += shieldPoint;
        GameBoardEvents.OnPlayerShieldChanged.Invoke(oldPoint, this.CurrentSP);
    }

    /// <summary>
    /// Player 체력 회복
    /// </summary>
    /// <param name="elements"></param>
    public void PlayerRecovery(List<GameBoardElement> elements)
    {
        var oldPoint = this.CurrentHP;
        this.CurrentHP += this.GetElementPoint(elements, 0, 1);
        if (this.CurrentHP > HP) this.CurrentHP = HP;
        GameBoardEvents.OnPlayerHealthChanged.Invoke(oldPoint, this.CurrentHP);
    }
    #endregion

    #region Damaged
    /// <summary>
    /// Player 피해
    /// </summary>
    /// <param name="damage"></param>
    public void Damaged(int damage)
    {
        DamagedEffect(damage);

        // 현 방어력이 0 초과 인가?
        if (CurrentSP > 0) ShieldDamaged(damage);
        // 현 방어력이 0 이하 이면 HealthDamaged
        else HealthDamaged(damage);
    }

    /// <summary>
    /// 데미지 받을 시 => RectTransform 흔들림 효과
    /// </summary>
    /// <param name="damage"></param>
    private void DamagedEffect(int damage)
    {
        if (damage < 50)
            StartCoroutine(this.statTable.ShakeCoroutine(2, 8, this.weakWreckParticle));
        else
            StartCoroutine(this.playerTable.ShakeCoroutine(5, 15, this.wreckParticle));
    }

    /// <summary>
    /// 플레이어 체력 피해 계산
    /// </summary>
    /// <param name="damage"></param>
    private void HealthDamaged(int damage)
    {
        if (damage <= 0) return;
        var damagedHP = CurrentHP - damage;

        // 플레이어 사망
        if (damagedHP <= 0)
        {
            this.battlePlayerUI.Dead();
            this.battleSystem.BattleCheck(BattlePlay.PlayerDead);

            // 전투 종료
            return;
        }

        CurrentHP = damagedHP;
        GameBoardEvents.OnPlayerHealthChanged.Invoke(CurrentHP, damagedHP);

        if (player != null)
        {
            player.SetHP(CurrentHP);
        }
    }

    /// <summary>
    /// 플레이어 방어력 피해 계산
    /// </summary>
    /// <param name="damage"></param>
    private void ShieldDamaged(int damage)
    {
        var damagedSP = this.CurrentSP;
        damagedSP -= damage;
        
        if (damagedSP <= 0)
        {
            var remainDamage = damagedSP * -1;
            HealthDamaged(remainDamage);
            damagedSP = 0;
        }

        this.CurrentSP = damagedSP;
        GameBoardEvents.OnPlayerShieldChanged.Invoke(this.CurrentSP, damagedSP);
    }
    #endregion
}
