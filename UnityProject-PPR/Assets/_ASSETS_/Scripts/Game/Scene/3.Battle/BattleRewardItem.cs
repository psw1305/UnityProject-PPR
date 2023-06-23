using PSW.Core.Enums;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleRewardItem : MonoBehaviour
{
    [SerializeField] private ItemType itemType;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI rewardText;

    [SerializeField] private string rewardName;
    [SerializeField] private float rewardAmount;

    /// <summary>
    /// 보상 설정
    /// </summary>
    /// <param name="amount">보상 수</param>
    public void SetReward(float amount)
    {
        this.rewardAmount = amount;
        SetRewardItemText();
    }

    private void SetRewardItemText()
    {
        switch (this.itemType)
        {
            case ItemType.Equipment:
                this.rewardText.text = this.rewardName;
                break;
            case ItemType.Useable:
                this.rewardText.text = this.rewardName + " x" + this.rewardAmount;
                break;
            case ItemType.Cash:
                this.rewardText.text = this.rewardAmount + " Gold";
                break;
        }

    }
}
