using PSW.Core.Enums;
using PSW.Core.Extensions;
using UnityEngine;
using DG.Tweening;

public class GameBoardCard : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cardSprite;
    [SerializeField] private SpriteRenderer caseSprite;
    [SerializeField] private SpriteRenderer coverSprite;
    [SerializeField] private Transform particleCase;
    
    private GameBoard board;
    private ItemBlueprintCard data;

    public CardType CardType { get; private set; }
    public CardDetailType CardDetail { get; private set; }
    public bool IsMoving { get; private set; }
    public bool IsSpawned => this.gameObject.activeSelf;

    public string GetCardName()
    {
        return this.data.CardName;
    }

    public void Set(GameBoard board, ItemBlueprintCard data)
    {
        this.board = board;
        SetData(data);
    }

    /// <summary>
    /// ī�� ������ ��������
    /// </summary>
    /// <param name="data"></param>
    public void SetData(ItemBlueprintCard data)
    {
        this.data = data;
        this.cardSprite.sprite = data.ItemImage;
        this.cardSprite.color = data.CardColor;
        this.caseSprite.sprite = data.CardFrame;

        this.CardType = data.CardType;
        this.CardDetail = data.CardDetail;
    }

    public void Selected()
    {
        this.coverSprite.DOFade(0.5f, 0.1f);
    }

    public void Deselected()
    {
        this.coverSprite.DOFade(0.0f, 0.1f);
    }

    /// <summary>
    /// ī�� ������Ʈ �̵� 
    /// </summary>
    /// <param name="targetPos"></param>
    /// <param name="time"></param>
    public void Move(Vector3 targetPos, float time)
    {
        this.IsMoving = true;
        this.transform.MoveCoroutine(targetPos, time, () => { this.IsMoving = false; });
    }

    /// <summary>
    /// ī�� ������Ʈ Ȱ��ȭ �� spawn �ִϸ��̼�
    /// </summary>
    public void Spawn()
    {
        // select�� ī�� cover sprite ���� �ʱ�ȭ
        Color coverAlpha = this.coverSprite.color;
        coverAlpha.a = 0.0f;
        this.coverSprite.color = coverAlpha;

        this.gameObject.SetActive(true);

        Vector2 startScale = Vector2.one * 0.1f;
        Vector2 endScale = Vector2.one * 1.0f;

        this.transform.ScaleCoroutine(startScale, endScale, 0.2f);
    }

    /// <summary>
    /// ī�� despawn �ִϸ��̼� �� ������Ʈ ��Ȱ��ȭ
    /// </summary>
    public void Despawn()
    {
        Vector2 startScale = Vector2.one * 1.0f;
        Vector2 endScale = Vector2.one * 0.1f;

        this.transform.ScaleCoroutine(startScale, endScale, 0.2f, () => { Init(); });
    }

    private void Init()
    {
        if (this.CardDetail != CardDetailType.Normal && this.CardDetail != CardDetailType.Obstacle)
        {
            this.board.SkillCardReset(this.data);
        }

        this.gameObject.SetActive(false);
    }
}
