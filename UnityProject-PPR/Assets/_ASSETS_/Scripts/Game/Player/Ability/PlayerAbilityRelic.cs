using PSW.Core.Enums;
using PSW.Core.Stat;
using System.Collections.Generic;

public static class PlayerAbilityRelic
{
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
                EquipSword(1, player, relic);
                break;
            case "Sword02_C_Brass":
                EquipSword(3, player, relic);
                break;
            case "Sword03_C_Steel":
                EquipSword(5, player, relic);
                break;
        };
    }

    public static void EquipSword(int value, Player player, InventoryItemRelic relic)
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
                EquipShield(1, player, relic);
                break;
            case "Shield02_C_Brass":
                EquipShield(3, player, relic);
                break;
            case "Shield03_C_Steel":
                EquipShield(5, player, relic);
                break;
        };
    }

    public static void EquipShield(int value, Player player, InventoryItemRelic relic)
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
                EquipArmor(1, player, relic);
                break;
            case "Armor02_C_Brass":
                EquipArmor(4, player, relic);
                break;
            case "Armor03_C_Steel":
                EquipArmor(6, player, relic);
                break;
        };
    }

    public static void EquipArmor(int value, Player player, InventoryItemRelic relic)
    {
        player.StartDEF.AddModifier(new StatModifier(value, StatModType.Int, relic));
    }
    #endregion

    #region Idol
    /// <summary>
    /// 갑옷 => 전투 시작시 방어도를 N 만큼 얻습니다
    /// </summary>
    public static void Idol(Player player, InventoryItemRelic relic)
    {
        switch (relic.RelicID)
        {
            case "Idol01_UC_Anger":
                EquipIdol_ATK(1, player, relic);
                break;
            case "Idol02_UC_Stable":
                EquipIdol_DEF(1, player, relic);
                break;
            case "Idol03_R_Rage":
                EquipIdol_ATK(2, player, relic);
                break;
            case "Idol04_R_Safety":
                EquipIdol_DEF(2, player, relic);
                break;
        };
    }

    public static void EquipIdol_ATK(int value, Player player, InventoryItemRelic relic)
    {
        player.ATK.AddModifier(new StatModifier(value, StatModType.Int, relic));
    }

    public static void EquipIdol_DEF(int value, Player player, InventoryItemRelic relic)
    {
        player.DEF.AddModifier(new StatModifier(value, StatModType.Int, relic));
    }
    #endregion
}
