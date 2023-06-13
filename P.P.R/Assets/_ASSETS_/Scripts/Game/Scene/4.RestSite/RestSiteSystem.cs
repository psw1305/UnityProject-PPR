using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RestSiteSystem : BehaviourSingleton<RestSiteSystem>
{
    [Header("UI")]
    [SerializeField] private Button restButton;
    [SerializeField] private Button smithButton;
    [SerializeField] private Button exitButton;

    [Header("Tween")]
    [SerializeField] private CanvasGroup exitCanvasGroup;

    protected override void Awake()
    {
        base.Awake();

        this.restButton.onClick.AddListener(Rest);
        this.smithButton.onClick.AddListener(Smith);
        this.exitButton.onClick.AddListener(Exit);

        //TODO => 임시 제한
        this.smithButton.interactable = false;

        this.exitButton.interactable = false;
        this.exitCanvasGroup.alpha = 0;
        this.exitCanvasGroup.transform.localPosition = new Vector3(-20, 0);

        AudioBGM.Instance.BGMChange(AudioBGM.Instance.restsite);
    }

    private Sequence ExitButtonSequence()
    {
        return DOTween.Sequence()
            .Append(this.exitCanvasGroup.transform.DOMoveX(0, 0.4f).SetEase(Ease.OutSine))
            .Join(this.exitCanvasGroup.DOFade(1, 0.4f))
            .OnComplete(() => { this.exitButton.interactable = true; });
    }


    public void Rest()
    {
        this.restButton.interactable = false;

        ExitButtonSequence();
    }

    public void Smith()
    {
        this.smithButton.interactable = false;

        //TODO => 차후 구현

        ExitButtonSequence();
    }

    public void Exit() 
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        this.exitButton.interactable = false;
        SceneLoader.Instance.PlayerCheckSceneLoad(SceneNames.RestSite);
    }
}
