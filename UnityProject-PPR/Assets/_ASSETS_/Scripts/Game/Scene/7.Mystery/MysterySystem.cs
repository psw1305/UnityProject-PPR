using PSW.Core.Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Components;
using TMPro;

public class MysterySystem : BehaviourSingleton<MysterySystem>
{
    [SerializeField] private Canvas mysteryCanvas;
    [SerializeField] private Camera mysteryCamera;

    [Header("Event Config")]
    [SerializeField] private MysteryConfig mysteryConfig;

    [Header("Event UI")]
    [SerializeField] private Image eventPicture;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Canvas")]
    [SerializeField] private CanvasGroup titleCanvas;
    [SerializeField] private CanvasGroup contentsCanvas;
    [SerializeField] private CanvasGroup pictureCanvas;
    private bool isFadeIn;

    [Header("Selection")]
    [SerializeField] private Transform selectionStartList;
    [SerializeField] private Transform selectionEndList;
    [SerializeField] private GameObject[] selections;
    [SerializeField] private GameObject selectionExitPrefab;
    private MysterySelection[] startSelections;
    private MysterySelection[] endSelections;

    private void Start()
    {
        if (Player.Instance != null)
        {
            this.mysteryConfig = GameManager.Instance.MysteryConfig;
        }

        this.eventPicture.sprite = this.mysteryConfig.EventPicture;
        this.selections = this.mysteryConfig.Selections;

        var stringTable = this.mysteryConfig.StringTable;
        SetTextContents(this.titleText, stringTable, "Title");
        SetTextContents(this.dialogueText, stringTable, "Before");

        this.dialogueText.color = Color.clear;

        SetSelections();

        this.isFadeIn = false;
        this.titleCanvas.alpha = 0;
        this.contentsCanvas.alpha = 0;
        this.pictureCanvas.alpha = 0;

        GameManager.Instance.CameraChange(this.mysteryCamera, this.mysteryCanvas);
    }

    private void Update()
    {
        if (SceneLoader.IsLoaded)
        {
            StartCoroutine(EventStart());
        }
    }

    /// <summary>
    /// 이벤트 시작시 연출 및 선택지 생성
    /// </summary>
    /// <returns></returns>
    private IEnumerator EventStart()
    {
        if (!this.isFadeIn)
        {
            this.isFadeIn = true;

            this.titleCanvas.CanvasFadeInDelay(DUR.SELECTION_FADE_TIME, 0.0f);
            this.contentsCanvas.CanvasFadeInDelay(DUR.SELECTION_FADE_TIME, 0.4f);
            this.pictureCanvas.CanvasFadeInDelay(DUR.SELECTION_FADE_TIME, 0.8f);

            yield return YieldCache.WaitForSeconds(1.2f);

            StartCoroutine(Display(this.dialogueText.text, this.startSelections));
        }
    }

    /// <summary>
    /// 선택지 선택 후 => 이벤트 마무리 및 나가기 선택지 생성
    /// </summary>
    public void EventEnd(string after)
    {
        this.selectionStartList.gameObject.SetActive(false);
        this.selectionEndList.gameObject.SetActive(true);

        var table = this.mysteryConfig.StringTable;
        SetTextContents(this.dialogueText, table, after);

        StartCoroutine(Display(this.dialogueText.text, this.endSelections));
    }

    /// <summary>
    /// UIText 로컬라이즈 텍스트로 세팅
    /// </summary>
    /// <param name="tmpText"></param>
    /// <param name="table"></param>
    /// <param name="entry"></param>
    private void SetTextContents(TextMeshProUGUI tmpText, string table, string entry)
    {
        var localizeString = tmpText.GetComponent<LocalizeStringEvent>();
        localizeString.SetTable(table);
        localizeString.SetEntry(entry);
    }

    /// <summary>
    /// 이벤트 선택지 생성
    /// </summary>
    private void SetSelections()
    {
        this.startSelections = new MysterySelection[this.selections.Length];

        for (int i = 0; i < this.startSelections.Length; i++)
        {
            GameObject clone = Instantiate(this.selections[i], this.selectionStartList);

            this.startSelections[i] = clone.GetComponent<MysterySelection>();
            this.startSelections[i].SetCanvasGroup();
        }

        this.endSelections = new MysterySelection[1];

        for (int i = 0; i < this.endSelections.Length; i++)
        {
            GameObject clone = Instantiate(this.selectionExitPrefab, this.selectionEndList);

            this.endSelections[i] = clone.GetComponent<MysterySelection>();
            this.endSelections[i].SetCanvasGroup();
            this.selectionEndList.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 이벤트 시작시 글자 차례대로 보여주기
    /// </summary>
    /// <param name="line"></param>
    /// <param name="selections"></param>
    /// <returns></returns>
    private IEnumerator Display(string line, MysterySelection[] selections)
    {
        this.dialogueText.text = "";
        this.dialogueText.color = Color.black;

        foreach (var letter in line.ToCharArray())
        {
            this.dialogueText.text += letter;
            yield return YieldCache.WaitForSeconds(DUR.TYPING_SPEED);
        }

        for (int i = 0; i < selections.Length; i++)
        {
            yield return YieldCache.WaitForSeconds(0.2f);
            selections[i].SetEventSelect();
        }
    }
}
