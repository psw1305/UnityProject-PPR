using PSW.Core.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class RewardsSystem : MonoBehaviour
{
    private CanvasGroup rewardCanvas;

    [SerializeField] private Transform rewardList;
    [SerializeField] private Button exitButton;
    [SerializeField] private RewardsItem[] rewards;

    private void Awake()
    {
        this.rewardCanvas = GetComponent<CanvasGroup>();
        this.rewardCanvas.CanvasInit();
        this.exitButton.onClick.AddListener(BattleExit);

        RewardItemCreate();
    }

    private void RewardItemCreate()
    {
        foreach (RewardsItem reward in this.rewards)
        {
            reward.Set();
        }
    }

    /// <summary>
    /// ���� â �����ֱ�
    /// </summary>
    public void Show()
    {
        BattleSFX.Instance.Play(BattleSFX.Instance.victory);

        this.rewardCanvas.CanvasFadeIn(0.25f);
    }

    /// <summary>
    /// ������ ��ư Ŭ�� �� => Stage Scene ���� ����
    /// </summary>
    private void BattleExit()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        this.exitButton.interactable = false;
        SceneLoader.Instance.PlayerCheckSceneLoad(SceneNames.Battle);
    }
}
