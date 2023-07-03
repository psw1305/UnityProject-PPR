using UnityEngine;
using TMPro;

public class GameManager : BehaviourSingleton<GameManager>
{
    [SerializeField] private Camera mainCamera;
    public Camera MainCamera => this.mainCamera;

    // 프레임 테스트용 텍스트
    public TextMeshProUGUI fpsText;
    private float deltaTime = 0;

    protected override void Awake()
    {
        base.Awake();

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = DataFrame.Instance.dFPS;
    }

    /// <summary>
    /// 향시 프레임 확인
    /// </summary>
    void Update()
    {
        if (fpsText == null) return;

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString();
    }

    public void CameraChange(Canvas canvas)
    {
        canvas.worldCamera = this.MainCamera;
    }

    public void CameraChange(Camera preCamra, Canvas canvas)
    {
        canvas.worldCamera = this.MainCamera;
        preCamra.enabled = false;
    }
}
