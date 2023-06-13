using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MysterySelection : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private bool isExit = false;

    [Header("UI")]
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI selectionText;

    [Header("Tween")]
    [SerializeField] private CanvasGroup canvasGroup;

    /// <summary>
    /// ��ư ������ ������ Tween �ִϸ��̼�
    /// </summary>
    /// <returns></returns>
    private Sequence ButtonSequence()
    {
        return DOTween.Sequence()
            .OnStart(() => UISFX.Instance.Play(UISFX.Instance.buttonAppear))
            .Append(this.canvasGroup.transform.DOMoveX(0, 0.4f).SetEase(Ease.OutSine))
            .Join(this.canvasGroup.DOFade(1, 0.4f));
    }

    public void SetCanvasGroup()
    {
        this.canvasGroup.alpha = 0;
        this.canvasGroup.transform.localPosition = new Vector3(-20, 0);
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

        // �̺�Ʈ ���� ������
        if (!this.isExit)
        {
            MysterySystem.Instance.EventEnd();
        }
        // �̺�Ʈ Ż��
        else
        {
            SceneLoader.Instance.PlayerCheckSceneLoad(SceneNames.Mystery);
        }
    }
}
