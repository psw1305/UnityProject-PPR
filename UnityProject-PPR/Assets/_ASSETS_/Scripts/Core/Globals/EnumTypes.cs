namespace PSW.Core.Enums
{
    /// <summary>
    /// ���� ���� ��Ȳ
    /// </summary>
    public enum GameState
    {
        Stage, Battle, Pause, Victory, Defeat
    }

    /// <summary>
    /// ���� ����� ���еǴ� Battle Types
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
    /// ������ Ÿ��
    /// </summary>
    public enum ItemType
    {
        Cash, Card, Potion, Relic
    }

    /// <summary>
    /// ������ ���
    /// </summary>
    public enum ItemGrade
    {
        Common, Uncommon, Rare, Shop, Boss, Event
    }

    /// <summary>
    /// ī�� ������ Ÿ��
    /// </summary>
    public enum CardType
    {
        Attack, Defense, Special, Joker
    }

    /// <summary>
    /// Element Ÿ��
    /// </summary>
    public enum ElementType
    {
        Attack, Defense, Synergy, Obstacle, None
    }

    /// <summary>
    /// Element Detail ����
    /// </summary>
    public enum ElementDetailType
    {
        Normal, Skill,
    }

    /// <summary>
    /// Element Skill ����
    /// </summary>
    public enum ElementSkillType
    {
        Normal, 
        Start, Finish, Instant,
    }

    /// <summary>
    /// �� Ÿ��
    /// </summary>
    public enum EnemyType
    {
        Minor, Elite, Boss
    }

    /// <summary>
    /// �� ��ų Ÿ��
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
/// Map ��ũ��Ʈ ���� enums
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