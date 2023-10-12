using PSW.Core.Enums;
using System.Collections;
using UnityEngine;

/// <summary>
/// 해골 몬스터 스킬 카드
/// </summary>
public class BattleEnemySkill_Skull : BattleEnemySkill
{
    [Header("Card")]
    [SerializeField] private ItemBlueprintCard card;
    [SerializeField] private int capacity = 1;

    public override IEnumerator UseSkill()
    {
        UseSkillByType();

        yield return StartCoroutine(CreateSkull());

        yield return YieldCache.WaitForSeconds(0.3f);

        Disable();
    }

    public override void CheckSkill()
    {
        int skullCount = this.gameBoard.ObstacleCardsCount(CardDetailType.Skull);
        this.resultSkillValue = this.skillValue + skullCount;

        base.CheckSkill();
    }

    /// <summary>
    /// 장애물 카드(해골) 생성
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreateSkull()
    {
        if (this.card == null) yield return null;

        for (int i = 0; i < capacity; i++)
        {
            yield return GameBoard.Instance.ObstacleCardSpawn(this.card);
        }
    }
}
