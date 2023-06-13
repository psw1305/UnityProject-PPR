using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : UI
{
    [SerializeField] private Button buttonStart;
    [SerializeField] private Button buttonSettings;

    private void Start()
    {
        this.buttonStart.onClick.AddListener(LoadStageScene);
        this.buttonSettings.onClick.AddListener(LoadSettingsCanvas);
    }

    /// <summary>
    /// 버튼 누를시 => 모든 버튼 중첩 방지
    /// </summary>
    private void AllButtonCheck(bool isCheck)
    {
        this.buttonStart.interactable = isCheck;
        this.buttonSettings.interactable = isCheck;
    }

    /// <summary>
    /// 게임 시작
    /// </summary>
    private void LoadStageScene()
    {
        AllButtonCheck(false);
        UISFX.Instance.Play(UISFX.Instance.buttonClick);
        SceneLoader.Instance.LoadScene(SceneNames.Stage);
    }

    /// <summary>
    /// 설정 관리
    /// </summary>
    private void LoadSettingsCanvas()
    {
        SettingsSystem.Instance.Show();
    }

    /// <summary>
    /// 게임 종료
    /// </summary>
    protected override void OnBackButtonClick()
    {
        if (!SceneLoader.IsLoaded) return;

        NoticeSystem.Instance.Notice(() => ExitApplication());
    }

    private void ExitApplication()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}
