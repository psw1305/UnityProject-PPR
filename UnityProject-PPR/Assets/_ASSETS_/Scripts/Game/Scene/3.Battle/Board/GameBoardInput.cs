using PSW.Core.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// GameBoard 터치 조작
/// </summary>
public class GameBoardInput
{
    private GameBoard board;
    private GameBoardCountingText countingText;
    private GameBoardSelectionLine selectionLine;

    public List<GameBoardElement> SelectedElements { get; private set; }

    public GameBoardInput(GameBoard board, GameBoardCountingText countingText, GameBoardSelectionLine selectionLine)
    {
        this.board = board;
        this.countingText = countingText;
        this.selectionLine = selectionLine;
        this.SelectedElements = new List<GameBoardElement>();
    }

    private void Clear()
    {
        this.countingText.Clear();
        this.selectionLine.Clear();
        this.SelectedElements.Clear();
    }

    public IEnumerator WaitForSelection()
    {
        Clear();

        while (true)
        {
            yield return null;

            // 화면을 클릭, 터치했는가? => 1회
            if (Input.GetMouseButtonDown(0))
            {
                // 클릭한 위치에 element가 존재하는가?
                if (InputRaycast(out BattlePlayerUseableItem item))
                {
                    ProcessBoardItem(item);
                }
            }

            // 화면을 클릭, 터치했는가? => 지속
            if (Input.GetMouseButton(0))
            {
                // 클릭한 위치에 element가 존재하는가?
                if (InputRaycast(out GameBoardElement element))
                {
                    ProcessBoardElement(element);
                }
            }
            // selection을 마쳤는가?
            else if (this.SelectedElements.Count > 0)
            {
                if (IsValidInput())
                {
                    this.countingText.Clear();
                    this.selectionLine.Clear();
                    
                    // 선 긋기 완료
                    yield break;
                }
                // 다시 선택 전으로 돌아갈 경우
                else
                {    
                    this.SelectedElements[0].Deselected();
                    Clear();
                }
            }
        }
    }

