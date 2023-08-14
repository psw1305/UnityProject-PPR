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
    /// ���� Ÿ��
    /// </summary>
    public enum RelicType
    {
        Sword, Shield, Armor, Helm, Idol
    }

    /// <summary>
    /// ������ ���
    /// </summary>
    public enum ItemGrade
    {
        Common, Uncommon, Rare, Shop, Boss, Event
    }

    /// <summary>
    /// Card Ÿ��
    /// </summary>
    public enum CardType
    {
        Attack, Defense, Synergy, Obstacle, None
    }

    /// <summary>
    /// Element Detail ����
    /// </summary>
    public enum CardDetail
    {
        Normal, Instant, Start, Finish,
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