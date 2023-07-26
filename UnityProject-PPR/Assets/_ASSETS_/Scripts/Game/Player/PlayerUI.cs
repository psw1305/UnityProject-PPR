using PSW.Core.Enums;
using PSW.Core.Extensions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : BehaviourSingleton<PlayerUI>
{
    [Header("Front UI")]
    [SerializeField] private Button settings;
    [SerializeField] private Button inventory;
    [SerializeField] private Button cardDeck;
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI cashText;

    [Header("Inventory")]
    [SerializeField] private CanvasGroup inventoryCanvas;
    [SerializeField] private Button inventoryClose;
    [SerializeField] private Transform relicAltar;
    [SerializeField] private InventorySlotPotion[] potionBelt;

    [Header("Card Deck")]
    [SerializeField] private CanvasGroup cardDeckCanvas;
    [SerializeField] private Button cardDeckClose;
    [SerializeField] private Transform cardDeckList;
    [SerializeField] private InventorySlotCard[] deckCards;

    [Header("Game End")]
    [SerializeField] private CanvasGroup endCanvas;
    [SerializeField] private GameObject endGameClearTitle;
    [SerializeField] private GameObject endGameOverTitle;
    [SerializeField] private Button endButton;

    protected override void Awake()
    {
        base.Awake();

        this.inventoryCanvas.CanvasInit();
        this.endCanvas.CanvasInit();

        this.inventory.onClick.AddListener(InventoryShow);
        this.inventoryClose.onClick.AddListener(InventoryHide);

        this.cardDeck.onClick.AddListener(CardDeckShow);
        this.cardDeckClose.onClick.AddListener(CardDeckHide);

        this.settings.onClick.AddListener(SettingShow);

        this.endButton.onClick.AddListener(GameEnd);
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
        UISFX.Instance.Play(UISFX.Instance.inventoryOpen);
        this.inventoryCanvas.CanvasFadeIn(Fade.CANVAS_FADE_TIME);
    }

    /// <summary>
    /// �κ��丮 â �ݱ�
    /// </summary>
    private void CardDeckHide()
    {
        UISFX.Instance.Play(UISFX.Instance.inventoryClose);
        this.inventoryCanvas.CanvasFadeOut(Fade.CANVAS_FADE_TIME);
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

            this.endGameClearTitle.SetActive(true);
        }
        else if (Player.Instance.GameState == GameState.Defeat)
        {
            AudioBGM.Instance.BGMEnd(AudioBGM.Instance.defeat);

            this.endGameOverTitle.SetActive(true);
        }

        this.endCanvas.CanvasFadeIn(Fade.CANVAS_FADE_TIME);
    }

    /// <summary>
    /// ��� â ���� ���� ���� ��ư Ŭ���� �̺�Ʈ
    /// </summary>
    public void GameEnd()
    {
        UISFX.Instance.Play(UISFX.Instance.buttonClick);
        this.endButton.interactable = false;

        SceneLoader.Instance.LoadScene(SceneNames.Lobby);
    }

    /// <summary>
    /// Player UI ����
    /// </summary>
    public void SetUI()
    {
        SetHealthUI();
        SetCashUI();
    }

    public void SetHealthUI()
    {
        this.health.text = Player.Instance.GetHpText();
    }

    /// <summary>
    /// Player ��ȭ UI ǥ��
    /// </summary>
    public void SetCashUI()
    {
        this.cashText.text = Player.Cash.ToString();
    }

    /// <summary>
    /// ��� ���� => [0.����][1.����][2.����][3.�׼�����]
    /// </summary>
    /// <param name="num">���� ��ȣ</param>
    /// <param name="invenItem">������ ������</param>
    public void LoadCardDeck(int num, InventoryItem invenItem)
    {
        invenItem.transform.SetParent(this.deckCards[num].transform);
    }

    /// <summary>
    /// �Ҹ�ǰ ����
    /// </summary>
    /// <param name="num">���� ��ȣ</param>
    /// <param name="invenItem">������ ������</param>
    public void LoadPotionBelt(int num, InventoryItem invenItem)
    {
        invenItem.SetSlotNumber(num);
        invenItem.SetParentAfterDrag(this.potionBelt[num].GetDropSlot());
        invenItem.transform.SetParent(this.potionBelt[num].GetDropSlot());
    }

    /// <summary>
    /// ���� ������ ���� => �ٽ� �κ��丮 â����
    /// </summary>
    /// <param name="invenItem">������ ������</param>
    public void CardUnload(InventoryItem invenItem)
    {
        invenItem.SetSlotNumber(0);
        invenItem.transform.SetParent(this.cardDeckList);
    }

    /// <summary>
    /// �κ��丮 ������ �巡�� ���۽� => �ش� ���� �ִϸ��̼�
    /// </summary>
    /// <param name="invenItem">�巡�� ������</param>
    public void ItemOnBeginDrag(InventoryItem invenItem)
    {
        if (invenItem.GetItemType() == ItemType.Card)
        {
            foreach (InventorySlotCard invenSlot in deckCards)
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
            foreach (InventorySlotCard invenSlot in deckCards)
            {
                invenSlot.AnimatePlateImage(1f);
            }
        }
    }
}
