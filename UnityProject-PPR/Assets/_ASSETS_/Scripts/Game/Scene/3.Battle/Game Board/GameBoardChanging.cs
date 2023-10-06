using System.Collections;

/// <summary>
/// Game Board ������ ��ų
/// </summary>
public class GameItemSkill
{
    /// <summary>
    /// Board ���� �ִ� ��� elements type ��ü
    /// </summary>
    /// <returns></returns>
    public IEnumerator AllElementsChanged(GameBoard board, ItemBlueprintCard changeCard)
    {
        // ȿ���� �ο�
        //GameUISFX.Instance.Play(GameUISFX.Instance.clickClip);

        foreach (var element in board.Cards)
        {
            element.Despawn();
        }

        yield return YieldCache.WaitForSeconds(0.25f);

        foreach (var element in board.Cards)
        {
            element.SetData(board.GameBoardCards.Get());
            element.Spawn();
        }

        yield return YieldCache.WaitForSeconds(0.25f);
    }
}