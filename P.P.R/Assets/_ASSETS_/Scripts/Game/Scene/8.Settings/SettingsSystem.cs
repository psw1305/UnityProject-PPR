using PSW.Core.Extensions;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingsSystem : BehaviourSingleton<SettingsSystem>
{
    public bool IsShow { get; private set; }

    [SerializeField] private BGMControlUI bgmControl;
    [SerializeField] private SFXControlUI sfxControl;
    [SerializeField] private FPSControlUI fpsControl;
    [SerializeField] private LanguageControlUI languageControl;

    [SerializeField] private Button closeButton;

    private CanvasGroup canvasGroup;

    private void Start()
    {
        this.canvasGroup = GetComponent<CanvasGroup>();

        this.bgmControl.Set(DataFrame.Instance.dBGM);
        this.sfxControl.Set(DataFrame.Instance.dSFX);
        this.fpsControl.Set(DataFrame.Instance.dFPS);
        this.languageControl.Set(DataFrame.Instance.dLanguage);

        this.IsShow = false;
        this.closeButton.onClick.AddListener(Hide);
    }

    public void Show()
    {
        if (this.IsShow == true) return;

        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        this.IsShow = true;
        this.closeButton.interactable = true;
        this.canvasGroup.CanvasFadeIn(0.25f);
    }

    private void Hide()
    {
        if (this.IsShow == false) return;

        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        this.IsShow = false;
        this.closeButton.interactable = false;
        this.canvasGroup.CanvasFadeOut(0.25f, new Vector3(-300, 0, 0));
    }
}