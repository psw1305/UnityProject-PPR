using System.Collections;
using UnityEngine;

/// <summary>
/// Game Board ������ ��ų
/// </summary>
public class GameItemSkill
{
    /// <summary>
    /// Board ���� �ִ� ��� elements type ��ü
    /// </summary>
    /// <returns></returns>
    public IEnumerator AllElementsChanged(GameBoard board, ElementBlueprint changeElement)
    {
        // ȿ���� �ο�
        //GameUISFX.Instance.Play(GameUISFX.Instance.clickClip);

        foreach (GameBoardElement element in board.Elements)
        {
            element.Despawn();
        }

        yield return new WaitForSeconds(0.25f);

        foreach (GameBoardElement element in board.Elements)
        {
            element.SetBaseData(board.ElementTypeList.Get());
            element.Spawn();
        }

        yield return new WaitForSeconds(0.25f);
    }
}