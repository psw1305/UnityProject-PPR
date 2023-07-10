using PSW.Core.Enums;
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

    [Header("Element Settings")]
    [SerializeField] private List<ElementBlueprint> elementDatas = new();
    [SerializeField] private List<ElementBlueprint> elementSkillDatas = new();
    [SerializeField] private List<ElementBlueprint> elementSkillUsedDatas = new();
    [SerializeField] private GameBoardElementList<ElementBlueprint> elementList = new();

    [Header("Additional")]
    [SerializeField] private GameBoardCountingText countingText;
    [SerializeField] private GameBoardSelectionLine selectionLine;
    [SerializeField] private GameBoardElement boardElementPrefab;

    private GameBoardInput boardInput;
    private GameBoardMovement boardMovement;
    private GameBoardSpawning boardSpawning;

    public List<GameBoardElement> Elements { get; } = new List<GameBoardElement>();
    public GameBoardElementList<ElementBlueprint> ElementList => this.elementList;
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

        // ����ġ �� list ����
        ProbabilityList();
    }

    /// <summary>
    /// ���õ� elements list
    /// </summary>
    /// <returns></returns>
    public List<GameBoardElement> GetSelectElements()
    {
        return this.boardInput.SelectedElements;
    }

    /// <summary>
    /// �־��� element ����ġ �� list ���� 
    /// </summary>
    private void ProbabilityList()
    {
        foreach (ElementBlueprint element in this.elementDatas)
        {
            this.elementList.Add(element, element.RandomWeighted);
        }
    }

    /// <summary>
    /// ���� ����ġ �׽�Ʈ
    /// </summary>
    private void TestProbability()
    {
        float count = 10000;
        int[] list = new int[this.elementDatas.Count];

        for (int i = 0; i < count; i++)
        {
            ElementBlueprint testElement = this.ElementList.Get();

            for (int k = 0; k < this.elementDatas.Count; k++)
            {
                if (testElement == this.elementDatas[k])
                {
                    list[k]++;
                }
            }
        }

        for (int i = 0; i < list.Length; i++)
        {
            Debug.Log(string.Format("{0} : {1}%\n", this.elementDatas[i].name, list[i] / count * 100));
        }
    }

    /// <summary>
    /// board elements ����
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
                element.Set(this, this.ElementList.Get());

                this.Elements.Add(element);
            }
        }

        // �÷��̾ ������ �Ҹ�ǰ ����
        BattlePlayer.Instance.SetUseableItems();

        // ����ġ �׽�Ʈ ��
        //TestProbability();
    }

    /// <summary>
    /// ��ȯ�Ǵ� elements �߿� ���� �Ⱦ�
    /// </summary>
    /// <returns></returns>
    public GameBoardElement RandomElement()
    {
        while (true)
        {
            var random = Random.Range(0, this.Elements.Count);
            var randomElement = this.Elements[random];
            if (randomElement.ElementDetailType == ElementDetailType.Normal) return randomElement;
        }
    }

    public bool IsSkillElementsEmpty()
    {
        if (this.elementSkillDatas.Count == 0) return true;

        return false;
    }

    /// <summary>
    /// ��ȯ�Ǵ� elements �߿� ���� �Ⱦ�
    /// </summary>
    /// <returns></returns>
    public ElementBlueprint RandomSkillElement()
    {
        var random = Random.Range(0, this.elementSkillDatas.Count);
        var randomSkillElement = this.elementSkillDatas[random];
        
        // ��ų ī�� ��� �� List ��ȯ
        this.elementSkillDatas.Remove(randomSkillElement);
        this.elementSkillUsedDatas.Add(randomSkillElement);

        return randomSkillElement;
    }

    public void SkillElementReset(ElementBlueprint data)
    {
        this.elementSkillUsedDatas.Remove(data);
        this.elementSkillDatas.Add(data);
    }

    /// <summary>
    /// Coroutine => board �ȿ� �ִ� elements ���� �����϶� ���� ���
    /// </summary>
    /// <returns></returns>
    public Coroutine WaitForMovement()
    {
        return StartCoroutine(this.boardMovement.WaitForMovement());
    }

    /// <summary>
    /// Coroutine => elements ���� selection ������ �̾����� ���� ���
    /// </summary>
    /// <returns></returns>
    public Coroutine WaitForSelection()
    {
        return StartCoroutine(this.boardInput.WaitForSelection());
    }

    /// <summary>
    /// Coroutine => ���õ� elements ���� �Ҹ�� �� ���� ���
    /// </summary>
    /// <returns></returns>
    public Coroutine RespawnElements()
    {
        return StartCoroutine(this.boardSpawning.Respawn());
    }

    /// <summary>
    /// Coroutine => elements ���� �� ���õ� �� ���� ���
    /// </summary>
    /// <returns></returns>
    public Coroutine DespawnSelection()
    {
        return StartCoroutine(this.boardSpawning.Despawn(GetSelectElements()));
    }
}
