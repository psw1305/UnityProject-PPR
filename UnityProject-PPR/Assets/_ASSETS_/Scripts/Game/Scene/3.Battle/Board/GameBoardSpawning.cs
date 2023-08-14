using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardSpawning
{
    private GameBoard board;
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
                element.SetData(this.board.CardList.Get());
                element.Spawn();

                if (this.currentStack >= this.skillStack)
                {
                    if (this.board.IsSkillCardEmpty() == false)
                    {
                        this.currentStack = 1;
                        yield return Change(this.board.RandomCard());
                    }
                }
                else
                {
                    this.currentStack++;
                }
            }
        }

        yield return YieldCache.WaitForSeconds(0.4f);
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

    public IEnumerator Change(GameBoardCard element)
    {
        element.Despawn();

        yield return YieldCache.WaitForSeconds(0.2f);

        BattleSFX.Instance.Play(BattleSFX.Instance.skillAppear);

        element.SetData(this.board.RandomSkillCard());
        element.Spawn();
    }
}
