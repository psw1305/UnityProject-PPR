using PSW.Core.Enums;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameBoardCountingText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countingText;

    public void Select()
    {
        this.countingText.DOFade(1.0f, 0.1f);
    }

    public void SetText(List<GameBoardElement> selectedElements)
    {
        int result = 0;

        switch (BattleSystem.Instance.PlayedElementType)
        {
            case ElementType.Attack:
                result = BattlePlayer.Instance.GetElementPoint(selectedElements, BattlePlayer.Instance.ATK);
                break;
            case ElementType.Defense:
                result = BattlePlayer.Instance.GetElementPoint(selectedElements, BattlePlayer.Instance.DEF);
                break;
            //case ElementType.Potion:
            //    result = BattlePlayer.Instance.GetElementPoint(selectedElements);
            //    break;
            //case ElementType.Coin:
            //    result = BattlePlayer.Instance.GetElementPoint(selectedElements);
            //    break;
            case ElementType.None:
                result = 0;
                break;
        }
;
        this.countingText.text = result.ToString();
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
