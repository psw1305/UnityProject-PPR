using UnityEngine;

public class GameManager : BehaviourSingleton<GameManager>
{
    [Header("Camera")]
    [SerializeField] private Camera mainCamera;
    public Camera MainCamera => this.mainCamera;

    protected override void Awake()
    {
        base.Awake();

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = DataFrame.Instance.dFPS;
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
