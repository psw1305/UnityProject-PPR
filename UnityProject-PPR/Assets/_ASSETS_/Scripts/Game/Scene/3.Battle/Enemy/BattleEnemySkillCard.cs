using PSW.Core.Enums;
using System.Collections;
using UnityEngine;

public class BattleEnemySkillCard : BattleEnemySkill
{
    [Header("Card")]
    [SerializeField] private ItemBlueprintCard card;
    [SerializeField] private int capacity = 1;

    public override IEnumerator UseSkill()
    {
        UseSkillByType();

        yield return StartCoroutine(CardCreate());

        yield return YieldCache.WaitForSeconds(0.3f);

        Disable();
    }

    public override void CheckSkill()
    {
        if (this.card.CardDetailType == CardDetailType.Skull)
        {
            int skullCount = this.gameBoard.ObstacleCardsCount(this.card.CardDetailType);
            this.resultSkillValue = this.skillValue + skullCount;
        }

        base.CheckSkill();
    }

    private IEnumerator CardCreate()
    {
        if (this.card == null) yield return null;

        for (int i = 0; i < capacity; i++)
        {
            yield return GameBoard.Instance.ObstacleCardSpawn(this.card);
        }
    }
}
