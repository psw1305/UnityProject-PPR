using PSW.Core.Enums;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UISFX : BehaviourSingleton<UISFX>
{
    [Header("UI")]
    public AudioClip buttonClick;
    public AudioClip buttonAppear;

    [Header("Player Inventory")]
    public AudioClip inventoryOpen;
    public AudioClip inventoryClose;
    public AudioClip cardDeckOpen;
    public AudioClip cardDeckClose;
    public AudioClip[] itemOpens;

    [Header("Item Drop")]
    public AudioClip dropCard;
    public AudioClip dropRelic;
    public AudioClip dropPotion;
    public AudioClip dropCash;

    [Header("Mystery")]
    public AudioClip healthUp;
    public AudioClip itemGain;
    public AudioClip cashGain;

    [Header("Map")]
    public AudioClip[] mapClicks;

    [Header("Shop")]
    public AudioClip shopBell;
    public AudioClip shopBuy;

    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        this.audioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip)
    {
        AudioSFX.Instance.PlayOneShot(clip);
    }

    public void Play(AudioClip[] clips)
    {
        var ran = Random.Range(0, clips.Length);
        AudioSFX.Instance.PlayOneShot(clips[ran]);
    }

    /// <summary>
    /// ������ Ÿ�Ժ��� ���� ����
    /// </summary>
    /// <param name="itemType">������ Ÿ��</param>
    public void ItemDropSFX(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Card:
                Play(this.dropCard);
                break;
            case ItemType.Relic:
                Play(this.dropRelic);
                break;
            case ItemType.Potion:
                Play(this.dropPotion);
                break;
            case ItemType.Cash:
                Play(this.dropCash);
                break;
        }
    }
}
