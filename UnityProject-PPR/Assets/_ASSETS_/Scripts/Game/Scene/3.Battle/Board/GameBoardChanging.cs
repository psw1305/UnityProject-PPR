using System.Collections;

/// <summary>
/// Game Board 아이템 스킬
/// </summary>
public class GameItemSkill
{
    /// <summary>
    /// Board 위에 있는 모든 elements type 교체
    /// </summary>
    /// <returns></returns>
    public IEnumerator AllElementsChanged(GameBoard board, ElementBlueprint changeElement)
    {
        // 효과음 부여
        //GameUISFX.Instance.Play(GameUISFX.Instance.clickClip);

        foreach (GameBoardElement element in board.Elements)
        {
            element.Despawn();
        }

        yield return YieldCache.WaitForSeconds(0.25f);

        foreach (GameBoardElement element in board.Elements)
        {
            element.SetData(board.ElementList.Get());
            element.Spawn();
        }

        yield return YieldCache.WaitForSeconds(0.25f);
    }
}