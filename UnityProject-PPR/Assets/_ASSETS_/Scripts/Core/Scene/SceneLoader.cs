using PSW.Core.Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Scene 로딩하면서 화면에 특정 애니메이션 작동
/// </summary>
public class SceneLoader : BehaviourSingleton<SceneLoader>
{
    // 로딩 하는 동안 다른 동작 방지용
    public static bool IsLoaded { get; set; }

    [SerializeField] private RectTransform sceneSize;
    [SerializeField] private GridLayoutGroup gridLayout;

    [Header("Blind")]
    [SerializeField] private Transform[] blinds;
    [SerializeField] private float fadeDuration = 0.5f;

    protected override void Awake()
    {
        base.Awake();

        IsLoaded = true;

        SetBlindSize();
    }

    private void SetBlindSize()
    {
        var sizeX = this.sceneSize.sizeDelta.x / 3.0f;
        var sizeY = this.sceneSize.sizeDelta.y / 7.0f;

        this.gridLayout.cellSize = new Vector2(sizeX, sizeY);
    }

    /// <summary>
    /// 씬 마무리 => 플레이어 유무에 따른 씬 체크
    /// </summary>
    /// <param name="sceneName"></param>
    public void PlayerCheckSceneLoad(string sceneName)
    {
        // 플레이어 정보 있을 시 => 해당 씬 해제
        if (Player.Instance != null)
        {
            UnLoadAdditiveScene(sceneName);

            // 스테이지로 복귀시 BGM 변경
            AudioBGM.Instance.BGMChange(AudioBGM.Instance.stage);
        }
        // 플레이어 정보 없을 시 => 해당 씬 재진입 (테스트 용)
        else
        {
            LoadScene(sceneName);
        }
    }

    /// <summary>
    /// 씬에서 씬으로 자유 이동
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        StopAllCoroutines();
        StartCoroutine(AnimationFadeScene(LoadSceneCoroutine(sceneName)));
    }

    /// <summary>
    /// 코루틴 동안 Scene 화면에 페이딩 부드럽게
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);     

        yield return new WaitForSeconds(0.5f);
    }

    /// <summary>
    /// 스테이지 씬에서 씬 추가 (Starting, Battle, Shop, Mystery, RestSite, Treasure)
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

        StageSystem.Instance.StageActive(false);
    }

    /// <summary>
    /// 스테이지 씬에서 추가된 씬 다시 해제
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

        StageSystem.Instance.StageActive(true);        
    }

    /// <summary>
    /// 씬 전환시 작동하는 페이드 애니메이션
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
