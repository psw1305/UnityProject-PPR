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
}
