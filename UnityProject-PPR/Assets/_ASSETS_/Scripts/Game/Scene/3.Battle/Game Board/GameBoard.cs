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

    [Header("Card Settings")]
    [SerializeField] private List<ItemBlueprintCard> cardDatas = new();
    [SerializeField] private List<ItemBlueprintCard> cardSkillDatas = new();
    [SerializeField] private List<ItemBlueprintCard> cardSkillUsedDatas = new();
    [SerializeField] private GameBoardCardList<ItemBlueprintCard> cardList = new();

    [Header("Additional")]
    [SerializeField] private GameBoardCountingText countingText;
    [SerializeField] private GameBoardSelectionLine selectionLine;
    [SerializeField] private GameObject battleCardPrefab;

    private GameBoardInput boardInput;
    private GameBoardMovement boardMovement;
    private GameBoardSpawning boardSpawning;

    public List<GameBoardCard> Cards { get; } = new List<GameBoardCard>();
    public GameBoardCardList<ItemBlueprintCard> CardList => this.cardList;
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
    /// ���õ� ī�� ����Ʈ
    /// </summary>
    /// <returns></returns>
    public List<GameBoardCard> GetSelectCards()
    {
        return this.boardInput.SelectedCards;
    }

    /// <summary>
    /// �־��� ī�� ����ġ �� list ���� 
    /// </summary>
    private void ProbabilityList()
    {
        foreach (var card in this.cardDatas)
        {
            this.cardList.Add(card, card.CardWeighted);
        }
    }

    /// <summary>
    /// ���� ����ġ �׽�Ʈ
    /// </summary>
    private void TestProbability()
    {
        float count = 10000;
        int[] list = new int[this.cardDatas.Count];

        for (int i = 0; i < count; i++)
        {
            var test = this.CardList.Get();

            for (int k = 0; k < this.cardDatas.Count; k++)
            {
                if (test == this.cardDatas[k])
                {
                    list[k]++;
                }
            }
        }

        for (int i = 0; i < list.Length; i++)
        {
            Debug.Log(string.Format("{0} : {1}%\n", this.cardDatas[i].name, list[i] / count * 100));
        }
    }

    /// <summary>
    /// GameBoard ī�� ����
    /// </summary>
    public void SetBoard()
    {
        if (Player.Instance != null)
        {
            foreach (var card in Player.Instance.GetCardDeck())
            {
                this.cardSkillDatas.Add(card.GetCardData());
            }
        }

        for (int y = 0; y < this.ColumnCount; y++)
        {
            for (int x = 0; x < this.RowCount; x++)
            {
                var card = Instantiate(this.battleCardPrefab).GetComponent<GameBoardCard>();
                card.transform.localScale = Vector2.one;
                card.transform.position = this.BoardToWorldPosition(x, y);
                card.transform.SetParent(this.BoardContainer, true);
                card.Set(this, this.CardList.Get());

                this.Cards.Add(card);
            }
        }

        // �÷��̾ ������ �Ҹ�ǰ ����
        //BattlePlayer.Instance.SetPotions();

        // ����ġ �׽�Ʈ ��
        //TestProbability();
    }

    /// <summary>
    /// ��ȯ�Ǵ� ī�� �߿� ���� �Ⱦ�
    /// </summary>
    /// <returns></returns>
    public GameBoardCard RandomCard()
    {
        while (true)
        {
            var random = Random.Range(0, this.Cards.Count);
            var randomCard = this.Cards[random];
            if (randomCard.CardDetail == CardDetail.Normal) return randomCard;
        }
    }

    public bool IsSkillCardEmpty()
    {
        if (this.cardSkillDatas.Count == 0) return true;

        return false;
    }

    /// <summary>
    /// ��ȯ�Ǵ� ī�� �߿� ���� �Ⱦ�
    /// </summary>
    /// <returns></returns>
    public ItemBlueprintCard RandomSkillCard()
    {
        var random = Random.Range(0, this.cardSkillDatas.Count);
        var randomSkillCard = this.cardSkillDatas[random];
        
        // ��ų ī�� ��� �� List ��ȯ
        this.cardSkillDatas.Remove(randomSkillCard);
        this.cardSkillUsedDatas.Add(randomSkillCard);

        return randomSkillCard;
    }

    public void SkillCardReset(ItemBlueprintCard data)
    {
        this.cardSkillUsedDatas.Remove(data);
        this.cardSkillDatas.Add(data);
    }

    /// <summary>
    /// Coroutine => GameBoard �ȿ� �ִ� ī����� �����϶� ���� ���
    /// </summary>
    public Coroutine WaitForMovement()
    {
        return StartCoroutine(this.boardMovement.WaitForMovement());
    }

    /// <summary>
    /// Coroutine => ī�� ���� selection ������ �̾����� ���� ���
    /// </summary>
    public Coroutine WaitForSelection()
    {
        return StartCoroutine(this.boardInput.WaitForSelection());
    }

    /// <summary>
    /// Coroutine => ���õ� ī����� �Ҹ�� �� ���� ���
    /// </summary>
    public Coroutine RespawnCards()
    {
        return StartCoroutine(this.boardSpawning.Respawn());
    }

    /// <summary>
    /// Coroutine => ī����� �� ���õ� �� ���� ���
    /// </summary>
    public Coroutine DespawnSelection()
    {
        return StartCoroutine(this.boardSpawning.Despawn(GetSelectCards()));
    }

    public Coroutine SkillCardSpawn()
    {
        return StartCoroutine(this.boardSpawning.SkillCardSpawn());
    }
}
