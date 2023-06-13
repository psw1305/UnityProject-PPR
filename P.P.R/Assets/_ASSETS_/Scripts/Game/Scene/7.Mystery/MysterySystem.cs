using PSW.Core.Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Components;
using TMPro;

public class MysterySystem : BehaviourSingleton<MysterySystem>
{
    [Header("Params")]
    [SerializeField] private MysteryConfig eventBlueprint;
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Event UI")]
    [SerializeField] private Image eventPicture;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    private string dialogue = string.Empty;

    [Header("Canvas")]
    [SerializeField] private CanvasGroup titleTable;
    [SerializeField] private CanvasGroup contentsTable;
    [SerializeField] private CanvasGroup pictureTable;
    private bool isFadeIn;

    [Header("Selection")]
    [SerializeField] private Transform selectionStartList;
    [SerializeField] private Transform selectionEndList;
    [SerializeField] private GameObject selectionPrefab;
    [SerializeField] private GameObject selectionExitPrefab;
    private MysterySelection[] startSelections;
    private MysterySelection[] endSelections;

    private void Start()
    {
        this.eventPicture.sprite = this.eventBlueprint.eventPicture;
        
        var stringTable = this.eventBlueprint.stringTable;
        SetTextContents(this.titleText, stringTable, "Title");
        SetTextContents(this.dialogueText, stringTable, "Before");

        this.dialogueText.color = Color.clear;

        SetSelections();

        this.isFadeIn = false;
        this.titleTable.alpha = 0;
        this.contentsTable.alpha = 0;
        this.pictureTable.alpha = 0;
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

            this.titleTable.CanvasFadeIn(0.6f, 0.0f);
            this.contentsTable.CanvasFadeIn(0.6f, 0.4f);
            this.pictureTable.CanvasFadeIn(0.6f, 0.8f);

            yield return new WaitForSeconds(1.2f);

            StartCoroutine(Display(this.dialogueText.text, this.startSelections));
        }
    }

    /// <summary>
    /// 선택지 선택 후 => 이벤트 마무리 및 나가기 선택지 생성
    /// </summary>
    public void EventEnd()
    {
        this.selectionStartList.gameObject.SetActive(false);
        this.selectionEndList.gameObject.SetActive(true);

        var table = this.eventBlueprint.stringTable;
        SetTextContents(this.dialogueText, table, "After");

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
        this.startSelections = new MysterySelection[3];

        for (int i = 0; i < this.startSelections.Length; i++)
        {
            GameObject clone = Instantiate(this.selectionPrefab, this.selectionStartList);

            this.startSelections[i] = clone.GetComponent<MysterySelection>();
            this.startSelections[i].SetCanvasGroup();
            this.selectionStartList.gameObject.SetActive(true);
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
            yield return new WaitForSeconds(this.typingSpeed);
        }

        for (int i = 0; i < selections.Length; i++)
        {
            yield return new WaitForSeconds(0.2f);
            selections[i].SetEventSelect();
        }
    }
}
