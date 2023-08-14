namespace PSW.Core.Enums
{
    /// <summary>
    /// 게임 진행 상황
    /// </summary>
    public enum GameState
    {
        Stage, Battle, Pause, Victory, Defeat
    }

    /// <summary>
    /// 전투 진행시 구분되는 Battle Types
    /// </summary>
    public enum BattlePlay
    {
        PlayerTurn, PlayerDead,
        EnemyTurn, EnemyAllDead
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
        Cash, Card, Potion, Relic
    }

    /// <summary>
    /// 유물 타입
    /// </summary>
    public enum RelicType
    {
        Sword, Shield, Armor, Helm, Idol
    }

    /// <summary>
    /// 아이템 등급
    /// </summary>
    public enum ItemGrade
    {
        Common, Uncommon, Rare, Shop, Boss, Event
    }

    /// <summary>
    /// Card 타입
    /// </summary>
    public enum CardType
    {
        Attack, Defense, Synergy, Obstacle, None
    }

    /// <summary>
    /// Element Detail 구분
    /// </summary>
    public enum CardDetail
    {
        Normal, Instant, Start, Finish,
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