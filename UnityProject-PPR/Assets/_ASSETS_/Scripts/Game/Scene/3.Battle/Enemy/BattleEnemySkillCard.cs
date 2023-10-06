using System.Collections;
using UnityEngine;

public class BattleEnemySkillCard : BattleEnemySkill
{
    [Header("Card")]
    [SerializeField] private ItemBlueprintCard card;
    [SerializeField] private int capacity = 1;

    public override IEnumerator Use(BattleEnemy battleEnemy)
    {
        UseBySkillType(battleEnemy);

        yield return StartCoroutine(CardCreate());

        yield return YieldCache.WaitForSeconds(0.3f);

        Disable();
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
