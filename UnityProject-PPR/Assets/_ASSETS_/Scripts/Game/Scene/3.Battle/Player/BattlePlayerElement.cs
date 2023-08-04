using PSW.Core.Enums;
using System.Collections.Generic;

public static class BattlePlayerElement
{
    public static int GetElementPoint(this BattlePlayer battleplayer, List<GameBoardElement> elements, int startPoint, int plusPoint)
    {
        int resultPoint = startPoint;

        foreach (GameBoardElement element in elements)
        {
            if (element.ElementDetailType == ElementDetailType.Skill)
            {
                if (element.ElementType == ElementType.Attack)
                {
                    resultPoint = AttackSkill(element.GetSkillName(), resultPoint);
                }
                else if (element.ElementType == ElementType.Defense)
                {
                    resultPoint = DefenseSkill(element.GetSkillName(), resultPoint);
                }
            }
            else
            {
                resultPoint += plusPoint;
            }
        }

        return resultPoint;
    }

    /// <summary>
    /// 공격 Element Skill
    /// </summary>
    /// <param name="skillName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int AttackSkill(string skillName, int value)
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
        return value *= 2;
    }

    /// <summary>
    /// 방어 Element Skill
    /// </summary>
    /// <param name="skillName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int DefenseSkill(string skillName, int value)
    {
        return skillName switch
        {
            "Defend" => Defend(value),
            "Entrench" => Entrench(value),
            _ => value,
        };
    }

    public static int Defend(int value)
    {
        return value += 4;
    }

    public static int Entrench(int value)
    {
        return value *= 2;
    }
}
