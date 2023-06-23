using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardSpawning
{
    private GameBoard board;
    public GameBoardSpawning(GameBoard board)
    {
        this.board = board;
    }

    public IEnumerator RespawnElements()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        foreach (GameBoardElement element in this.board.Elements)
        {
            if (element.IsSpawned == false)
            {
                element.SetBaseData(this.board.ElementTypeList.Get());
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

        // �ൿ�� ������ elements�� ����� �� event �ߵ�
        GameBoardEvents.OnElementsDespawned.Invoke();
    }
}
