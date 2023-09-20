using PSW.Core.Enums;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardsItem : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private ItemType itemType;
    [SerializeField] private Image rewardsImage;
    [SerializeField] private TextMeshProUGUI rewardsText;
    
    private EnemyType enemyType;
    private int rewardsCash;
    private ItemBlueprint rewardsItemBlueprint;

    /// <summary>
    /// 보상 설정
    /// </summary>
    public void Set(EnemyType enemyType)
    {
        SetRewards(enemyType);

        this.button.onClick.AddListener(Rewards);
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

    #region Set Rewards
    public void SetRewardsCard()
    {
        this.rewardsItemBlueprint = GameManager.Instance.GetRandomCard(0);
        this.rewardsImage.sprite = this.rewardsItemBlueprint.ItemImage;
        this.rewardsText.text = this.rewardsItemBlueprint.ItemName;
    }

    public void SetRewardsRelic()
    {
        this.rewardsItemBlueprint = GameManager.Instance.GetRandomRelic(0);
        this.rewardsImage.sprite = this.rewardsItemBlueprint.ItemImage;
        this.rewardsText.text = this.rewardsItemBlueprint.ItemName;
    }

    public void SetRewardsPotion()
    {
        this.rewardsItemBlueprint = GameManager.Instance.GetRandomPotion(0);
        this.rewardsImage.sprite = this.rewardsItemBlueprint.ItemImage;
        this.rewardsText.text = this.rewardsItemBlueprint.ItemName;
    }

    /// <summary>
    /// 보상 금액 설정
    /// </summary>
    public void SetRewardsCash()
    {
        switch (this.enemyType)
        {
            case EnemyType.Minor:
                this.rewardsCash = Random.Range(CASH.REWARD_MIN_MINOR, CASH.REWARD_MAX_MINOR);
                break;
            case EnemyType.Elite:
                this.rewardsCash = Random.Range(CASH.REWARD_MIN_ELITE, CASH.REWARD_MAX_ELITE);
                break;
            case EnemyType.Boss:
                this.rewardsCash = Random.Range(CASH.REWARD_MIN_BOSS, CASH.REWARD_MAX_BOSS);
                break;
        }

        this.rewardsText.text = this.rewardsCash + " Gold";
    }
    #endregion

    /// <summary>
    /// 보상 선택 시 적용
    /// </summary>
    private void Rewards()
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
                Player.Instance.AddItemCard(this.rewardsItemBlueprint);
                break;
            case ItemType.Relic:
                Player.Instance.AddItemRelic(this.rewardsItemBlueprint);
                break;
            case ItemType.Potion:
                Player.Instance.AddItemPotion(this.rewardsItemBlueprint);
                break;
            case ItemType.Cash:
                Player.Instance.ObtainCash(this.rewardsCash);
                break;
        }
    }
}
