using PSW.Core.Enums;
using System.Collections;
using UnityEngine;

/// <summary>
/// �ذ� ���� ��ų ī��
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
    /// ��ֹ� ī��(�ذ�) ����
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
