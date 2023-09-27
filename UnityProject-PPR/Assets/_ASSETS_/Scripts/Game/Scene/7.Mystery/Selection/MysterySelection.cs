using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;

public class MysterySelection : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private UnityEvent mysteryResult;

    [Header("UI")]
    [SerializeField] private CanvasGroup selectionCanvas;
    [SerializeField] protected Button button;
    [SerializeField] protected TextMeshProUGUI selectionText;


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

    public virtual void SetEventSelect()
    {
        ButtonSequence();
        
        this.button.onClick.AddListener(EventSelect);
    }

    public void EventSelect()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);

        SelectionResult();
    }

    /// <summary>
    /// 선택 시 나오는 보상
    /// </summary>
    protected virtual void SelectionResult()
    {
        this.button.interactable = false;

        this.mysteryResult?.Invoke();
    }
}
