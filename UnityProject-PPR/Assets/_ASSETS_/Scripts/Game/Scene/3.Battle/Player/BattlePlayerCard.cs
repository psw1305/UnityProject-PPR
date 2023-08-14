using PSW.Core.Enums;
using System.Collections.Generic;

public static class BattlePlayerCard
{
    public static int GetElementPoint(this BattlePlayer battleplayer, List<GameBoardCard> cards, int firstPoint, int plusPoint)
    {
        int resultPoint = firstPoint;

        foreach (var card in cards)
        {
            if (card.CardDetail == CardDetail.Instant)
            {
                if (card.CardType == CardType.Attack)
                {
                    resultPoint = AttackSkill(card.GetCardName(), resultPoint);
                }
                else if (card.CardType == CardType.Defense)
                {
                    resultPoint = DefenseSkill(card.GetCardName(), resultPoint);
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
