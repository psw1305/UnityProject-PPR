using PSW.Core.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer : BehaviourSingleton<BattlePlayer>
{
    [Header("Potion")]
    [SerializeField] private Transform[] potionSlots;

    [Header("Scripts")]
    [SerializeField] private BattleSystem battleSystem;
    [SerializeField] private BattlePlayerUI battlePlayerUI;
    [SerializeField] private GameBoard gameBoard;

    public int HP { get; set; }
    public int ACT { get; set; }

    // 타일 하나당 주어지는 일반 포인트
    public int ATK { get; set; }
    public int DEF { get; set; }

    // 전투 시작 시 주어지는 포인트
    public int StartDEF { get; set; }
    public int StartCard { get; set; }

    // 첫 공격 시 주어지는 시작 포인트
    public int FirstATK { get; set; }
    public int FirstDEF { get; set; }

    // 시작과 끝 Bool 체크
    public bool OnStart { get; private set; }
    public bool OnFirst { get; private set; }

    // 현 Stat
    public int CurrentHP { get; private set; }
    public int CurrentSP { get; private set; }
    public int CurrentACT { get; private set; }

    public float GetPercentHP() => CurrentHP / (float)this.HP;

    protected override void Awake()
    {
        base.Awake();

        this.OnStart = true;

        if (Player.Instance != null)
        {
            this.HP = Player.Instance.HP.Value;
            this.ACT = Player.Instance.ACT.Value;
            this.ATK = Player.Instance.ATK.Value;
            this.DEF = Player.Instance.DEF.Value;

            this.StartDEF = Player.Instance.StartDEF.Value;
            this.StartCard = Player.Instance.StartCard.Value;

            this.FirstATK = Player.Instance.FirstATK.Value;
            this.FirstDEF = Player.Instance.FirstDEF.Value;

            this.CurrentHP = Player.Instance.CurrentHP;
        }
        // 플레이어 스탯 데이터 없을 시 더미 데이터
        else
        {
            this.HP = 999;
            this.ACT = 20;
            this.ATK = 1;
            this.DEF = 1;

            this.StartDEF = 0;
            this.StartCard = 0;

            this.FirstATK = 0;
            this.FirstDEF = 0;

            this.CurrentHP = 999;
        }

        this.battlePlayerUI.SetHpText(this.CurrentHP, this.HP);
        this.battlePlayerUI.SetStatText();
    }

    #region Init
    /// <summary>
    /// 전투 시작 전 세팅
    /// </summary>
    public IEnumerator PlayerOnStart()
    {
        this.OnStart = false;

        InitAct();

        // 전투 시작 시 방어도 부여
        if (this.StartDEF > 0)
        {
            this.CurrentSP = this.StartDEF;
            GameBoardEvents.OnPlayerShieldChanged.Invoke(0, this.CurrentSP);
        }

        // 전투 시작 시 스킬 카드 배치
        if (this.StartCard > 0)
        {
            for (int i = 0; i < this.StartCard; i++)
            {
                yield return this.gameBoard.SkillCardSpawn();
            }
        }
    }

    /// <summary>
    /// 플레이어 턴 초기화
    /// </summary>
    public void PlayerOnInit()
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
    public void PlayerAttack(List<GameBoardCard> elements, BattleEnemy enemy)
    {
        int attackPoint;

        if (this.OnFirst)
        {
            this.OnFirst = false;
            attackPoint = this.GetPoint(elements, this.FirstATK, this.ATK);
        }
        else
        {
            attackPoint = this.GetPoint(elements, 0, this.ATK);
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
    public void PlayerShield(List<GameBoardCard> elements)
    {
        int shieldPoint;

        if (this.OnFirst)
        {
            this.OnFirst = false;
            shieldPoint = this.GetPoint(elements, this.FirstDEF, this.DEF);
        }
        else
        {
            shieldPoint = this.GetPoint(elements, 0, this.DEF);
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
    public void PlayerRecovery(List<GameBoardCard> elements)
    {
        var oldPoint = this.CurrentHP;
        this.CurrentHP += this.GetPoint(elements, 0, 1);
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
        this.battlePlayerUI.TakeDamageEffect(damage);

        // 현 방어력이 0 초과 인가?
        if (CurrentSP > 0) ShieldDamaged(damage);
        // 현 방어력이 0 이하 이면 HealthDamaged
        else HealthDamaged(damage);
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

        if (Player.Instance != null)
        {
            Player.Instance.SetHP(CurrentHP);
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
