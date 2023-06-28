using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class AudioBGM : AudioSystem<AudioBGM>
{
    public AudioClip lobby;
    public AudioClip stage;

    [Header("Event")]
    public AudioClip shop;
    public AudioClip restsite;

    [Header("Battle")]
    public AudioClip elite;
    public AudioClip boss;

    [Header("Game End")]
    public AudioClip victory;
    public AudioClip defeat;

    private AudioSource AudioSource { get; set; }

    private float volumeBGMScale = 1.0f;
    public float VolumeBGMScale
    {
        get
        {
            return this.volumeBGMScale;
        }
        set
        {
            this.volumeBGMScale = Mathf.Clamp01(value);
            SetVolume(this.volumeBGMScale);
        }
    }


    protected override void Awake()
    {
        base.Awake();
        this.AudioSource = GetComponent<AudioSource>();

        //SetBGM();
    }

    protected override void SetVolume(float volumeScale)
    {
        SetVolume(AudioNames.BGM, volumeScale);
    }

    private void SetBGM()
    {
        var scene = SceneManager.GetActiveScene();

        switch (scene.buildIndex)
        {
            case 0:
                BGMPlay(this.lobby);
                break;
            case 3:
                BGMPlay(this.restsite);
                break;
            case 5:
                BGMPlay(this.shop);
                break;
            default:
                BGMPlay(this.stage);
                break;
        }
    }

    public void BGMPlay(AudioClip clip)
    {
        this.AudioSource.clip = clip;
        this.AudioSource.Play();
    }

    public void BGMChange(AudioClip clip)
    {
        if (this.AudioSource.clip == clip) return;
        if (!this.AudioSource.loop) this.AudioSource.loop = true;

        this.AudioSource
            .DOFade(0, 1.2f)
            .OnComplete(() => BGMPlay(clip));

        this.AudioSource
            .DOFade(1, 1.2f)
            .SetDelay(1.5f);
    }

    public void BGMEnd(AudioClip clip)
    {
        this.AudioSource.loop = false;

        this.AudioSource
            .DOFade(0, 0.25f)
            .OnComplete(() => BGMPlay(clip));

        this.AudioSource
            .DOFade(1, 0.25f)
            .SetDelay(0.3f);
    }
}
