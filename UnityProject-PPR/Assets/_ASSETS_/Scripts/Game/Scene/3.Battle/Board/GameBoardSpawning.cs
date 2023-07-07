using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardSpawning
{
    private GameBoard board;
    private int skillStack;
    private int currentStack = 1;

    public GameBoardSpawning(GameBoard board, int skillStack)
    {
        this.board = board;
        this.skillStack = skillStack;
    }

    public IEnumerator Respawn()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        foreach (GameBoardElement element in this.board.Elements)
        {
            if (element.IsSpawned == false)
            {
                element.SetData(this.board.ElementList.Get());
                element.Spawn();

                if (this.currentStack >= this.skillStack)
                {
                    if (this.board.IsSkillElementsEmpty() == false)
                    {
                        this.currentStack = 1;
                        yield return Change(this.board.RandomElement());
                    }
                }
                else
                {
                    this.currentStack++;
                }
            }
        }

        yield return new WaitForSeconds(0.4f);
    }

    public IEnumerator Despawn(List<GameBoardElement> elements)
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        foreach (GameBoardElement boardElement in elements)
        {
            boardElement.Despawn();
        }

        yield return new WaitForSeconds(0.4f);    

        // 행동이 끝나고 elements가 사라질 때 event 발동
        GameBoardEvents.OnElementsDespawned.Invoke();
    }

    public IEnumerator Change(GameBoardElement element)
    {
        element.Despawn();

        yield return new WaitForSeconds(0.2f);

        BattleSFX.Instance.Play(BattleSFX.Instance.skillAppear);

        element.SetData(this.board.RandomSkillElement());
        element.Spawn();
    }
}
