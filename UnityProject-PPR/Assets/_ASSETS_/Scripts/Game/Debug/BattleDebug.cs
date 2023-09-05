using PSW.Core.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class BattleDebug : MonoBehaviour
{
    private bool isDebug;

    [Header("UI")]
    [SerializeField] private Button debugButton;
    [SerializeField] private CanvasGroup debugCanvas;

    private void Awake()
    {
        this.isDebug = false;

        this.debugCanvas.CanvasInit();
        this.debugButton.onClick.AddListener(DebugButtonClick);
    }


    private void DebugButtonClick()
    {
        if (!this.isDebug)
        {
            this.debugCanvas.CanvasFadeIn(DUR.CANVAS_FADE_TIME);
        }
        else
        {
            this.debugCanvas.CanvasFadeOut(DUR.CANVAS_FADE_TIME);
        }

        this.isDebug = !this.isDebug;
    }
}
