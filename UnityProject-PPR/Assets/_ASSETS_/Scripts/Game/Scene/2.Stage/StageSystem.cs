using UnityEngine;

public class StageSystem : BehaviourSingleton<StageSystem>
{
    [SerializeField] private GameObject stageGroup;
    [SerializeField] private StageUI stageUI;

    private void Start()
    {
        AudioBGM.Instance.BGMChange(AudioBGM.Instance.stage);
    }

    public void StageActive(bool isActive)
    {
        this.stageGroup.SetActive(isActive);
    }
}
