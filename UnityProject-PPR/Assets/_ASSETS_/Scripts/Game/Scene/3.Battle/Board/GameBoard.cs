using PSW.Core.Probability;
using System.Collections.Generic;
using UnityEngine;

public partial class GameBoard : MonoBehaviour
{
    [Header("Board Settings")]
    [SerializeField] private int rowCount = 6;
    [SerializeField] private int columnCount = 6;
    [SerializeField] private int skillStack = 5;
    [SerializeField] private float rescaling = 0.25f;

    [Header("Elements Settings")]
    [SerializeField] private List<ElementBlueprint> elementBaseDatas = new();
    [SerializeField] private List<ElementBlueprint> elementSkillDatas = new();
    [SerializeField] private GameBoardElementList<ElementBlueprint> elementList = new();
    [SerializeField] private GameBoardElementList<ElementBlueprint> elementSkillList = new();

    [Header("Additional")]
    [SerializeField] private GameBoardCountingText countingText;
    [SerializeField] private GameBoardSelectionLine selectionLine;
    [SerializeField] private GameBoardElement boardElementPrefab;

    private GameBoardInput boardInput;
    private GameBoardMovement boardMovement;
    private GameBoardSpawning boardSpawning;

    public List<GameBoardElement> Elements { get; } = new List<GameBoardElement>();
    public GameBoardElementList<ElementBlueprint> ElementList => this.elementList;
    public GameBoardElementList<ElementBlueprint> ElementSkillList => this.elementSkillList;
    public Vector2 StartCellPosition => this.BoardCenter - this.Rescaling * new Vector2(this.ColumnCount - 1, this.RowCount - 1) / 2.0f;
    public int RowCount => this.rowCount;
    public int ColumnCount => this.columnCount;
    public float Rescaling => this.rescaling;
    public Vector2 BoardCenter => this.transform.position;
    public Transform BoardContainer => this.transform;

    private void Awake()
    {
        this.boardInput = new GameBoardInput(this, this.countingText, this.selectionLine);
        this.boardMovement = new GameBoardMovement(this);
        this.boardSpawning = new GameBoardSpawning(this, this.skillStack);

        // 가중치 값 list 생성
        ProbabilityList();
    }

    /// <summary>
    /// 선택된 elements list
    /// </summary>
    /// <returns></returns>
    public List<GameBoardElement> GetSelectElements()
    {
        return this.boardInput.SelectedElements;
    }

    /// <summary>
    /// 주어진 element 가중치 값 list 생성 
    /// </summary>
    private void ProbabilityList()
    {
        foreach (ElementBlueprint element in this.elementBaseDatas)
        {
            this.elementList.Add(element, element.RandomWeighted);
        }

        foreach (ElementBlueprint element in this.elementSkillDatas)
        {
            this.elementSkillList.Add(element, element.RandomWeighted);
        }
    }

    /// <summary>
    /// 랜덤 가중치 테스트
    /// </summary>
    private void TestProbability()
    {
        float count = 10000;
        int[] list = new int[this.elementBaseDatas.Count];

        for (int i = 0; i < count; i++)
        {
            ElementBlueprint testElement = this.ElementList.Get();

            for (int k = 0; k < this.elementBaseDatas.Count; k++)
            {
                if (testElement == this.elementBaseDatas[k])
                {
                    list[k]++;
                }
            }
        }

        for (int i = 0; i < list.Length; i++)
        {
            Debug.Log(string.Format("{0} : {1}%\n", this.elementBaseDatas[i].name, list[i] / count * 100));
        }
    }

    /// <summary>
    /// board elements 생성
    /// </summary>
    public void SetBoard()
    {
        for (int y = 0; y < this.ColumnCount; y++)
        {
            for (int x = 0; x < this.RowCount; x++)
            {
                GameBoardElement element = Instantiate(this.boardElementPrefab);
                element.transform.localScale = Vector2.one;
                element.transform.position = this.BoardToWorldPosition(x, y);
                element.transform.SetParent(this.BoardContainer, true);
                element.SetBaseData(this.ElementList.Get());
                this.Elements.Add(element);
            }
        }

        // 플레이어가 장착한 소모품 생성
        BattlePlayer.Instance.SetUseableItems();

        // 가중치 테스트 용
        //TestProbability();
    }

    /// <summary>
    /// Coroutine => board 안에 있는 elements 들이 움직일때 까지 대기
    /// </summary>
    /// <returns></returns>
    public Coroutine WaitForMovement()
    {
        return StartCoroutine(this.boardMovement.WaitForMovement());
    }

    /// <summary>
    /// Coroutine => elements 끼리 selection 라인이 이어지기 까지 대기
    /// </summary>
    /// <returns></returns>
    public Coroutine WaitForSelection()
    {
        return StartCoroutine(this.boardInput.WaitForSelection());
    }

    /// <summary>
    /// Coroutine => 선택된 elements 들이 소멸될 때 까지 대기
    /// </summary>
    /// <returns></returns>
    public Coroutine RespawnElements()
    {
        return StartCoroutine(this.boardSpawning.RespawnElements());
    }

    /// <summary>
    /// Coroutine => elements 들이 다 선택될 때 까지 대기
    /// </summary>
    /// <returns></returns>
    public Coroutine DespawnSelection()
    {
        return StartCoroutine(this.boardSpawning.Despawn(GetSelectElements()));
    }
}
