using PSW.Core.Enums;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GameBoardSelectionLine : MonoBehaviour
{
    private LineRenderer lineRenderer = null;
    
    private CardType elementType = CardType.None;
    public CardType ElementType 
    {
        get { return this.elementType; }
        set { this.elementType = value; }
    }

    private void Awake()
    {
        this.lineRenderer = GetComponent<LineRenderer>();
    }

    /// <summary>
    /// 선택된 elements 수 끼리 Line 위치 설정
    /// </summary>
    /// <param name="selectedElements"></param>
    public void SetPosition(List<GameBoardCard> selectedElements)
    {
        this.lineRenderer.positionCount = selectedElements.Count;

        for (int i = 0; i < selectedElements.Count; i++)
        {
            this.lineRenderer.SetPosition(i, selectedElements[i].transform.position);
        }
    }

    public void Clear()
    {
        this.ElementType = CardType.None;
        this.lineRenderer.positionCount = 0;
    }
}
