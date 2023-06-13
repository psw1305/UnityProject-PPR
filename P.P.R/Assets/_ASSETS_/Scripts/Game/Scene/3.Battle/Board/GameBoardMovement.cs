using System.Collections;
using UnityEngine;

public class GameBoardMovement
{
    private GameBoard board;

    public GameBoardMovement(GameBoard board)
    {
        this.board = board;
    }

    public IEnumerator WaitForMovement()
    {
        MoveElements();

        while (IsMovementDone() == false)
        {
            yield return new WaitForSeconds(0.05f);
        }

        yield break;
    }

    private bool IsMovementDone()
    {
        foreach (GameBoardElement element in this.board.Elements)
        {
            if (element.IsSpawned && element.IsMoving)
            {
                return false;
            }
        }

        return true;
    }

    private void MoveElements()
    {
        for (int y = 0; y < this.board.RowCount; y++)
        {
            for (int x = 0; x < this.board.ColumnCount; x++)
            {
                ProcessCell(x, y);
            }
        }
    }

    private void ProcessCell(int column, int row)
    {
        var element = this.board.GetElement(column, row);

        if (element.IsSpawned == false)
        {
            for (int i = row + 1; i < this.board.RowCount; i++)
            {
                var next = this.board.GetElement(column, i);

                if (next != null && next.IsSpawned && !next.IsMoving)
                {
                    MoveElement(new Vector2Int(column, i), new Vector2Int(column, row));

                    return;
                }
            }
        }
    }

    private void MoveElement(Vector2Int oldPos, Vector2Int newPos)
    {
        var element1 = this.board.GetElement(oldPos.x, oldPos.y);
        var element2 = this.board.GetElement(newPos.x, newPos.y);

        this.board.Elements[this.board.BoardPositionToIndex(oldPos)] = element2;
        this.board.Elements[this.board.BoardPositionToIndex(newPos)] = element1;

        element2.transform.position = this.board.BoardToWorldPosition(oldPos);
        element1.Move(this.board.BoardToWorldPosition(newPos), 0.3f);
    }
}
