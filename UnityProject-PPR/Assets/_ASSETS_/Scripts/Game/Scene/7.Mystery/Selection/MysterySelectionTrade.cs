using UnityEngine;

public class MysterySelectionTrade : MysterySelection
{
    [Header("Trade")]
    [SerializeField] private int tradePrice;

    public override void SetEventSelect()
    {
        base.SetEventSelect();

        if (Player.Instance == null) return;

        // �÷��̾��� �ܾ��� �������� �ݾ׺��� ������?
        if (Player.Instance.Cash >= this.tradePrice)
        {
            this.selectionText.color = Color.green;
        }
        else
        {
            this.button.interactable = false;
            this.selectionText.color = Color.red;
        }
    }

    protected override void SelectionResult()
    {
        if (Player.Instance == null) return;

        if (Player.Instance.UseCash(this.tradePrice))
        {
            base.SelectionResult();
        }
    }
}
