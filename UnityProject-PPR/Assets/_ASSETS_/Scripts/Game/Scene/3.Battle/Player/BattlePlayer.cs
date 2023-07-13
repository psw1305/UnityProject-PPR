using PSW.Core.Enums;
using PSW.Core.Extensions;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer : BehaviourSingleton<BattlePlayer>
{
    [Header("Settings")]
    [SerializeField] private RectTransform playerTable;
    [SerializeField] private RectTransform statTable;

    [Header("Useables")]
    [SerializeField] private GameObject useableItemPrefab;
    [SerializeField] private Transform[] useableSlots;
    private BattlePlayerUseableItem[] useableItems;

    [Header("Particle")]
    [SerializeField] private ParticleSystem weakWreckParticle;
    [SerializeField] private ParticleSystem wreckParticle;

    [Header("Scripts")]
    [SerializeField] private BattleSystem battleSystem;
    [SerializeField] private BattlePlayerUI playerUI;

    public int HP { get; set; }
    public int ACT { get; set; }
    public int ATK { get; set; }
    public int DEF { get; set; }

    public int CurrentHP { get; private set; } // 현 체력
    public int CurrentSP { get; private set; } // 현 방어력
    public int CurrentACT { get; private set; } // 현 행동력
    public int EarnCash { get; private set; }

    public float GetPercentHP() => CurrentHP / (float)this.HP;

    protected override void Awake()
    {
        base.Awake();

        this.useableItems = new BattlePlayerUseableItem[5];

        if (Player.Instance != null)
        {
            this.HP = Player.Instance.HP.Value;
            this.ACT = Player.Instance.ACT.Value;
            this.ATK = Player.Instance.ATK.Value;
            this.DEF = Player.Instance.DEF.Value;
            this.CurrentHP = Player.CurrentHP;
        }
        // 플레이어 스탯 데이터 없을 시 더미 데이터
        else
        {
            this.HP = 999;
            this.ACT = 20;
            this.ATK = 1;
            this.DEF = 1;
            this.CurrentHP = 999;
        }

        this.playerUI.SetHpText(this.CurrentHP, this.HP);
        this.playerUI.SetStatText(this.ACT, this.ATK, this.DEF);
    }

    private void OnEnable()
    {
        GameBoardEvents.OnPlayerTurnInit.AddListener(Init);
    }

    private void OnDisable()
    {
        GameBoardEvents.OnPlayerTurnInit.RemoveListener(Init);
    }

    public void SetUseableItems()
    {
        if (Player.Instance == null) return;

        var itemLists = Player.Instance.useableItems;

        for (int i = 0; i < itemLists.Length; i++)
        {
            if (itemLists[i] != null)
            {
                var useableItemClone = Instantiate(this.useableItemPrefab, this.useableSlots[i]).GetComponent<BattlePlayerUseableItem>();
                useableItemClone.Set(itemLists[i].GetUseableData());
                this.useableItems[i] = useableItemClone;
            }
        }
    }

    /// <summary>
    /// 플레이어 공격
    /// </summary>
    /// <param name="elements"></param>
    /// <param name="enemy"></param>
    public void PlayerAttack(List<GameBoardElement> elements, BattleEnemy enemy)
    {
        var attackPoint = this.GetElementPoint(elements, this.ATK);

        if (attackPoint < 50) 
            BattleSFX.Instance.Play(BattleSFX.Instance.playerAttackNormal);
        else
            BattleSFX.Instance.Play(BattleSFX.Instance.playerAttackHeavy);

        enemy.Damage(attackPoint);
    }

    /// <summary>
    /// 플레이어 쉴드 생성
    /// </summary>
    /// <param name="elements"></param>
    public void PlayerDefense(List<GameBoardElement> elements)
    {
        BattleSFX.Instance.Play(BattleSFX.Instance.defense);

        var oldPoint = this.CurrentSP;
        this.CurrentSP += this.GetElementPoint(elements, this.DEF);
        GameBoardEvents.OnPlayerShieldChanged.Invoke(oldPoint, this.CurrentSP);
    }

    /// <summary>
    /// 플레이어 체력 회복
    /// </summary>
    /// <param name="elements"></param>
    public void PlayerRecovery(List<GameBoardElement> elements)
    {
        var oldPoint = this.CurrentHP;
        this.CurrentHP += this.GetElementPoint(elements);
        if (this.CurrentHP > HP) this.CurrentHP = HP;
        GameBoardEvents.OnPlayerHealthChanged.Invoke(oldPoint, this.CurrentHP);
    }

    /// <summary>
    /// 플레이어 돈 벌기
    /// </summary>
    /// <param name="elements"></param>
    public void PlayerEarn(List<GameBoardElement> elements)
    {
        var oldPoint = EarnCash;
        EarnCash += this.GetElementPoint(elements);
        GameBoardEvents.OnPlayerCashChanged.Invoke(oldPoint, EarnCash);
    }

    /// <summary>
    /// 플레이어 행동 횟수 계산
    /// </summary>
    /// <param name="count"></param>
    public void ActionCounter(int count)
    {
        CurrentACT += count;

        // 0 이하 표시 금지
        if (CurrentACT <= 0) CurrentACT = 0;

        this.playerUI.SetActText(CurrentACT);
    }

    /// <summary>
    /// 플레이어 피해
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage)
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
            this.playerUI.Dead();
            this.battleSystem.BattleCheck(BattlePlay.PlayerDead);

            // 전투 종료
            return;
        }

        CurrentHP = damagedHP;
        GameBoardEvents.OnPlayerHealthChanged.Invoke(CurrentHP, damagedHP);

        if (Player.Instance != null) Player.Instance.SetHp(CurrentHP);
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

    /// <summary>
    /// 플레이어 초기화
    /// </summary>
    private void Init()
    {
        // 스탯 초기화
        InitActionCount();
        InitShieldPoint();

        // 소모품 아이템 초기화
        for (int i = 0; i < this.useableItems.Length; i++)
        {
            if (this.useableItems[i] != null)
            {
                this.useableItems[i].Init();
            }
        }
    }

    /// <summary>
    /// 플레이어 공격력 초기화
    /// </summary>
    private void InitActionCount()
    {
        this.playerUI.UpdateAnimateUI(this.CurrentACT, this.ACT);
        this.CurrentACT = this.ACT;
    }

    /// <summary>
    /// 플레이어 방어력 초기화
    /// </summary>
    private void InitShieldPoint()
    {
        var oldPoint = this.CurrentSP;
        this.CurrentSP = 0;
        GameBoardEvents.OnPlayerShieldChanged.Invoke(oldPoint, this.CurrentSP);
    }
}
