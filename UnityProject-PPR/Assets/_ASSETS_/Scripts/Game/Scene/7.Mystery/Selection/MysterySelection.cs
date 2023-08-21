using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;

public class MysterySelection : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private bool isExit = false;
    [SerializeField] private UnityEvent mysteryResult;

    [Header("UI")]
    [SerializeField] private CanvasGroup selectionCanvas;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI selectionText;


    /// <summary>
    /// 버튼 생성시 나오는 Tween 애니메이션
    /// </summary>
    /// <returns></returns>
    private Sequence ButtonSequence()
    {
        return DOTween.Sequence()
            .OnStart(() => UISFX.Instance.Play(UISFX.Instance.buttonAppear))
            .Append(this.selectionCanvas.transform.DOMoveX(0, 0.4f).SetEase(Ease.OutSine))
            .Join(this.selectionCanvas.DOFade(1, 0.4f));
    }

    public void SetCanvasGroup()
    {
        this.selectionCanvas.alpha = 0;
        this.selectionCanvas.transform.localPosition = new Vector3(-20, 0);
    }

    public void SetEventSelect()
    {
        ButtonSequence();
        
        this.button.onClick.AddListener(EventSelect);
    }

    public void EventSelect()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        this.button.interactable = false;

        // 이벤트 시작 선택지
        if (!this.isExit)
        {
            SelectionResult();
        }
        // 이벤트 탈출
        else
        {
            SceneLoader.Instance.PlayerCheckSceneLoad(SceneNames.Mystery);
        }
    }

    /// <summary>
    /// 선택시 나오는 보상
    /// </summary>
    private void SelectionResult()
    {
        this.mysteryResult?.Invoke();
    }
}
