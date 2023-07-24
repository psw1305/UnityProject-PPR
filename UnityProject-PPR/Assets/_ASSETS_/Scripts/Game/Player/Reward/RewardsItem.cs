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
    /// ���� ����
    /// </summary>
    /// <param name="amount">���� ��</param>
    public void Set()
    {
        this.button.onClick.AddListener(Rewards);
    }

    /// <summary>
    /// ���� ���ý� ����
    /// </summary>
    private void Rewards()
    {
        if (Player.Instance == null) return;


    }
}
