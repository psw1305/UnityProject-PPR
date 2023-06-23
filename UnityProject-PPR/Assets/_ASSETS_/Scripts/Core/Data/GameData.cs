using UnityEngine;
using UnityEditor;

/// <summary>
/// 전체적인 게임의 진행을 관리
/// Game Data 위주로 로드,저장
/// </summary>
public class GameData : BehaviourSingleton<GameData>
{
    // 데이터 불러오기
    private void Start()
    {
        DataSystem.Instance.LoadFromFile();
    }

    // 게임 종료
    private void OnApplicationQuit()
    {
        DataSystem.Instance.SaveToFile();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GameData))]
public class GameSystem_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var o = (GameData)target;
        if (GUILayout.Button("Save To File"))
        {
            DataSystem.Instance.SaveToFile();
        }

        if (GUILayout.Button("Load From File"))
        {
            DataSystem.Instance.LoadFromFile();
        }
    }
}
#endif