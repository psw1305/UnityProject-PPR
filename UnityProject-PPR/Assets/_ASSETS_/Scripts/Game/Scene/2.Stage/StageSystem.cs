using UnityEngine;

public class StageSystem : BehaviourSingleton<StageSystem>
{
    [SerializeField] private GameObject stageGroup;
    [SerializeField] private StageUI stageUI;
    [SerializeField] private Canvas stageCanvas;
    [SerializeField] private Camera stageCamera;

    protected override void Awake()
    {
        base.Awake();

        GameManager.Instance.CameraChange(this.stageCamera, this.stageCanvas);
    }

    private void Start()
    {
        AudioBGM.Instance.BGMChange(AudioBGM.Instance.stage);
    }

    public void StageActive(bool isActive)
    {
        this.stageGroup.SetActive(isActive);
    }
}
