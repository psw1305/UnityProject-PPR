using PSW.Core.Enums;
using System.Collections.Generic;

public static class BattlePlayerExtenstions
{
    public static int GetElementPoint(this BattlePlayer battlePlayer, List<GameBoardElement> elements, int plusPoint = 1)
    {
        int resultPoint = 0;

        foreach (GameBoardElement element in elements)
        {
            if (element.ElementType == ElementType.Synergy)
            {
                resultPoint *= 2;
            }
            else
            {
                resultPoint += plusPoint;
            }
        }

        return resultPoint;
    }
}
