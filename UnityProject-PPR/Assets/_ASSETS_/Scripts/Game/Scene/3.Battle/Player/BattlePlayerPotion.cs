using UnityEngine;
using UnityEngine.UI;

public class BattlePlayerPotion : MonoBehaviour
{
    public bool IsAvailable { get; set; }

    [SerializeField] private GameObject emptyPotion;
    [SerializeField] private GameObject availablePotion;
    [SerializeField] private Image potionImage;
    [SerializeField] private Button useButton;

    private InventoryItemPotion potion;

    private void Awake()
    {
        this.IsAvailable = false;
        this.useButton.onClick.AddListener(UsePotion);
    }

    public void Set(InventoryItemPotion potion)
    {
        this.potion = potion;
        this.potionImage.sprite = potion.GetBlueprint().ItemImage;
        this.name = potion.GetBlueprint().name;

        this.IsAvailable = true;
        this.emptyPotion.SetActive(false);
        this.availablePotion.SetActive(true);
    }

    /// <summary>
    /// Ȱ��ȭ �� ���� ���
    /// </summary>
    public void UsePotion()
    {
        // �� ������ ���
        if (this.IsAvailable == false)
        {
            BattleSFX.Instance.Play(BattleSFX.Instance.emptyPotion);
            return;
        }

        BattleSFX.Instance.Play(BattleSFX.Instance.playerUsePotion);

        this.IsAvailable = false;
        this.emptyPotion.SetActive(true);
        this.availablePotion.SetActive(false);

        if (Player.Instance == null) return;

        Player.Instance.RemovePotion(this.potion);
    }
}
