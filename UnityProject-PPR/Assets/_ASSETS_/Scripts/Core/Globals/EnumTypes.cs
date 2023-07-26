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
        Cash, Card, Potion, Artifact
    }

    /// <summary>
    /// 아이템 레어도 타입
    /// </summary>
    public enum ItemRare
    {
        Common, Uncommon, Rare, Boss
    }

    /// <summary>
    /// 카드 아이템 타입
    /// </summary>
    public enum CardType
    {
        Attack, Defense, Special, Joker
    }

    /// <summary>
    /// Element 타입
    /// </summary>
    public enum ElementType
    {
        Attack, Defense, Synergy, Obstacle, None
    }

    /// <summary>
    /// Element Detail 구분
    /// </summary>
    public enum ElementDetailType
    {
        Normal, Skill,
    }

    /// <summary>
    /// Element Skill 구분
    /// </summary>
    public enum ElementSkillType
    {
        Normal, 
        Start, Finish, Instant,
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