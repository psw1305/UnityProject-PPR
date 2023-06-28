using PSW.Core.Extensions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleReward : BehaviourSingleton<BattleReward>
{
    private RectTransform rectTransform;
    private CanvasGroup rewardCanvas;

    [SerializeField] private Transform rewardList;
    [SerializeField] private Button exitButton;
    [SerializeField] private List<BattleRewardItem> rewardItems = new ();

    protected override void Awake()
    {
        base.Awake();

        this.rectTransform = GetComponent<RectTransform>();
        this.rewardCanvas = GetComponent<CanvasGroup>();
        this.rewardCanvas.CanvasInit();

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
    public void BattleExit()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        this.exitButton.interactable = false;
        SceneLoader.Instance.PlayerCheckSceneLoad(SceneNames.Battle);
    }

    public void GetReward()
    {
        // ���� �߿� ���� ��ȭ �ջ�
        Player.Cash += BattlePlayer.EarnCash;
    }
}
