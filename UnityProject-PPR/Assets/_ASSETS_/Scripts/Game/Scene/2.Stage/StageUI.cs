using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StageUI : UI
{
    /// <summary>
    /// Back ����Ű ���� �� �۵�
    /// </summary>
    protected override void OnBackButtonClick()
    {
        PlayerNotice.Instance.Notice(() => SceneLoader.Instance.LoadScene(SceneNames.Lobby));
    }
}