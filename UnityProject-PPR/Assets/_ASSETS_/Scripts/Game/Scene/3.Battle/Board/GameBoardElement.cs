using PSW.Core.Enums;
using PSW.Core.Extensions;
using UnityEngine;
using DG.Tweening;

public class GameBoardElement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer caseSprite;
    [SerializeField] private SpriteRenderer coverSprite;
    [SerializeField] private SpriteRenderer elementSprite;
    [SerializeField] private Transform particleCase;
    [SerializeField] private ElementType elementType;
    [SerializeField] private ElementAttack elementAttack;
    [SerializeField] private float randomWeighted = 0;

    public ElementType ElementType
    {
        get { return this.elementType; }
        set { this.elementType = value; }
    }

    public bool IsMoving { get; private set; }
    public bool IsSpawned => this.gameObject.activeSelf;

    /// <summary>
    /// ElementBaseData(Scriptable Object) ������ ��������
    /// </summary>
    /// <param name="data"></param>
    public void SetBaseData(ElementBlueprint data)
    {
        this.caseSprite.sprite = data.ElementCaseImage;
        this.elementSprite.sprite = data.ElementImage;
        this.elementSprite.color = data.ElementColor;
        this.ElementType = data.ElementType;
        this.randomWeighted = data.RandomWeighted;
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
    /// element ������Ʈ �̵� 
    /// </summary>
    /// <param name="targetPos"></param>
    /// <param name="time"></param>
    public void Move(Vector3 targetPos, float time)
    {
        this.IsMoving = true;
        this.transform.MoveCoroutine(targetPos, time, () => { this.IsMoving = false; });
    }

    /// <summary>
    /// element ������Ʈ Ȱ��ȭ �� spawn �ִϸ��̼�
    /// </summary>
    public void Spawn()
    {
        // select�� element cover sprite ���� �ʱ�ȭ
        Color coverAlpha = this.coverSprite.color;
        coverAlpha.a = 0.0f;
        this.coverSprite.color = coverAlpha;

        this.gameObject.SetActive(true);

        Vector2 startScale = Vector2.one * 0.1f;
        Vector2 endScale = Vector2.one * 1.0f;

        this.transform.ScaleCoroutine(startScale, endScale, 0.2f);
    }

    /// <summary>
    /// element despawn �ִϸ��̼� �� ������Ʈ ��Ȱ��ȭ
    /// </summary>
    public void Despawn()
    {
        Vector2 startScale = Vector2.one * 1.0f;
        Vector2 endScale = Vector2.one * 0.1f;

        this.transform.ScaleCoroutine(startScale, endScale, 0.2f, () => { this.gameObject.SetActive(false); });
    }
}
