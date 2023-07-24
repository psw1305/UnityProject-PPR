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
    /// 보상 창 보여주기
    /// </summary>
    public void Show()
    {
        BattleSFX.Instance.Play(BattleSFX.Instance.victory);

        this.rewardCanvas.CanvasFadeIn(0.25f);
    }

    /// <summary>
    /// 떠나기 버튼 클릭 시 => Stage Scene 으로 복귀
    /// </summary>
    private void BattleExit()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        this.exitButton.interactable = false;
        SceneLoader.Instance.PlayerCheckSceneLoad(SceneNames.Battle);
    }
}