    /// <summary>
    /// 해당 클릭,터치 위치에 elements 존재시 true, 다른거라면 false
    /// 초기화를 하지 않아도 값을 호출하도록 out 사용
    /// out => 터치 된 elements, null => 나머지
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    private bool InputRaycast(out GameBoardElement element)
    {
        element = null;  

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);     
        var hitInfo = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hitInfo)
        {
            element = hitInfo.transform.GetComponent<GameBoardElement>();

            // 해당 마우스, 터치 위치에 GameBoardElement 존재 => true
            if (element != null)
            {
                return true;
            }
        }

        return false;
    }

    private bool InputRaycast(out BattlePlayerUseableItem item)
    {
        item = null;
        // get 마우스 위치
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // z-level로 raycast 
        RaycastHit2D hitInfo = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hitInfo)
        {
            item = hitInfo.transform.GetComponent<BattlePlayerUseableItem>();

            // 해당 마우스, 터치 위치에 GameBoardElement 존재 => true
            if (item != null)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 선택된 element 조작 프로세스
    /// </summary>
    /// <param name="element"></param>
    private void ProcessBoardElement(GameBoardElement element)
    {
        // 아직 element 하나도 선택되지 않았는가?
        if (this.SelectedElements.Count == 0)
        {
            // 맨 처음 element 선택 시작
            FirstElementToSelection(element);
        }
        else
        {
            // element가 선택된 준비가 되었는가?
            if (this.SelectedElements.Contains(element))
            {
                // 플레이가 다시 전 element로 가는가?
                if (IsSecondLast(element))
                {
                    // 선택된 마지막 element 취소
                    DeselectLast();
                }
            }
            // 선택되지 않음
            else if (IsSelectable(element))
            {
                AddElementToSelection(element);
            }
        }
    }

    /// <summary>
    /// 선택된 element 조작 프로세스
    /// </summary>
    /// <param name="item"></param>
    private void ProcessBoardItem(BattlePlayerUseableItem item)
    {
        item.Used(this.board);
    }

    private bool IsSecondLast(GameBoardElement element)
    {
        return this.SelectedElements.Count >= 2 && element.Equals(this.SelectedElements[this.SelectedElements.Count - 2]) == true;
    }

    /// <summary>
    /// 다음 selection line 이을 때 2가지 bool 체크
    /// 1. 같은 element type 인가?
    /// 2. elements 사이 거리가 충분히 되는가?
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    private bool IsSelectable(GameBoardElement element)
    {
        return HasValidElementType(element) && IsInDistance(element);
    }

    /// <summary>
    /// 같은 element type인지 체크
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    private bool HasValidElementType(GameBoardElement element)
    {
        // 타입이 시너지 일 경우 통과
        if (element.ElementType == ElementType.Synergy) return true;
        // 시너지 타입에서 다시 이을 경우 => 기존 선택된 타입으로 변경
        if (this.selectionLine.ElementType == ElementType.Synergy)
        {
            this.selectionLine.ElementType = BattleSystem.Instance.PlayedElementType;
        }

        return element.ElementType == this.selectionLine.ElementType;
    }

    /// <summary>
    /// 이을려는 elements 끼리 거리가 일정 수치 이하인지 체크 
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    private bool IsInDistance(GameBoardElement element)
    {
        var lastElementPos = this.SelectedElements.Last().transform.position;
        return Vector2.Distance(element.transform.position, lastElementPos) < 1.5f * this.board.Rescaling;
    }

    /// <summary>
    /// 맨 처음으로 element 체크 함수
    /// </summary>
    /// <param name="element"></param>
    private void FirstElementToSelection(GameBoardElement element)
    {
        // 맨 처음 시너지 타입 선택 제외
        if (element.ElementType == ElementType.Synergy) return;

        BattleSFX.Instance.Play(BattleSFX.Instance.elementClick, 1.0f);
        BattleSystem.Instance.PlayedElementType = element.ElementType;
        
        this.countingText.Select();
        AddElementToSelection(element);
    }

    /// <summary>
    /// 주어진 element에 selection 추가
    /// </summary>
    /// <param name="element"></param>
    private void AddElementToSelection(GameBoardElement element)
    {
        if (BattlePlayer.CurrentACT <= 0) return;

        if (this.SelectedElements.Count > 0)
        {
            var pitch = 1.0f + this.SelectedElements.Count * 0.1f;
            BattleSFX.Instance.Play(BattleSFX.Instance.elementClick, pitch);
            
            // 행동 횟수 차감
            BattlePlayer.Instance.ActionCounter(-1);
        }

        element.Selected();
        this.SelectedElements.Add(element);
        
        this.countingText.SetPosition(element);
        this.countingText.SetText(this.SelectedElements);
        this.selectionLine.ElementType = element.ElementType;
        this.selectionLine.SetPosition(this.SelectedElements);
    }

    /// <summary>
    /// 마지막으로 선택된 element에서 전 element로 돌아갈 경우 => 선택된 element 지우기
    /// </summary>
    private void DeselectLast()
    {
        var pitch = 1.0f + this.SelectedElements.Count * 0.1f;
        BattleSFX.Instance.Play(BattleSFX.Instance.elementClick, pitch);

        // 행동 횟수 복귀
        BattlePlayer.Instance.ActionCounter(1);

        var lastElement = this.SelectedElements.Last();
        lastElement.Deselected();
        this.SelectedElements.Remove(lastElement);

        this.countingText.SetPosition(this.SelectedElements.Last());
        this.countingText.SetText(this.SelectedElements);
        this.selectionLine.SetPosition(this.SelectedElements);
    }

    private bool IsValidInput()
    {
        return this.SelectedElements.Count > 1 ? true : false;
    }
}
