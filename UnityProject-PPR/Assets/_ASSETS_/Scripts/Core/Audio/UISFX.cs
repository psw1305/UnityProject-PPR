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
    public AudioClip[] itemOpens;

    [Header("Item")]
    public AudioClip itemDrag;
    public AudioClip equipNormalDrop;
    public AudioClip equipHeavyDrop;
    public AudioClip equipMagicDrop;
    public AudioClip useableDrop;

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
    /// 인벤토리에서 아이템 드랍시 타입별로 소리 구분
    /// </summary>
    /// <param name="item"></param>
    public void ItemDropSFX(InventoryItem item)
    {
        if (item.GetItemType() == ItemType.Artifact)
        {
            switch (item.GetEquipmentType())
            {
                case EquipmentType.Armor:
                    Play(this.equipHeavyDrop);
                    break;

                case EquipmentType.Trinket:
                    Play(this.equipMagicDrop);
                    break;

                default:
                    Play(this.equipNormalDrop);
                    break;
            }
        }
        else
        {
            Play(this.useableDrop);
        }
    }
}
