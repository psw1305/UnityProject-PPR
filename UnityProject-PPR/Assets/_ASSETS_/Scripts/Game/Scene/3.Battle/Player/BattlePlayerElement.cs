using PSW.Core.Enums;
using System.Collections.Generic;

public static class BattlePlayerElement
{
    public static int GetElementPoint(this BattlePlayer battlePlayer, List<GameBoardElement> elements, int plusPoint = 1)
    {
        int resultPoint = 0;

        foreach (GameBoardElement element in elements)
        {
            if (element.ElementDetailType == ElementDetailType.Skill)
            {
                resultPoint = Skill(element.GetSkillName(), resultPoint);
            }
            else
            {
                resultPoint += plusPoint;
            }
        }

        return resultPoint;
    }

    public static int Skill(string skillName, int value)
    {
        return skillName switch
        {
            "Strike" => Strike(value),
            "Anger" => Anger(value),
            _ => value,
        };
    }

    public static int Strike(int value)
    {
        return value += 4;
    }

    public static int Anger(int value)
    {
        return value * 2;
    }
}
