using UnityEngine;
using TMPro;

public class Performance : MonoBehaviour
{
    // ������ �׽�Ʈ�� �ؽ�Ʈ
    public TextMeshProUGUI fpsText;
    private float deltaTime = 0;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = DataFrame.Instance.dFPS;
    }

    /// <summary>
    /// ��� ������ Ȯ��
    /// </summary>
    void Update()
    {
        if (fpsText == null) return;

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString();
    }
}
