using PSW.Core.Enums;
using PSW.Core.Extensions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [Header("Front UI - Stat")]
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI actText;
    [SerializeField] private TextMeshProUGUI cashText;

    [Header("Front UI - Button")]
    [SerializeField] private Button settings;
    [SerializeField] private Button inventory;
    [SerializeField] private Button cardDeck;

    [Header("Inventory")]
    [SerializeField] private CanvasGroup inventoryCanvas;
    [SerializeField] private Button inventoryClose;
    [SerializeField] private Transform relicAltar;
    [SerializeField] private InventorySlotPotion[] potionBelt;

    [Header("Card Deck")]
    [SerializeField] private CanvasGroup cardDeckCanvas;
    [SerializeField] private Button cardDeckClose;
    [SerializeField] private Transform cardInvenList;
    [SerializeField] private InventorySlotCard[] cards;

    [Header("Game Result")]
    [SerializeField] private CanvasGroup resultCanvas;
    [SerializeField] private GameObject gameClearTitle;
    [SerializeField] private GameObject gameOverTitle;
    [SerializeField] private Button gameEndButton;

    private void Awake()
    {
        this.inventoryCanvas.CanvasInit();
        this.inventory.onClick.AddListener(InventoryShow);
        this.inventoryClose.onClick.AddListener(InventoryHide);

        this.cardDeckCanvas.CanvasInit();
        this.cardDeck.onClick.AddListener(CardDeckShow);
        this.cardDeckClose.onClick.AddListener(CardDeckHide);

        this.settings.onClick.AddListener(SettingShow);

        this.resultCanvas.CanvasInit();
        this.gameEndButton.onClick.AddListener(GameEnd);
    }

    /// <summary>
    /// �κ��丮 â ����
    /// </summary>
    private void InventoryShow()
    {
        UISFX.Instance.Play(UISFX.Instance.inventoryOpen);
        this.inventoryCanvas.CanvasFadeIn(Fade.CANVAS_FADE_TIME);
    }

    /// <summary>
    /// �κ��丮 â �ݱ�
    /// </summary>
    private void InventoryHide()
    {
        UISFX.Instance.Play(UISFX.Instance.inventoryClose);
        this.inventoryCanvas.CanvasFadeOut(Fade.CANVAS_FADE_TIME);
    }

    private void CardDeckShow()
    {
        UISFX.Instance.Play(UISFX.Instance.cardDeckOpen);
        this.cardDeckCanvas.CanvasFadeIn(Fade.CANVAS_FADE_TIME);
    }

    /// <summary>
    /// �κ��丮 â �ݱ�
    /// </summary>
    private void CardDeckHide()
    {
        UISFX.Instance.Play(UISFX.Instance.cardDeckClose);
        this.cardDeckCanvas.CanvasFadeOut(Fade.CANVAS_FADE_TIME);
    }

    private void SettingShow()
    {
        SettingsSystem.Instance.Show();
    }

    /// <summary>
    /// ���� ������ => ��� â ����
    /// </summary>
    public void EndCanvasShow()
    {
        if (Player.Instance.GameState == GameState.Victory)
        {
            AudioBGM.Instance.BGMEnd(AudioBGM.Instance.victory);

            this.gameClearTitle.SetActive(true);
        }
        else if (Player.Instance.GameState == GameState.Defeat)
        {
            AudioBGM.Instance.BGMEnd(AudioBGM.Instance.defeat);

            this.gameOverTitle.SetActive(true);
        }

        this.resultCanvas.CanvasFadeIn(Fade.CANVAS_FADE_TIME);
    }

    /// <summary>
    /// ��� â ���� ���� ���� ��ư Ŭ���� �̺�Ʈ
    /// </summary>
    public void GameEnd()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);
        this.gameEndButton.interactable = false;

        SceneLoader.Instance.LoadScene(SceneNames.Lobby);
    }

    /// <summary>
    /// Front UI Stat ����
    /// </summary>
    public void SetUI()
    {
        SetHPText();
        SetACTText();
        SetCashText();
    }

    public void SetHPText()
    {
        this.hpText.text = Player.Instance.GetHPText();
    }

    public void SetACTText()
    {
        this.actText.text = Player.Instance.GetACTText();
    }

    public void SetCashText()
    {
        this.cashText.text = Player.Instance.Cash.ToString();
    }

    /// <summary>
    /// ��� ���� => [0.����][1.����][2.����][3.�׼�����]
    /// </summary>
    /// <param name="num">���� ��ȣ</param>
    /// <param name="invenItem">������ ������</param>
    public void LoadCardDeck(int num, InventoryItem invenItem)
    {
        invenItem.transform.SetParent(this.cards[num].transform);
    }

    ///// <summary>
    ///// �Ҹ�ǰ ����
    ///// </summary>
    ///// <param name="num">���� ��ȣ</param>
    ///// <param name="invenItem">������ ������</param>
    //public void LoadPotionBelt(int num, InventoryItem invenItem)
    //{
    //    invenItem.SetSlotNumber(num);
    //    invenItem.SetParentAfterDrag(this.potionBelt[num].GetDropSlot());
    //    invenItem.transform.SetParent(this.potionBelt[num].GetDropSlot());
    //}

    ///// <summary>
    ///// ���� ������ ���� => �ٽ� �κ��丮 â����
    ///// </summary>
    ///// <param name="invenItem">������ ������</param>
    //public void CardUnload(InventoryItem invenItem)
    //{
    //    invenItem.SetSlotNumber(0);
    //    invenItem.transform.SetParent(this.cardInvenList);
    //}

    /// <summary>
    /// �κ��丮 ������ �巡�� ���۽� => �ش� ���� �ִϸ��̼�
    /// </summary>
    /// <param name="invenItem">�巡�� ������</param>
    public void ItemOnBeginDrag(InventoryItem invenItem)
    {
        if (invenItem.GetItemType() == ItemType.Card)
        {
            foreach (InventorySlotCard invenSlot in cards)
            {
                invenSlot.AnimatePlateImage(1.1f);
            }
        }
    }

    /// <summary>
    /// �κ��丮 ������ �巡�� ���۽� => �ش� ���� �ִϸ��̼�
    /// </summary>
    /// <param name="invenItem">�巡�� ������</param>
    public void ItemOnEndDrag(InventoryItem invenItem)
    {
        if (invenItem.GetItemType() == ItemType.Card)
        {
            foreach (InventorySlotCard invenSlot in cards)
            {
                invenSlot.AnimatePlateImage(1f);
            }
        }
    }
}
