using UnityEngine;

public class MysterySelectionTrade : MysterySelection
{
    [Header("Trade")]
    [SerializeField] private int tradePrice;

    public override void SetEventSelect()
    {
        base.SetEventSelect();

        if (Player.Instance == null) return;

        // 플레이어의 잔액이 선택지에 금액보다 높은가?
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
