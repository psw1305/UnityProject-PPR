using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class AudioBGM : AudioSystem<AudioBGM>
{
    public AudioClip lobby;
    public AudioClip stage;
    public AudioClip elite;
    public AudioClip boss;
    public AudioClip shop;
    public AudioClip restsite;

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

        SetBGM();
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

        this.AudioSource
            .DOFade(0, 1.2f)
            .OnComplete(() => BGMPlay(clip));

        this.AudioSource
            .DOFade(1, 1.2f)
            .SetDelay(1.5f);
    }
}
