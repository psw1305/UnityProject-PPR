using UnityEngine;
using UnityEngine.UI;

public class TreasureSystem : BehaviourSingleton<TreasureSystem>
{
    [SerializeField] private Camera treasureCamera;
    [SerializeField] private Canvas treasureCanvas;

    [SerializeField] private Button exitButton;

    protected override void Awake()
    {
        base.Awake();

        this.exitButton.onClick.AddListener(Exit);

        AudioBGM.Instance.BGMChange(AudioBGM.Instance.stage);

        GameManager.Instance.CameraChange(this.treasureCamera, this.treasureCanvas);
    }

    public void Exit()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        this.exitButton.interactable = false;
        SceneLoader.Instance.PlayerCheckSceneLoad(SceneNames.Treasure);
    }
}
