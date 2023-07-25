using PSW.Core.Enums;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardsItem : MonoBehaviour
{
    [SerializeField] private ItemType itemType;
    [SerializeField] private Button button;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI rewardText;

    /// <summary>
    /// ���� ����
    /// </summary>
    /// <param name="amount">���� ��</param>
    public void Set()
    {
        this.button.onClick.AddListener(GetReward);
    }

    /// <summary>
    /// ���� ���ý� ����
    /// </summary>
    private void GetReward()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        if (Player.Instance != null)
        {
            RewardItemType();
        }
        
        Destroy(this.gameObject);
    }

    private void RewardItemType()
    {

    }
}
