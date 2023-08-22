using PSW.Core.Enums;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardsItem : MonoBehaviour
{
    [SerializeField] private ItemType itemType;
    [SerializeField] private Button button;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI rewardsText;
    
    private EnemyType enemyType;
    private int rewardsCash;

    /// <summary>
    /// 보상 설정
    /// </summary>
    public void Set(EnemyType enemyType)
    {
        SetRewards(enemyType);

        this.button.onClick.AddListener(GetRewards);
    }

    /// <summary>
    /// 보상 선택시 적용
    /// </summary>
    private void SetRewards(EnemyType enemyType)
    {
        this.enemyType = enemyType;

        switch (this.itemType)
        {
            case ItemType.Card:
                SetRewardsCard();
                break;
            case ItemType.Relic:
                SetRewardsRelic();
                break;
            case ItemType.Potion:
                SetRewardsPotion();
                break;
            case ItemType.Cash:
                SetRewardsCash();
                break;
        }
    }

    public void SetRewardsCard()
    {
    }

    public void SetRewardsRelic()
    {
    }

    public void SetRewardsPotion()
    {
    }

    public void SetRewardsCash()
    {
        switch (this.enemyType)
        {
            case EnemyType.Minor:
                this.rewardsCash = Random.Range(10, 21);
                break;
            case EnemyType.Elite:
                this.rewardsCash = Random.Range(25, 36);
                break;
            case EnemyType.Boss:
                this.rewardsCash = Random.Range(50, 61);
                break;
        }

        this.rewardsText.text = this.rewardsCash + " Gold";
    }

    /// <summary>
    /// 보상 선택시 적용
    /// </summary>
    private void GetRewards()
    {
        UISFX.Instance.ItemDropSFX(this.itemType);

        if (Player.Instance != null)
        {
            ItemRewards();
        }
        
        Destroy(this.gameObject);
    }

    private void ItemRewards()
    {
        switch (this.itemType) 
        {
            case ItemType.Card:
                break;
            case ItemType.Relic:
                break;
            case ItemType.Potion:
                break;
            case ItemType.Cash:
                Player.Instance.ObtainCash(this.rewardsCash);
                break;
        }
    }
}
