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

    public void SetText(List<GameBoardCard> selectedElements)
    {
        int elementPoint = 0;

        switch (this.battleSystem.PlayedElementType)
        {
            case CardType.Attack:
                elementPoint = DetailSetText(selectedElements, this.battlePlayer.FirstATK, this.battlePlayer.ATK);
                break;
            case CardType.Defense:
                elementPoint = DetailSetText(selectedElements, this.battlePlayer.FirstDEF, this.battlePlayer.DEF);
                break;
            case CardType.None:
                elementPoint = 0;
                break;
        }
;
        this.countingText.text = elementPoint.ToString();
    }

    private int DetailSetText(List<GameBoardCard> selectedElements, int firstPoint, int point)
    {
        int resultPoint;

        if (this.battlePlayer.OnFirst)
        {
            resultPoint = this.battlePlayer.GetPoint(selectedElements, firstPoint, point);
        }
        else
        {
            resultPoint = this.battlePlayer.GetPoint(selectedElements, 0, point);
        }

        return resultPoint;
    }

    /// <summary>
    /// 해당 element 위치로 포지션 조정 [InGame : UI = 1 : 100]
    /// </summary>
    /// <param name="element"></param>
    public void SetPosition(GameBoardCard element)
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
