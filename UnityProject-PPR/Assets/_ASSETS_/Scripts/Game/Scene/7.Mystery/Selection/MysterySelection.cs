using PSW.Core.Enums;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using PSW.Core.Stat;

public class MysterySelection : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private bool isExit = false;

    [Header("UI")]
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI selectionText;

    [Header("Tween")]
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Selection")]
    [SerializeField] private MysteryType mysteryType;
    [SerializeField] private ItemBlueprint[] selectionEquipments;
    [SerializeField] private ItemBlueprint[] selectionUseables;

    /// <summary>
    /// 버튼 생성시 나오는 Tween 애니메이션
    /// </summary>
    /// <returns></returns>
    private Sequence ButtonSequence()
    {
        return DOTween.Sequence()
            .OnStart(() => UISFX.Instance.Play(UISFX.Instance.buttonAppear))
            .Append(this.canvasGroup.transform.DOMoveX(0, 0.4f).SetEase(Ease.OutSine))
            .Join(this.canvasGroup.DOFade(1, 0.4f));
    }

    public void SetCanvasGroup()
    {
        this.canvasGroup.alpha = 0;
        this.canvasGroup.transform.localPosition = new Vector3(-20, 0);
    }

    private ItemBlueprint GetRandomItem(ItemBlueprint[] items)
    {
        int random = Random.Range(0, items.Length);
        return items[random];
    }

    public void SetEventSelect()
    {
        ButtonSequence();
        
        this.button.onClick.AddListener(EventSelect);
    }

    public void EventSelect()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        this.button.interactable = false;

        // 이벤트 시작 선택지
        if (!this.isExit)
        {
            MysterySystem.Instance.EventEnd();
            SelectionReward();
        }
        // 이벤트 탈출
        else
        {
            SceneLoader.Instance.PlayerCheckSceneLoad(SceneNames.Mystery);
        }
    }

    /// <summary>
    /// 선택지 선택시 나오는 보상 
    /// TODO => 3가지 보상으로 제한, 추후 디테일하게 구현 필요
    /// </summary>
    private void SelectionReward()
    {
        switch (this.mysteryType)
        {
            case MysteryType.HealthUp:
                HealthUp();
                break;
            case MysteryType.ItemGain:
                ItemGain();
                break;
            case MysteryType.GoldGain:
                CashGain();
                break;
        }

    }

    private void HealthUp()
    {
        UISFX.Instance.Play(UISFX.Instance.healthUp);

        if (Player.Instance == null) return;

        var randomHp = Random.Range(5, 11);
        Player.Instance.HP.AddModifier(new StatModifier(randomHp, StatModType.Int, this));
        Player.Instance.SetHP(Player.Instance.CurrentHP + randomHp);
    }

    private void ItemGain()
    {
        UISFX.Instance.Play(UISFX.Instance.itemGain);

        if (InventorySystem.Instance == null) return;

        InventorySystem.Instance.AddItem(GetRandomItem(this.selectionEquipments));
        InventorySystem.Instance.AddItem(GetRandomItem(this.selectionUseables));
    }

    private void CashGain()
    {
        UISFX.Instance.Play(UISFX.Instance.cashGain);

        if (Player.Instance == null) return;

        var randomCash = Random.Range(50, 201);
        Player.Instance.SetCash(randomCash);
    }
}
