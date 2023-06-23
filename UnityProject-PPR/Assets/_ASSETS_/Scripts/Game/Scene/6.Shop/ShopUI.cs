using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShopUI : UI
{
    [Header("Button")]
    [SerializeField] private Button npcButton;
    [SerializeField] private Button exitButton;

    [Header("Board")]
    [SerializeField] private Transform npcBoard;
    [SerializeField] private Transform shopBoard;

    private bool isOpen = false;

    private void Start()
    {
        this.npcButton.onClick.AddListener(Open);
        this.exitButton.onClick.AddListener(Exit);
    }

    private void Open()
    {
        UISFX.Instance.Play(UISFX.Instance.shopBell);

        if (this.isOpen)
        {
            this.npcBoard.DOLocalMoveY(0, 0.4f).SetEase(Ease.OutSine);
            this.shopBoard.DOLocalMoveY(480, 0.4f).SetEase(Ease.OutSine);
        }
        else
        {
            this.npcBoard.DOLocalMoveY(-150, 0.4f).SetEase(Ease.OutSine);
            this.shopBoard.DOLocalMoveY(100, 0.4f).SetEase(Ease.OutSine);
        }

        this.isOpen = !this.isOpen;
    }

    private void Exit()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        this.exitButton.interactable = false;
        SceneLoader.Instance.PlayerCheckSceneLoad(SceneNames.Shop);
    }

    protected override void OnBackButtonClick()
    {
        
    }
}
