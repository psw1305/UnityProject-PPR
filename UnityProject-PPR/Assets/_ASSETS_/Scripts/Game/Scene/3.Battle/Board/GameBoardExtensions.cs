using UnityEngine;

public static class GameBoardExtensions
{
    public static GameBoardCard GetElement(this GameBoard board, int column, int row)
    {
        return board.Cards[row * board.ColumnCount + column];
    }

    public static Vector2 BoardToWorldPosition(this GameBoard board, int column, int row)
    {
        return board.StartCellPosition + board.Rescaling * new Vector2(column, row);
    }

    public static Vector2 BoardToWorldPosition(this GameBoard board, Vector2Int boardPos)
    {
        return board.BoardToWorldPosition(boardPos.x, boardPos.y);
    }

    public static int BoardPositionToIndex(this GameBoard board, Vector2Int boardPos)
    {
        return board.ColumnCount * boardPos.y + boardPos.x;
    }
}
