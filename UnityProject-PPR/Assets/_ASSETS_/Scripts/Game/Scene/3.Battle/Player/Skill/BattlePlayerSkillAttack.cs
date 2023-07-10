using UnityEngine;

public static class BattlePlayerElementSkill
{
    public static int Skill(string skillName, int value)
    {
        return skillName switch
        {
            "Strike" => Strike(value),
            "Angry" => Angry(value),
            _ => value,
        };
    }

    public static int Strike(int value)
    {
        return value += 4;
    }

    public static int Angry(int value)
    {
        return value * 2;
    }
}
