public class MysterySelectionExit : MysterySelection
{
    /// <summary>
    /// ������ ������ => �������� ������ ��ȯ
    /// </summary>
    protected override void SelectionResult()
    {
        this.button.interactable = false;

        SceneLoader.Instance.PlayerCheckSceneLoad(SceneNames.Mystery);
    }
}
