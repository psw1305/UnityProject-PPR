using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class NoticeSystem : BehaviourSingleton<NoticeSystem>
{
    public static bool IsShow { private set; get; }

    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI contentText;

    [SerializeField] private Button okButton;
    [SerializeField] private Button cancelButton;

    protected override void Awake()
    {
        base.Awake();

        IsShow = false;
        this.cancelButton.onClick.AddListener(Init);
    }

    public void Notice(UnityAction events)
    {
        if (IsShow == false)
        {
            Show(events);
        }
        else if (IsShow == true)
        {
            Init();
        }
    }


    private void Show(UnityAction events)
    {
        IsShow = true;

        // 확인 버튼 누를 시 => events 실행
        this.okButton.onClick.AddListener(events);
        this.okButton.onClick.AddListener(() => ButtonInteract(false));

        ButtonInteract(true);

        this.transform.localPosition = Vector3.zero;
    }

    public void Init()
    {
        IsShow = false;

        // 저장된 events 초기화
        this.okButton.onClick.RemoveAllListeners();

        ButtonInteract(false);

        this.transform.localPosition = new Vector3(0, 630, 0);
    }

    private void ButtonInteract(bool isInteract)
    {
        this.okButton.interactable = isInteract;
        this.cancelButton.interactable = isInteract;
    }
}
