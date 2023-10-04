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
        Card, Relic, Potion, Cash
    }

    /// <summary>
    /// ������ ���
    /// </summary>
    public enum ItemGradeType
    {
        Common, Uncommon, Rare, Legend
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
    public enum CardDetailType
    {
        Normal, Instant, Ready, Finish, Obstacle
    }

    /// <summary>
    /// ���� Ÿ��
    /// </summary>
    public enum RelicType
    {
        Sword, Shield, Armor, Helm,
        Idol, Instrument,
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
    public enum EnemySkillType
    {
        Attack,
        Defense,
        Buff,
    }

    /// <summary>
    /// �� ��ų ���� Ÿ��
    /// </summary>
    public enum EnemySkillCard
    {
        Attack,
        Defense,
        Buff,
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