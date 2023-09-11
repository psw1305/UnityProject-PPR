using UnityEngine;
using UnityEngine.UI;

public class ShopUI : UI
{
    [Header("UI")]
    [SerializeField] private Button exitButton;

    private void Start()
    {
        this.exitButton.onClick.AddListener(Exit);
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
