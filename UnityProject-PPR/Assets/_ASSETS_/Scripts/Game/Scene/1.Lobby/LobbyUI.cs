using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : UI
{
    [SerializeField] private Canvas lobbyCanvas;
    [SerializeField] private Camera lobbyCamera;

    [SerializeField] private Button buttonStart;
    [SerializeField] private Button buttonSettings;

    private void Start()
    {
        GameManager.Instance.CameraChange(this.lobbyCamera, this.lobbyCanvas);

        this.buttonStart.onClick.AddListener(LoadStageScene);
        this.buttonSettings.onClick.AddListener(LoadSettingsCanvas);

        AudioBGM.Instance.BGMChange(AudioBGM.Instance.lobby);
    }

    /// <summary>
    /// ��ư ������ => ��� ��ư ��ø ����
    /// </summary>
    private void AllButtonCheck(bool isCheck)
    {
        this.buttonStart.interactable = isCheck;
        this.buttonSettings.interactable = isCheck;
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    private void LoadStageScene()
    {
        AllButtonCheck(false);
        UISFX.Instance.Play(UISFX.Instance.buttonClick);
        SceneLoader.Instance.LoadScene(SceneNames.Stage);
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    private void LoadSettingsCanvas()
    {
        SettingsSystem.Instance.Show();
    }

    /// <summary>
    /// ���� ����
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
