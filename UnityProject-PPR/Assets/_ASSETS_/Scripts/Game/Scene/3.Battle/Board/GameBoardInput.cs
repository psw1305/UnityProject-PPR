using PSW.Core.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// GameBoard ��ġ ����
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

            // ȭ���� Ŭ��, ��ġ�ߴ°�? => 1ȸ
            if (Input.GetMouseButtonDown(0))
            {
                // Ŭ���� ��ġ�� element�� �����ϴ°�?
                if (InputRaycast(out BattlePlayerUseableItem item))
                {
                    ProcessBoardItem(item);
                }
            }

            // ȭ���� Ŭ��, ��ġ�ߴ°�? => ����
            if (Input.GetMouseButton(0))
            {
                // Ŭ���� ��ġ�� element�� �����ϴ°�?
                if (InputRaycast(out GameBoardElement element))
                {
                    ProcessBoardElement(element);
                }
            }
            // selection�� ���ƴ°�?
            else if (this.SelectedElements.Count > 0)
            {
                if (IsValidInput())
                {
                    this.countingText.Clear();
                    this.selectionLine.Clear();
                    
                    // �� �߱� �Ϸ�
                    yield break;
                }
                // �ٽ� ���� ������ ���ư� ���
                else
                {    
                    this.SelectedElements[0].Deselected();
                    Clear();
                }
            }
        }
    }

    /// <summary>
    /// �ش� Ŭ��,��ġ ��ġ�� elements ����� true, �ٸ��Ŷ�� false
    /// �ʱ�ȭ�� ���� �ʾƵ� ���� ȣ���ϵ��� out ���
    /// out => ��ġ �� elements, null => ������
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

            // �ش� ���콺, ��ġ ��ġ�� GameBoardElement ���� => true
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
        // get ���콺 ��ġ
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // z-level�� raycast 
        RaycastHit2D hitInfo = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hitInfo)
        {
            item = hitInfo.transform.GetComponent<BattlePlayerUseableItem>();

            // �ش� ���콺, ��ġ ��ġ�� GameBoardElement ���� => true
            if (item != null)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// ���õ� element ���� ���μ���
    /// </summary>
    /// <param name="element"></param>
    private void ProcessBoardElement(GameBoardElement element)
    {
        // ���� element �ϳ��� ���õ��� �ʾҴ°�?
        if (this.SelectedElements.Count == 0)
        {
            // �� ó�� element ���� ����
            FirstElementToSelection(element);
        }
        else
        {
            // element�� ���õ� �غ� �Ǿ��°�?
            if (this.SelectedElements.Contains(element))
            {
                // �÷��̰� �ٽ� �� element�� ���°�?
                if (IsSecondLast(element))
                {
                    // ���õ� ������ element ���
                    DeselectLast();
                }
            }
            // ���õ��� ����
            else if (IsSelectable(element))
            {
                AddElementToSelection(element);
            }
        }
    }

    /// <summary>
    /// ���õ� element ���� ���μ���
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
    /// ���� selection line ���� �� 2���� bool üũ
    /// 1. ���� element type �ΰ�?
    /// 2. elements ���� �Ÿ��� ����� �Ǵ°�?
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    private bool IsSelectable(GameBoardElement element)
    {
        return HasValidElementType(element) && IsInDistance(element);
    }

    /// <summary>
    /// ���� element type���� üũ
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    private bool HasValidElementType(GameBoardElement element)
    {
        // Ÿ���� �ó��� �� ��� ���
        if (element.ElementType == ElementType.Synergy) return true;
        // �ó��� Ÿ�Կ��� �ٽ� ���� ��� => ���� ���õ� Ÿ������ ����
        if (this.selectionLine.ElementType == ElementType.Synergy)
        {
            this.selectionLine.ElementType = BattleSystem.Instance.PlayedElementType;
        }

        return element.ElementType == this.selectionLine.ElementType;
    }

    /// <summary>
    /// �������� elements ���� �Ÿ��� ���� ��ġ �������� üũ 
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    private bool IsInDistance(GameBoardElement element)
    {
        var lastElementPos = this.SelectedElements.Last().transform.position;
        return Vector2.Distance(element.transform.position, lastElementPos) < 1.5f * this.board.Rescaling;
    }

    /// <summary>
    /// �� ó������ element üũ �Լ�
    /// </summary>
    /// <param name="element"></param>
    private void FirstElementToSelection(GameBoardElement element)
    {
        // �� ó�� �ó��� Ÿ�� ���� ����
        if (element.ElementType == ElementType.Synergy) return;

        BattleSFX.Instance.Play(BattleSFX.Instance.elementClick, 1.0f);
        BattleSystem.Instance.PlayedElementType = element.ElementType;
        
        this.countingText.Select();
        AddElementToSelection(element);
    }

    /// <summary>
    /// �־��� element�� selection �߰�
    /// </summary>
    /// <param name="element"></param>
    private void AddElementToSelection(GameBoardElement element)
    {
        if (BattlePlayer.CurrentACT <= 0) return;

        if (this.SelectedElements.Count > 0)
        {
            var pitch = 1.0f + this.SelectedElements.Count * 0.1f;
            BattleSFX.Instance.Play(BattleSFX.Instance.elementClick, pitch);
            
            // �ൿ Ƚ�� ����
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
    /// ���������� ���õ� element���� �� element�� ���ư� ��� => ���õ� element �����
    /// </summary>
    private void DeselectLast()
    {
        var pitch = 1.0f + this.SelectedElements.Count * 0.1f;
        BattleSFX.Instance.Play(BattleSFX.Instance.elementClick, pitch);

        // �ൿ Ƚ�� ����
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
