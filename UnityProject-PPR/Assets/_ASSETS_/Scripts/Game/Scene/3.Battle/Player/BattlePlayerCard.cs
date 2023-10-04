using PSW.Core.Enums;
using System.Collections.Generic;

public static class BattlePlayerCard
{
    public static int GetPoint(this BattlePlayer battleplayer, List<GameBoardCard> cards, int firstPoint, int plusPoint)
    {
        var point = firstPoint;
        var last = cards.Count - 1;

        for (int i = 0; i < cards.Count; i++)
        {
            // 첫 카드 능력이 Ready이면 발동
            if (i == 0 && cards[i].CardDetail == CardDetailType.Ready)
            {
                point = Card_Ready(cards[i].GetCardName(), point);
            }
            else if (cards[i].CardDetail == CardDetailType.Instant)
            {
                point = Card_Instant(cards[i].GetCardName(), point);
            }
            // 마지막 카드 능력이 Finish이면 발동
            else if (i == last && cards[i].CardDetail == CardDetailType.Finish)
            {
                point = Card_Finish(cards[i].GetCardName(), point);
            }
            else
            {
                point += plusPoint;
            }
        }

        return point;
    }

    #region Card - Ready
    /// <summary>
    /// Ready 카드 능력
    /// </summary>
    public static int Card_Ready(string skillName, int value)
    {
        return skillName switch
        {
            "Flex" => Flex(value),
            _ => 1,
        };
    }


    public static int Flex(int value)
    {
        return value += 3;
    }
    #endregion

    #region Card - Instant
    /// <summary>
    /// Instant 카드 능력
    /// </summary>
    public static int Card_Instant(string skillName, int value)
    {
        return skillName switch
        {
            "Strike" => Strike(value),
            "Strike-Powerful" => PowerfulStrike(value),

            "Defend" => Defend(value),
            "Defend-Perfected" => PerfectedDefend(value),

            _ => value,
        };
    }

    public static int Strike(int value)
    {
        return value += 4;
    }

    public static int PowerfulStrike(int value)
    {
        return value += 8;
    }

    public static int Defend(int value)
    {
        return value += 4;
    }

    public static int PerfectedDefend(int value)
    {
        return value += 8;
    }
    #endregion

    #region Card - Finish
    /// <summary>
    /// Finish 카드 능력
    /// </summary>
    public static int Card_Finish(string skillName, int value)
    {
        return skillName switch
        {
            "Anger" => Anger(value),

            "Entrench" => Entrench(value),
            "ShieldBash" => ShieldBash(value),

            _ => value,
        };
    }


    public static int Anger(int value)
    {
        return value *= 2;
    }

    public static int Entrench(int value)
    {
        return value *= 2;
    }

    public static int ShieldBash(int value)
    {
        return value *= 2;
    }
    #endregion
}
