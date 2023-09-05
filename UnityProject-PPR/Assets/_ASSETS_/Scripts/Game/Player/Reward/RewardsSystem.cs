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
    [SerializeField] private RewardsItem cashPrefab;
    [SerializeField] private RewardsItem cardPrefab;
    [SerializeField] private RewardsItem potionPrefab;
    [SerializeField] private RewardsItem artifactPrefab;

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
    /// ����Ʈ ���� ���� => ��Ƽ��Ʈ, ���, ī��, ����(���� Ȯ��)
    /// </summary>
    private void BattleRewardsElite()
    {
        CreateReward(this.artifactPrefab);
        CreateReward(this.cashPrefab);
        CreateReward(this.potionPrefab);
        CreateReward(this.cardPrefab);
    }

    /// <summary>
    /// ���� ���� ���� => ��Ƽ��Ʈ(����), ���
    /// </summary>
    private void BattleRewardsBoss()
    {
        CreateReward(this.artifactPrefab);
        CreateReward(this.cashPrefab);
    }

    private void CreateReward(RewardsItem rewardItem)
    {
        RewardsItem clone = Instantiate(rewardItem, this.rewardList);
        clone.Set(this.enemyType);
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
