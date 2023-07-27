using PSW.Core.Structs;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattlePlayerUseableItem : MonoBehaviour
{
    public bool IsUsed { get; set; }

    [SerializeField] private Image image;

    private GameBoard board;
    private Button button;
    private ItemBlueprintPotion useableData;
    private AbilityData ability;

    private ElementBlueprint element;

    public void Set(ItemBlueprintPotion data)
    {
        this.IsUsed = false;
        this.useableData = data;
        this.ability = data.Ability;
        this.image.sprite = data.ItemImage;

        this.element = data.ChangeElement;

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

        foreach (GameBoardElement element in this.board.Elements)
        {
            element.Despawn();
        }

        yield return YieldCache.WaitForSeconds(0.25f);

        foreach (GameBoardElement element in this.board.Elements)
        {
            element.SetData(this.element);
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
