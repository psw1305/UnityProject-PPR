using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StageUI : UI
{
    /// <summary>
    /// Back 단축키 누를 시 작동
    /// </summary>
    protected override void OnBackButtonClick()
    {
        NoticeSystem.Instance.Notice(() => SceneLoader.Instance.LoadScene(SceneNames.Lobby));
    }
}