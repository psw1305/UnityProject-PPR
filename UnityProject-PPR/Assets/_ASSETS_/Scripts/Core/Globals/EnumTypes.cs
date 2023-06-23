namespace PSW.Core.Enums
{
    /// <summary>
    /// 게임 진행시 구분되는 Play Types
    /// </summary>
    public enum GamePlayType
    {
        Ready, Play, Pause, End,
        PlayerTurn, PlayerTurnEnd, PlayerDead,
        EnemyTurn, EnemyTurnEnd, EnemyDead
    }

    /// <summary>
    /// Player Stat Types
    /// </summary>
    public enum StatType
    {
        HP, ACT, ATK, DEF
    }

    /// <summary>
    /// 아이템 타입
    /// </summary>
    public enum ItemType
    {
        Equipment, Useable, Cash, Stuff
    }

    /// <summary>
    /// 아이템 레어도 타입
    /// </summary>
    public enum ItemRare
    {
        Common, Uncommon, Rare
    }

    /// <summary>
    /// 아이템 Equipment 타입
    /// </summary>
    public enum EquipmentType
    {
        Helmet, Armor, Weapon, Trinket
    }

    /// <summary>
    /// 소모품 능력 타입
    /// </summary>
    public enum UseableAbility
    {
        Change, 
        Remove, 
        StatModify,
    }

    /// <summary>
    /// 플레이어 Element 타입
    /// </summary>
    public enum ElementType
    {
        Attack, 
        Defense, 
        Potion, 
        Coin, 
        Synergy, 
        Obstacle, 
        None
    }

    /// <summary>
    /// 플레이어 Element 공격 타입
    /// </summary>
    public enum ElementAttack
    {
        Normal, 
        Reinforce, 
        Burn
    }

    /// <summary>
    /// 적 타입
    /// </summary>
    public enum EnemyType
    {
        Minor, Elite, Boss
    }

    /// <summary>
    /// 적 스킬 타입
    /// </summary>
    public enum EnemySkill
    {
        Attack, 
        Defense, 
        Reinforce, 
        Recovery, 
        Buff,
        Debuff
    }

    public enum MysteryType
    {
        HealthUp,
        ItemGain,
        GoldGain,
    }
}

/// <summary>
/// Map 스크립트 전용 enums
/// </summary>
namespace PSW.Core.Map
{
    public enum MapNodeType
    {
        Starting,
        MinorEnemy,
        EliteEnemy,
        RestSite,
        Treasure,
        Shop,
        Mystery,
        Boss,
    }

    public enum StageState
    {
        Locked,
        Visited,
        Attainable
    }
}