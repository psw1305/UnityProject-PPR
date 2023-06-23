using PSW.Core.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSystem : BehaviourSingleton<SettingsSystem>
{
    public bool IsShow { get; private set; }

    [Header("Control")]
    [SerializeField] private BGMControlUI bgmControl;
    [SerializeField] private SFXControlUI sfxControl;
    [SerializeField] private FPSControlUI fpsControl;
    [SerializeField] private LanguageControlUI languageControl;

    [Header("Button")]
    [SerializeField] private Button closeButton;
    [SerializeField] private Button lobbyButton;
    [SerializeField] private Button exitButton;

    private CanvasGroup canvasGroup;

    private void Start()
    {
        this.canvasGroup = GetComponent<CanvasGroup>();

        this.bgmControl.Set(DataFrame.Instance.dBGM);
        this.sfxControl.Set(DataFrame.Instance.dSFX);
        this.fpsControl.Set(DataFrame.Instance.dFPS);
        this.languageControl.Set(DataFrame.Instance.dLanguage);

        this.closeButton.onClick.AddListener(HideClick);
        this.lobbyButton.onClick.AddListener(GameLobby);
        this.exitButton.onClick.AddListener(GameExit);
        this.IsShow = false;
    }

    public void Show()
    {
        if (this.IsShow == true) return;

        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        this.IsShow = true;
        this.closeButton.interactable = true;
        this.canvasGroup.CanvasFadeIn(0.25f);
    }

    private void HideClick()
    {
        if (this.IsShow == false) return;

        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        Hide();
    }

    private void Hide()
    {
        this.IsShow = false;
        this.closeButton.interactable = false;
        this.canvasGroup.CanvasFadeOut(0.25f, new Vector3(-300, 0, 0));
    }

    private void GameLobby()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        this.lobbyButton.interactable = false;
        SceneLoader.Instance.LoadScene(SceneNames.Lobby);
    }

    private void GameExit()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);
        
        this.exitButton.interactable = false;
        Application.Quit();
    }

    public void Init()
    {
        if (!this.lobbyButton.interactable) this.lobbyButton.interactable = true;
        if (!this.exitButton.interactable) this.exitButton.interactable = true;

        if (this.IsShow) Hide();
    }
}