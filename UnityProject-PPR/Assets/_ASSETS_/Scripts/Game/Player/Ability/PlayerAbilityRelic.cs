using PSW.Core.Enums;
using PSW.Core.Stat;
using System.Collections.Generic;

public static class PlayerAbilityRelic
{
    /// <summary>
    /// 유물 장착 시 => 능력 부여
    /// </summary>
    /// <param name="player"></param>
    /// <param name="relicList"></param>
    /// <param name="relic"></param>
    public static void SetRelic(this Player player, List<InventoryItemRelic> relicList, InventoryItemRelic relic)
    {
        relicList.Add(relic);

        switch (relic.RelicType)
        {
            case RelicType.Sword:
                Relic.Sword(player, relic);
                break;
            case RelicType.Shield:
                Relic.Shield(player, relic);
                break;
            case RelicType.Armor:
                Relic.Armor(player, relic);
                break;
            case RelicType.Idol:
                Relic.Idol(player, relic);
                break;
            case RelicType.Instrument:
                Relic.Instrument(player, relic);
                break;
        }
    }
}

public static class Relic
{
    #region Sword
    /// <summary>
    /// 검 => 처음 공격 시작 시 +N 
    /// </summary> 
    public static void Sword(Player player, InventoryItemRelic relic)
    {
        switch (relic.RelicID)
        {
            case "Sword01_C_Cheap":
                Equip_Sword(1, player, relic);
                break;
            case "Sword02_C_Brass":
                Equip_Sword(3, player, relic);
                break;
            case "Sword03_C_Steel":
                Equip_Sword(5, player, relic);
                break;
        };
    }

    public static void Equip_Sword(int value, Player player, InventoryItemRelic relic)
    {
        player.FirstATK.AddModifier(new StatModifier(value, StatModType.Int, relic));
    }
    #endregion

    #region Shield
    /// <summary>
    /// 방패 => 처음 방어 시작 시 +N 
    /// </summary> 
    public static void Shield(Player player, InventoryItemRelic relic)
    {
        switch (relic.RelicID)
        {
            case "Shield01_C_Cheap":
                Equip_Shield(1, player, relic);
                break;
            case "Shield02_C_Brass":
                Equip_Shield(3, player, relic);
                break;
            case "Shield03_C_Steel":
                Equip_Shield(5, player, relic);
                break;
        };
    }

    public static void Equip_Shield(int value, Player player, InventoryItemRelic relic)
    {
        player.FirstDEF.AddModifier(new StatModifier(value, StatModType.Int, relic));
    }
    #endregion

    #region Armor
    /// <summary>
    /// 갑옷 => 전투 시작시 방어도를 N 만큼 얻습니다
    /// </summary>
    public static void Armor(Player player, InventoryItemRelic relic)
    {
        switch (relic.RelicID)
        {
            case "Armor01_C_Cheap":
                Equip_Armor(1, player, relic);
                break;
            case "Armor02_C_Brass":
                Equip_Armor(4, player, relic);
                break;
            case "Armor03_C_Steel":
                Equip_Armor(6, player, relic);
                break;
        };
    }

    public static void Equip_Armor(int value, Player player, InventoryItemRelic relic)
    {
        player.StartDEF.AddModifier(new StatModifier(value, StatModType.Int, relic));
    }
    #endregion

    #region Idol
    /// <summary>
    /// 우상 => 카드 공격력 또는 방어력을 +N
    /// </summary>
    public static void Idol(Player player, InventoryItemRelic relic)
    {
        switch (relic.RelicID)
        {
            case "Idol01_UC_Anger":
                Equip_Idol_ATK(1, player, relic);
                break;
            case "Idol02_UC_Stable":
                Equip_Idol_DEF(1, player, relic);
                break;
            case "Idol03_R_Rage":
                Equip_Idol_ATK(2, player, relic);
                break;
            case "Idol04_R_Safety":
                Equip_Idol_DEF(2, player, relic);
                break;
        };
    }

    public static void Equip_Idol_ATK(int value, Player player, InventoryItemRelic relic)
    {
        player.ATK.AddModifier(new StatModifier(value, StatModType.Int, relic));
    }

    public static void Equip_Idol_DEF(int value, Player player, InventoryItemRelic relic)
    {
        player.DEF.AddModifier(new StatModifier(value, StatModType.Int, relic));
    }
    #endregion

    #region Instrument
    /// <summary>
    /// 악기 => 전투 시작시 스킬 카드를 N 만큼 배치
    /// </summary>
    public static void Instrument(Player player, InventoryItemRelic relic)
    {
        switch (relic.RelicID)
        {
            case "Instrument01_C_Flute":
                Equip_Instrument(1, player, relic);
                break;
            case "Instrument02_UC_Drum":
                Equip_Instrument(2, player, relic);
                break;
            case "Instrument03_R_Horn":
                Equip_Instrument(4, player, relic);
                break;
        };
    }

    public static void Equip_Instrument(int value, Player player, InventoryItemRelic relic)
    {
        player.StartCard.AddModifier(new StatModifier(value, StatModType.Int, relic));
    }
    #endregion
}
