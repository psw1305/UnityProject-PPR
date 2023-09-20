using PSW.Core.Enums;
using PSW.Core.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class RewardsSystem : MonoBehaviour
{
    private CanvasGroup rewardCanvas;
    private EnemyType enemyType;

    [SerializeField] private Transform rewardList;
    [SerializeField] private Button exitButton;

    [Header("Prefab")]
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject relicPrefab;
    [SerializeField] private GameObject potionPrefab;
    [SerializeField] private GameObject cashPrefab;

    private void Awake()
    {
        this.rewardCanvas = GetComponent<CanvasGroup>();
        this.rewardCanvas.CanvasInit();
        this.exitButton.onClick.AddListener(ExitToBattle);
    }

    /// <summary>
    /// ���� ����� ���� ����
    /// </summary>
    /// <param name="enemyType"></param>
    public void SetBattleRewards(EnemyType enemyType)
    {
        this.enemyType = enemyType;

        switch (this.enemyType)
        {
            case EnemyType.Minor:
                BattleRewardsMinor();
                break;
            case EnemyType.Elite:
                BattleRewardsElite();
                break;
            case EnemyType.Boss:
                BattleRewardsBoss();
                break;
        }
    }

    /// <summary>
    /// �Ϲ� ���� ���� => ���, ī��, ����(���� Ȯ��)
    /// </summary>
    private void BattleRewardsMinor()
    {
        CreateReward(this.cashPrefab);
        CreateReward(this.potionPrefab);
        CreateReward(this.cardPrefab);
    }

    /// <summary>
    /// ����Ʈ ���� ���� => ����, ���, ī��, ����(���� Ȯ��)
    /// </summary>
    private void BattleRewardsElite()
    {
        CreateReward(this.relicPrefab);
        CreateReward(this.cashPrefab);
        CreateReward(this.potionPrefab);
        CreateReward(this.cardPrefab);
    }

    /// <summary>
    /// ���� ���� ���� => ����(Rare), ���
    /// </summary>
    private void BattleRewardsBoss()
    {
        CreateReward(this.relicPrefab);
        CreateReward(this.cashPrefab);
    }

    private void CreateReward(GameObject rewardsPrefab)
    {
        var rewards = Instantiate(rewardsPrefab, this.rewardList).GetComponent<RewardsItem>();
        rewards.Set(this.enemyType);
    }

    /// <summary>
    /// ���� â �����ֱ�
    /// </summary>
    public void Show()
    {
        BattleSFX.Instance.Play(BattleSFX.Instance.victory);

        this.rewardCanvas.CanvasFadeIn(DUR.CANVAS_FADE_TIME);
    }

    /// <summary>
    /// ������ ��ư Ŭ�� �� => Stage Scene ���� ����
    /// </summary>
    private void ExitToBattle()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        this.exitButton.interactable = false;
        SceneLoader.Instance.PlayerCheckSceneLoad(SceneNames.Battle);
    }
}
