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

    public List<GameBoardCard> SelectedCards { get; private set; }

    public GameBoardInput(GameBoard board, GameBoardCountingText countingText, GameBoardSelectionLine selectionLine)
    {
        this.board = board;
        this.countingText = countingText;
        this.selectionLine = selectionLine;
        this.SelectedCards = new List<GameBoardCard>();
    }

    private void Clear()
    {
        this.countingText.Clear();
        this.selectionLine.Clear();
        this.SelectedCards.Clear();
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
                if (InputRaycast(out BattlePlayerPotion item))
                {
                    ProcessBoardItem(item);
                }
            }

            // ȭ���� Ŭ��, ��ġ�ߴ°�? => ����
            if (Input.GetMouseButton(0))
            {
                // Ŭ���� ��ġ�� element�� �����ϴ°�?
                if (InputRaycast(out GameBoardCard element))
                {
                    ProcessBoardElement(element);
                }
            }
            // selection�� ���ƴ°�?
            else if (this.SelectedCards.Count > 0)
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
                    this.SelectedCards[0].Deselected();
                    BattlePlayer.Instance.ActionCounter(1);
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
    private bool InputRaycast(out GameBoardCard element)
    {
        element = null;  

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);     
        var hitInfo = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hitInfo)
        {
            element = hitInfo.transform.GetComponent<GameBoardCard>();

            // �ش� ���콺, ��ġ ��ġ�� GameBoardElement ���� => true
            if (element != null)
            {
                return true;
            }
        }

        return false;
    }

    private bool InputRaycast(out BattlePlayerPotion item)
    {
        item = null;
        // get ���콺 ��ġ
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // z-level�� raycast 
        RaycastHit2D hitInfo = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hitInfo)
        {
            item = hitInfo.transform.GetComponent<BattlePlayerPotion>();

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
    private void ProcessBoardElement(GameBoardCard element)
    {
        // ���� element �ϳ��� ���õ��� �ʾҴ°�?
        if (this.SelectedCards.Count == 0)
        {
            // �� ó�� element ���� ����
            FirstElementToSelection(element);
        }
        else
        {
            // element�� ���õ� �غ� �Ǿ��°�?
            if (this.SelectedCards.Contains(element))
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
    private void ProcessBoardItem(BattlePlayerPotion item)
    {
        item.Used(this.board);
    }

    private bool IsSecondLast(GameBoardCard element)
    {
        return this.SelectedCards.Count >= 2 && element.Equals(this.SelectedCards[this.SelectedCards.Count - 2]) == true;
    }

    /// <summary>
    /// ���� selection line ���� �� 2���� bool üũ
    /// 1. ���� element type �ΰ�?
    /// 2. elements ���� �Ÿ��� ����� �Ǵ°�?
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    private bool IsSelectable(GameBoardCard element)
    {
        return HasValidElementType(element) && IsInDistance(element);
    }

    /// <summary>
    /// ���� element type���� üũ
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    private bool HasValidElementType(GameBoardCard element)
    {
        // Ÿ���� �ó��� �� ��� ���
        if (element.CardType == CardType.Synergy) return true;
        // �ó��� Ÿ�Կ��� �ٽ� ���� ��� => ���� ���õ� Ÿ������ ����
        if (this.selectionLine.ElementType == CardType.Synergy)
        {
            this.selectionLine.ElementType = BattleSystem.Instance.PlayedElementType;
        }

        return element.CardType == this.selectionLine.ElementType;
    }

    /// <summary>
    /// �������� elements ���� �Ÿ��� ���� ��ġ �������� üũ 
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    private bool IsInDistance(GameBoardCard element)
    {
        var lastElementPos = this.SelectedCards.Last().transform.position;
        return Vector2.Distance(element.transform.position, lastElementPos) < 1.5f * this.board.Rescaling;
    }

    /// <summary>
    /// �� ó������ element üũ �Լ�
    /// </summary>
    /// <param name="element"></param>
    private void FirstElementToSelection(GameBoardCard element)
    {
        // �� ó�� �ó��� Ÿ�� ���� ����
        if (element.CardType == CardType.Synergy) return;

        BattleSFX.Instance.Play(BattleSFX.Instance.elementClick, 1.0f);
        BattleSystem.Instance.PlayedElementType = element.CardType;
        
        this.countingText.Select();
        AddElementToSelection(element);
    }

    /// <summary>
    /// �־��� element�� selection �߰�
    /// </summary>
    /// <param name="element"></param>
    private void AddElementToSelection(GameBoardCard element)
    {
        if (BattlePlayer.Instance.CurrentACT <= 0) return;

        if (this.SelectedCards.Count > 0)
        {
            var pitch = 1.0f + this.SelectedCards.Count * 0.1f;
            BattleSFX.Instance.Play(BattleSFX.Instance.elementClick, pitch);
        }

        // �ൿ Ƚ�� ����
        BattlePlayer.Instance.ActionCounter(-1);

        element.Selected();
        this.SelectedCards.Add(element);
        
        this.countingText.SetPosition(element);
        this.countingText.SetText(this.SelectedCards);
        this.selectionLine.ElementType = element.CardType;
        this.selectionLine.SetPosition(this.SelectedCards);
    }

    /// <summary>
    /// ���������� ���õ� element���� �� element�� ���ư� ��� => ���õ� element �����
    /// </summary>
    private void DeselectLast()
    {
        var pitch = 1.0f + this.SelectedCards.Count * 0.1f;
        BattleSFX.Instance.Play(BattleSFX.Instance.elementClick, pitch);

        // �ൿ Ƚ�� ����
        BattlePlayer.Instance.ActionCounter(1);

        var lastElement = this.SelectedCards.Last();
        lastElement.Deselected();
        this.SelectedCards.Remove(lastElement);

        this.countingText.SetPosition(this.SelectedCards.Last());
        this.countingText.SetText(this.SelectedCards);
        this.selectionLine.SetPosition(this.SelectedCards);
    }

    private bool IsValidInput()
    {
        return this.SelectedCards.Count > 1 ? true : false;
    }
}
