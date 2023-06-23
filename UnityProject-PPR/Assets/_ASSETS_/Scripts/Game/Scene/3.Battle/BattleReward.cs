using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleReward : BehaviourSingleton<BattleReward>
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    [SerializeField] private Transform rewardList;
    [SerializeField] private Button exitButton;
    [SerializeField] private List<BattleRewardItem> rewardItems = new ();

    protected override void Awake()
    {
        base.Awake();

        this.rectTransform = GetComponent<RectTransform>();
        this.canvasGroup = GetComponent<CanvasGroup>();
        this.canvasGroup.alpha = 0;

        this.exitButton.onClick.AddListener(BattleExit);
    }

    private void RewardItemCreate()
    {
        foreach (BattleRewardItem item in this.rewardItems)
        {
            var itemClone = Instantiate(item, rewardList);
            itemClone.SetReward(100);
        }
    }

    /// <summary>
    /// 보상 창 보여주기
    /// </summary>
    public void Show()
    {
        BattleSFX.Instance.Play(BattleSFX.Instance.victory);

        this.rectTransform.position = new Vector3(0, 0, 0);
        this.canvasGroup.DOFade(1, 0.25f);
    }

    /// <summary>
    /// 떠나기 버튼 클릭 시 => Stage Scene 으로 복귀
    /// </summary>
    public void BattleExit()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        this.exitButton.interactable = false;
        SceneLoader.Instance.PlayerCheckSceneLoad(SceneNames.Battle);
    }

    public void GetReward()
    {
        // 전투 중에 얻은 재화 합산
        Player.Cash += BattlePlayer.EarnCash;
    }
}
