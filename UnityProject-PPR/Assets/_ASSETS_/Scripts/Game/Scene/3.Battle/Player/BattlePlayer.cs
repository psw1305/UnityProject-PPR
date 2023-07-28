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
    [SerializeField] private GameObject potionPrefab;
    [SerializeField] private Transform[] potionSlots;

    [Header("Particle")]
    [SerializeField] private ParticleSystem weakWreckParticle;
    [SerializeField] private ParticleSystem wreckParticle;

    [Header("Scripts")]
    [SerializeField] private BattleSystem battleSystem;
    [SerializeField] private BattlePlayerUI battlePlayerUI;

    public int HP { get; set; }
    public int ACT { get; set; }
    public int ATK { get; set; }
    public int DEF { get; set; }

    public int CurrentHP { get; private set; } // �� ü��
    public int CurrentSP { get; private set; } // �� ����
    public int CurrentACT { get; private set; } // �� �ൿ��
    public int EarnCash { get; private set; }

    public float GetPercentHP() => CurrentHP / (float)this.HP;

    private Player player;

    protected override void Awake()
    {
        base.Awake();

        this.player = Player.Instance;

        if (this.player != null)
        {
            this.HP = this.player.HP.Value;
            this.ACT = this.player.ACT.Value;
            this.CurrentHP = this.player.CurrentHP;
        }
        // �÷��̾� ���� ������ ���� �� ���� ������
        else
        {
            this.HP = 999;
            this.ACT = 20;
            this.CurrentHP = 999;
        }

        this.ATK = 1;
        this.DEF = 1;

        this.battlePlayerUI.SetHpText(this.CurrentHP, this.HP);
        this.battlePlayerUI.SetStatText(this.ACT, this.ATK, this.DEF);
    }

    private void OnEnable()
    {
        GameBoardEvents.OnPlayerTurnInit.AddListener(Init);
    }

    private void OnDisable()
    {
        GameBoardEvents.OnPlayerTurnInit.RemoveListener(Init);
    }

    /// <summary>
    /// �÷��̾� ����
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
    /// �÷��̾� ���� ����
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
    /// �÷��̾� ü�� ȸ��
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
    /// �÷��̾� �� ����
    /// </summary>
    /// <param name="elements"></param>
    public void PlayerEarn(List<GameBoardElement> elements)
    {
        var oldPoint = EarnCash;
        EarnCash += this.GetElementPoint(elements);
        GameBoardEvents.OnPlayerCashChanged.Invoke(oldPoint, EarnCash);
    }

    /// <summary>
    /// �÷��̾� �ൿ Ƚ�� ���
    /// </summary>
    /// <param name="count"></param>
    public void ActionCounter(int count)
    {
        CurrentACT += count;

        // 0 ���� ǥ�� ����
        if (CurrentACT <= 0) CurrentACT = 0;

        this.battlePlayerUI.SetActText(CurrentACT);
    }

    /// <summary>
    /// �÷��̾� ����
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage)
    {
        DamagedEffect(damage);

        // �� ������ 0 �ʰ� �ΰ�?
        if (CurrentSP > 0) ShieldDamaged(damage);
        // �� ������ 0 ���� �̸� HealthDamaged
        else HealthDamaged(damage);
    }

    /// <summary>
    /// ������ ���� �� => RectTransform ��鸲 ȿ��
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
    /// �÷��̾� ü�� ���� ���
    /// </summary>
    /// <param name="damage"></param>
    private void HealthDamaged(int damage)
    {
        if (damage <= 0) return;
        var damagedHP = CurrentHP - damage;

        // �÷��̾� ���
        if (damagedHP <= 0)
        {
            this.battlePlayerUI.Dead();
            this.battleSystem.BattleCheck(BattlePlay.PlayerDead);

            // ���� ����
            return;
        }

        CurrentHP = damagedHP;
        GameBoardEvents.OnPlayerHealthChanged.Invoke(CurrentHP, damagedHP);

        if (this.player != null)
        {
            this.player.SetHP(CurrentHP);
        }
    }

    /// <summary>
    /// �÷��̾� ���� ���� ���
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
    /// �÷��̾� �ʱ�ȭ
    /// </summary>
    private void Init()
    {
        // ���� �ʱ�ȭ
        InitAct();
        InitShield();
    }

    /// <summary>
    /// �÷��̾� ���ݷ� �ʱ�ȭ
    /// </summary>
    private void InitAct()
    {
        this.battlePlayerUI.UpdateAnimateUI(this.CurrentACT, this.ACT);
        this.CurrentACT = this.ACT;
    }

    /// <summary>
    /// �÷��̾� ���� �ʱ�ȭ
    /// </summary>
    private void InitShield()
    {
        var oldPoint = this.CurrentSP;
        this.CurrentSP = 0;
        GameBoardEvents.OnPlayerShieldChanged.Invoke(oldPoint, this.CurrentSP);
    }
}
