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

        // 가중치 값 list 생성
        ProbabilityList();
    }

    /// <summary>
    /// 선택된 카드 리스트
    /// </summary>
    /// <returns></returns>
    public List<GameBoardCard> GetSelectCards()
    {
        return this.boardInput.SelectedCards;
    }

    /// <summary>
    /// 주어진 카드 가중치 값 list 생성 
    /// </summary>
    private void ProbabilityList()
    {
        foreach (var card in this.cardDatas)
        {
            this.cardList.Add(card, card.CardWeighted);
        }
    }

    /// <summary>
    /// 랜덤 가중치 테스트
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
    /// GameBoard 카드 생성
    /// </summary>
    public void SetBoard()
    {
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

        // 플레이어가 장착한 소모품 생성
        //BattlePlayer.Instance.SetPotions();

        // 가중치 테스트 용
        //TestProbability();
    }

    /// <summary>
    /// 변환되는 카드 중에 랜덤 픽업
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
    /// 변환되는 카드 중에 랜덤 픽업
    /// </summary>
    /// <returns></returns>
    public ItemBlueprintCard RandomSkillCard()
    {
        var random = Random.Range(0, this.cardSkillDatas.Count);
        var randomSkillCard = this.cardSkillDatas[random];
        
        // 스킬 카드 사용 후 List 변환
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
    /// Coroutine => GameBoard 안에 있는 카드들이 움직일때 까지 대기
    /// </summary>
    public Coroutine WaitForMovement()
    {
        return StartCoroutine(this.boardMovement.WaitForMovement());
    }

    /// <summary>
    /// Coroutine => 카드 끼리 selection 라인이 이어지기 까지 대기
    /// </summary>
    public Coroutine WaitForSelection()
    {
        return StartCoroutine(this.boardInput.WaitForSelection());
    }

    /// <summary>
    /// Coroutine => 선택된 카드들이 소멸될 때 까지 대기
    /// </summary>
    public Coroutine RespawnCards()
    {
        return StartCoroutine(this.boardSpawning.Respawn());
    }

    /// <summary>
    /// Coroutine => 카드들이 다 선택될 때 까지 대기
    /// </summary>
    public Coroutine DespawnSelection()
    {
        return StartCoroutine(this.boardSpawning.Despawn(GetSelectCards()));
    }
}
