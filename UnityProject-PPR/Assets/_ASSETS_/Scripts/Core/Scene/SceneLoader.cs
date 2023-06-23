using PSW.Core.Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Scene �ε��ϸ鼭 ȭ�鿡 Ư�� �ִϸ��̼� �۵�
/// </summary>
public class SceneLoader : BehaviourSingleton<SceneLoader>
{
    // �ε� �ϴ� ���� �ٸ� ���� ������
    public static bool IsLoaded { get; set; }

    [Header("Blind")]
    [SerializeField] private Transform[] blinds;
    [SerializeField] private float fadeDuration = 0.5f;

    protected override void Awake()
    {
        base.Awake();

        IsLoaded = true;

        SetBlindTransforms();
    }

    private void SetBlindTransforms()
    {
        this.blinds = new Transform[this.transform.childCount];

        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.blinds[i] = this.transform.GetChild(i);
        }
    }

    /// <summary>
    /// �� ������ => �÷��̾� ������ ���� �� üũ
    /// </summary>
    /// <param name="sceneName"></param>
    public void PlayerCheckSceneLoad(string sceneName)
    {
        // �÷��̾� ���� ���� �� => �ش� �� ����
        if (Player.Instance != null)
        {
            UnLoadAdditiveScene(sceneName);

            // ���������� ���ͽ� BGM ����
            AudioBGM.Instance.BGMChange(AudioBGM.Instance.stage);
        }
        // �÷��̾� ���� ���� �� => �ش� �� ������ (�׽�Ʈ ��)
        else
        {
            LoadScene(sceneName);
        }
    }

    /// <summary>
    /// ������ ������ ���� �̵�
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        StopAllCoroutines();
        StartCoroutine(AnimationFadeScene(LoadSceneCoroutine(sceneName)));
    }

    /// <summary>
    /// �ڷ�ƾ ���� Scene ȭ�鿡 ���̵� �ε巴��
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);     

        yield return new WaitForSeconds(0.5f);

        NoticeSystem.Instance.Init();
    }

    /// <summary>
    /// �������� ������ �� �߰� (Starting, Battle, Shop, Mystery, RestSite, Treasure)
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadAdditiveScene(string sceneName)
    {
        StopAllCoroutines();
        StartCoroutine(AnimationFadeScene(LoadAdditiveSceneCoroutine(sceneName)));
    }

    private IEnumerator LoadAdditiveSceneCoroutine(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        
        yield return new WaitForSeconds(0.5f);

        NoticeSystem.Instance.Init();
        StageSystem.Instance.StageActive(false);
    }

    /// <summary>
    /// �������� ������ �߰��� �� �ٽ� ����
    /// </summary>
    /// <param name="sceneName"></param>
    public void UnLoadAdditiveScene(string sceneName)
    {
        StopAllCoroutines();
        StartCoroutine(AnimationFadeScene(UnLoadAdditiveSceneCoroutine(sceneName)));
    }

    private IEnumerator UnLoadAdditiveSceneCoroutine(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
        
        yield return new WaitForSeconds(0.5f);

        NoticeSystem.Instance.Init();
        StageSystem.Instance.StageActive(true);        
    }

    /// <summary>
    /// �� ��ȯ�� �۵��ϴ� ���̵� �ִϸ��̼�
    /// </summary>
    /// <param name="coroutine"></param>
    /// <returns></returns>
    private IEnumerator AnimationFadeScene(IEnumerator coroutine)
    {
        IsLoaded = false;
        
        yield return StartCoroutine(this.blinds.FadeInCoroutine(this.fadeDuration));

        SettingsSystem.Instance.Init();
        yield return StartCoroutine(coroutine);

        yield return StartCoroutine(this.blinds.FadeOutCoroutine(this.fadeDuration));
        
        IsLoaded = true;
    }
}