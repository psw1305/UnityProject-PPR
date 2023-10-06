using System.Collections;
using System.Collections.Generic;

public class GameBoardSpawning
{
    private readonly GameBoard board;
    private readonly int skillStack;
    private int currentStack = 1;

    public GameBoardSpawning(GameBoard board, int skillStack)
    {
        this.board = board;
        this.skillStack = skillStack;
    }

    public IEnumerator Respawn()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        foreach (GameBoardCard element in this.board.Cards)
        {
            if (element.IsSpawned == false)
            {
                element.SetData(this.board.GameBoardCards.Get());
                element.Spawn();

                // 스택에 따른 스킬 카드 생성
                if (this.currentStack >= this.skillStack)
                {
                    yield return SkillCardSpawn();
                }
                else
                {
                    this.currentStack++;
                }
            }
        }

        yield return YieldCache.WaitForSeconds(0.4f);
    }

    /// <summary>
    /// 스킬 카드 생성
    /// </summary>
    /// <returns></returns>
    public IEnumerator SkillCardSpawn()
    {
        if (this.board.IsSkillCardEmpty() == false)
        {
            this.currentStack = 1;

            yield return Change(this.board.RandomCardFromNormal(), this.board.SkillCardRandomPickUp());
        }
    }

    /// <summary>
    /// 장애물 카드 생성
    /// </summary>
    /// <returns></returns>
    public IEnumerator ObstacleCardSpawn(ItemBlueprintCard obstacleCard)
    {
        

        yield return Change(this.board.RandomCardFromNormal(), obstacleCard);
    }

    public IEnumerator Despawn(List<GameBoardCard> elements)
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        foreach (GameBoardCard boardElement in elements)
        {
            boardElement.Despawn();
        }

        yield return YieldCache.WaitForSeconds(0.4f);    

        // 행동이 끝나고 elements가 사라질 때 event 발동
        GameBoardEvents.OnElementsDespawned.Invoke();
    }

    public IEnumerator Change(GameBoardCard element, ItemBlueprintCard card)
    {
        if (element == null) yield break;

        element.Despawn();

        yield return YieldCache.WaitForSeconds(0.2f);

        BattleSFX.Instance.Play(BattleSFX.Instance.skillAppear);

        element.SetData(card);
        element.Spawn();
    }
}
