using PSW.Core.Enums;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardsItem : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI rewardText;

    /// <summary>
    /// 보상 설정
    /// </summary>
    /// <param name="amount">보상 수</param>
    public void Set()
    {
        this.button.onClick.AddListener(Rewards);
    }

    /// <summary>
    /// 보상 선택시 적용
    /// </summary>
    private void Rewards()
    {
        if (Player.Instance == null) return;


    }
}
