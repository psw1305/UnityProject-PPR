namespace PSW.Core.Enums
{
    /// <summary>
    /// ���� ����� ���еǴ� Play Types
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
    /// ������ Ÿ��
    /// </summary>
    public enum ItemType
    {
        Equipment, Useable, Cash, Stuff
    }

    /// <summary>
    /// ������ ��� Ÿ��
    /// </summary>
    public enum ItemRare
    {
        Common, Uncommon, Rare
    }

    /// <summary>
    /// ������ Equipment Ÿ��
    /// </summary>
    public enum EquipmentType
    {
        Helmet, Armor, Weapon, Trinket
    }

    /// <summary>
    /// �Ҹ�ǰ �ɷ� Ÿ��
    /// </summary>
    public enum UseableAbility
    {
        Change, 
        Remove, 
        StatModify,
    }

    /// <summary>
    /// �÷��̾� Element Ÿ��
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
    /// �÷��̾� Element ���� Ÿ��
    /// </summary>
    public enum ElementAttack
    {
        Normal, 
        Reinforce, 
        Burn
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