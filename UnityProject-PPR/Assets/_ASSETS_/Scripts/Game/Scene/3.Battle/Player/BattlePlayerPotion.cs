using PSW.Core.Structs;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattlePlayerPotion : MonoBehaviour
{
    public bool IsUsed { get; set; }

    [SerializeField] private Image image;

    private GameBoard board;
    private Button button;
    private ItemBlueprintPotion useableData;
    private AbilityData ability;

    private ItemBlueprintCard card;

    public void Set(ItemBlueprintPotion data)
    {
        this.IsUsed = false;
        this.useableData = data;
        this.ability = data.Ability;
        this.image.sprite = data.ItemImage;

        this.card = data.ChangeCard;

        this.button = GetComponent<Button>();
        this.button.onClick.AddListener(Selected);

        // 프리팹 네임 => 데이터 네임으로 변경
        this.name = data.name;
    }

    public void Selected()
    {
        if (IsUsed)
        {
        }
        else
        {
        }

        IsUsed = !IsUsed;
    }

    /// <summary>
    /// 아이템 선택 후 사용
    /// </summary>
    /// <param name="board"></param>
    public void Used(GameBoard board)
    {
        if (this.IsUsed) return;
        
        this.board = board;
        StartCoroutine(AllElementsChanged());

        this.IsUsed = !this.IsUsed;
    }

    public IEnumerator AllElementsChanged()
    {
        // 효과음 부여
        //GameUISFX.Instance.Play(GameUISFX.Instance.clickClip);

        foreach (GameBoardCard element in this.board.Cards)
        {
            element.Despawn();
        }

        yield return YieldCache.WaitForSeconds(0.25f);

        foreach (GameBoardCard element in this.board.Cards)
        {
            element.SetData(this.card);
            element.Spawn();
        }

        yield return YieldCache.WaitForSeconds(0.25f);
    }

    /// <summary>
    /// 아이템 초기화
    /// </summary>
    public void Init()
    {
        if (!this.IsUsed) return;

        this.IsUsed = false;
    }
}
