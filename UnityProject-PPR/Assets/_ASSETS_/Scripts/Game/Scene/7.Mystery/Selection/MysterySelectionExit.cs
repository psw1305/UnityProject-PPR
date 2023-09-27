public class MysterySelectionExit : MysterySelection
{
    /// <summary>
    /// 마무리 선택지 => 스테이지 씬으로 전환
    /// </summary>
    protected override void SelectionResult()
    {
        this.button.interactable = false;

        SceneLoader.Instance.PlayerCheckSceneLoad(SceneNames.Mystery);
    }
}
