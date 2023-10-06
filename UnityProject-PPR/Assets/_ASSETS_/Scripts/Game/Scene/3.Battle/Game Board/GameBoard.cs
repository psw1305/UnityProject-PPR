using PSW.Core.Enums;
using PSW.Core.Probability;
using System.Collections.Generic;
using UnityEngine;

public partial class GameBoard : BehaviourSingleton<GameBoard>
{
    [Header("Board Settings")]
    [SerializeField] private int rowCount = 6;
    [SerializeField] private int columnCount = 6;
    [SerializeField] private int skillStack = 5;
    [SerializeField] private float rescaling = 0.25f;

    [Header("Card Settings")]
    [SerializeField] private List<ItemBlueprintCard> cards = new();
    [SerializeField] private List<ItemBlueprintCard> skillCards = new();
    [SerializeField] private List<ItemBlueprintCard> usedSkillCards = new();
    [SerializeField] private GameBoardCardList<ItemBlueprintCard> gameBoardCards = new();

    [Header("Additional")]
    [SerializeField] private GameBoardCountingText countingText;
    [SerializeField] private GameBoardSelectionLine selectionLine;
    [SerializeField] private GameObject battleCardPrefab;

    private GameBoardInput boardInput;
    private GameBoardMovement boardMovement;
    private GameBoardSpawning boardSpawning;

    public List<GameBoardCard> Cards { get; } = new List<GameBoardCard>();
    public GameBoardCardList<ItemBlueprintCard> GameBoardCards => this.gameBoardCards;
    public Vector2 StartCellPosition => this.BoardCenter - this.Rescaling * new Vector2(this.ColumnCount - 1, this.RowCount - 1) / 2.0f;
    public int RowCount => this.rowCount;
    public int ColumnCount => this.columnCount;
    public float Rescaling => this.rescaling;
    public Vector2 BoardCenter => this.transform.position;
    public Transform BoardContainer => this.transform;

    protected override void Awake()
    {
        base.Awake();

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
    /// �־��� ī�� ����ġ ������ ī�� ����Ʈ ���� 
    /// </summary>
    private void ProbabilityList()
    {
        foreach (var card in this.cards)
        {
            this.gameBoardCards.Add(card, card.CardWeighted);
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
                this.skillCards.Add(card.GetCardData());
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
                card.Set(this, this.GameBoardCards.Get());

                this.Cards.Add(card);
            }
        }

        // �÷��̾ ������ �Ҹ�ǰ ����
        //BattlePlayer.Instance.SetPotions();
    }

    /// <summary>
    /// �Ϲ� ī�� �߿� �������� �ϳ� �Ⱦ�
    /// </summary>
    /// <returns></returns>
    public GameBoardCard RandomCardFromNormal()
    {
        if (AllCardsIsNotNormal()) return null;

        while (true)
        {
            var random = Random.Range(0, this.Cards.Count);
            var randomCard = this.Cards[random];
            
            if (randomCard.CardDetailType == CardDetailType.Normal)
            {
                return randomCard;
            }

            // ���� ���� �˻�
            InfiniteLoopDetector.Run();
        }
    }

    /// <summary>
    /// ��ȯ�Ǵ� ī�� �߿� ���� �Ⱦ�
    /// </summary>
    /// <returns></returns>
    public ItemBlueprintCard SkillCardRandomPickUp()
    {
        var random = Random.Range(0, this.skillCards.Count);
        var randomSkillCard = this.skillCards[random];
        
        // ��ų ī�� ��� �� List ��ȯ
        this.skillCards.Remove(randomSkillCard);
        this.usedSkillCards.Add(randomSkillCard);

        return randomSkillCard;
    }

    /// <summary>
    /// ���� ��ų ī�� �ʱ�ȭ
    /// </summary>
    /// <param name="data"></param>
    public void SkillCardReset(ItemBlueprintCard data)
    {
        this.usedSkillCards.Remove(data);
        this.skillCards.Add(data);
    }

    /// <summary>
    /// Ư�� ��ֹ� ī�� ���� ȱ��
    /// </summary>
    /// <returns></returns>
    public int ObstacleCardsCount()
    {
        int count = 0;

        foreach (var card in this.Cards)
        {
            if (card.CardDetailType == CardDetailType.Skull)
            {
                count++;
            }
        }

        return count;
    }

    #region Game Board Play Coroutine
    /// <summary>
    /// GameBoard �ȿ� �ִ� ī����� �����϶� ���� ���
    /// </summary>
    public Coroutine WaitForMovement()
    {
        return StartCoroutine(this.boardMovement.WaitForMovement());
    }

    /// <summary>
    /// ī�� ���� selection ������ �̾����� ���� ���
    /// </summary>
    public Coroutine WaitForSelection()
    {
        return StartCoroutine(this.boardInput.WaitForSelection());
    }

    /// <summary>
    /// ���õ� ī����� �Ҹ�� �� ���� ���
    /// </summary>
    public Coroutine RespawnCards()
    {
        return StartCoroutine(this.boardSpawning.Respawn());
    }

    /// <summary>
    /// ī����� �� ���õ� �� ���� ���
    /// </summary>
    public Coroutine DespawnSelection()
    {
        return StartCoroutine(this.boardSpawning.Despawn(GetSelectCards()));
    }

    /// <summary>
    /// ��ų ī�� ����
    /// </summary>
    public Coroutine SkillCardSpawn()
    {
        return StartCoroutine(this.boardSpawning.SkillCardSpawn());
    }

    /// <summary>
    /// ��ֹ� ī�� ����
    /// </summary>
    public Coroutine ObstacleCardSpawn(ItemBlueprintCard obstacleCard)
    {
        return StartCoroutine(this.boardSpawning.ObstacleCardSpawn(obstacleCard));
    }
    #endregion

    #region Game Board Check Function
    /// <summary>
    /// ��ų ī�尡 ������� ���
    /// </summary>
    /// <returns></returns>
    public bool IsSkillCardEmpty()
    {
        if (this.skillCards.Count == 0) return true;

        return false;
    }

    /// <summary>
    /// ���� ����� �Ϲ� ī�� �ϳ��� ���� ���
    /// </summary>
    /// <returns></returns>
    private bool AllCardsIsNotNormal()
    {
        foreach (var card in this.Cards)
        {
            if (card.CardDetailType == CardDetailType.Normal)
            {
                return false;
            }
        }

        return true;
    }
    #endregion
}
