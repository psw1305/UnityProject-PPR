using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RestSiteSystem : MonoBehaviour
{
    [SerializeField] private Camera restsiteCamera;
    [SerializeField] private Canvas restsiteCanvas;

    [Header("UI")]
    [SerializeField] private Button restButton;
    [SerializeField] private Button removeButton;
    [SerializeField] private Button exitButton;

    [Header("Tween")]
    [SerializeField] private CanvasGroup exitCanvasGroup;

    private void Awake()
    {
        this.restButton.onClick.AddListener(PlayerRest);
        this.removeButton.onClick.AddListener(CardRemove);
        this.exitButton.onClick.AddListener(Exit);

        this.exitButton.interactable = false;
        this.exitCanvasGroup.alpha = 0;
        this.exitCanvasGroup.transform.localPosition = new Vector3(-20, 0);

        AudioBGM.Instance.BGMChange(AudioBGM.Instance.restsite);

        GameManager.Instance.CameraChange(this.restsiteCamera, this.restsiteCanvas);
    }

    private Sequence ExitButtonSequence()
    {
        return DOTween.Sequence()
            .OnStart(() => {
                this.restButton.interactable = false;
                this.removeButton.interactable = false;
            })
            .Append(this.exitCanvasGroup.transform.DOMoveX(0, 0.4f).SetEase(Ease.OutSine))
            .Join(this.exitCanvasGroup.DOFade(1, 0.4f))
            .OnComplete(() => { this.exitButton.interactable = true; });
    }

    /// <summary>
    /// 휴식 선택 시 => 플레이어 최대 체력의 25% 회복 (소수점 내림)
    /// </summary>
    public void PlayerRest()
    {
        UISFX.Instance.Play(UISFX.Instance.playerRest);
        ExitButtonSequence();

        if (Player.Instance != null)
        {
            var recoveryPercent = Mathf.FloorToInt(Player.Instance.GetMaxHP() * 0.25f);
            Debug.Log(recoveryPercent);
            Player.Instance.Recovery(recoveryPercent);
        }
    }

    public void CardRemove()
    {
        UISFX.Instance.Play(UISFX.Instance.cardRemove);
        ExitButtonSequence();

        if (Player.Instance != null)
        {
            // TODO
        }
    }

    public void Exit() 
    {
        this.exitButton.interactable = false;
        UISFX.Instance.Play(UISFX.Instance.buttonClick);
        SceneLoader.Instance.PlayerCheckSceneLoad(SceneNames.RestSite);
    }
}
