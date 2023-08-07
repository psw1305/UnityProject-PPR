using PSW.Core.Enums;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameBoardCountingText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countingText;

    [Header("Script")]
    [SerializeField] private BattleSystem battleSystem;
    [SerializeField] private BattlePlayer battlePlayer;

    public void Select()
    {
        this.countingText.DOFade(1.0f, 0.1f);
    }

    public void SetText(List<GameBoardElement> selectedElements)
    {
        int elementPoint = 0;

        switch (this.battleSystem.PlayedElementType)
        {
            case ElementType.Attack:
                elementPoint = DetailSetText(selectedElements, this.battlePlayer.FirstATK, this.battlePlayer.ATK);
                break;
            case ElementType.Defense:
                elementPoint = DetailSetText(selectedElements, this.battlePlayer.FirstDEF, this.battlePlayer.DEF);
                break;
            case ElementType.None:
                elementPoint = 0;
                break;
        }
;
        this.countingText.text = elementPoint.ToString();
    }

    private int DetailSetText(List<GameBoardElement> selectedElements, int firstPoint, int point)
    {
        int resultPoint;

        if (this.battlePlayer.OnFirst)
        {
            resultPoint = this.battlePlayer.GetElementPoint(selectedElements, firstPoint, point);
        }
        else
        {
            resultPoint = this.battlePlayer.GetElementPoint(selectedElements, 0, point);
        }

        return resultPoint;
    }

    /// <summary>
    /// 해당 element 위치로 포지션 조정 [InGame : UI = 1 : 100]
    /// </summary>
    /// <param name="element"></param>
    public void SetPosition(GameBoardElement element)
    {
        Vector2 boardPosition = element.transform.position;
        Vector2 textPosition = boardPosition * 100.0f;
        this.transform.localPosition = new Vector3(textPosition.x, textPosition.y + 100);
    }

    public void Clear()
    {
        this.countingText.DOFade(0.0f, 0.1f);
    }
}
