using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardSpawning
{
    private GameBoard board;
    private int skillStack;
    private int currentStack = 0;

    public GameBoardSpawning(GameBoard board, int skillStack)
    {
        this.board = board;
        this.skillStack = skillStack;
    }

    public IEnumerator RespawnElements()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        foreach (GameBoardElement element in this.board.Elements)
        {
            if (element.IsSpawned == false)
            {
                this.currentStack++;

                if (this.currentStack < this.skillStack)
                {
                    element.SetBaseData(this.board.ElementList.Get());
                }
                else
                {
                    element.SetBaseData(this.board.ElementSkillList.Get());
                    this.currentStack = 0;
                }

                element.Spawn();
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
}
