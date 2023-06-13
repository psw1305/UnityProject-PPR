using UnityEngine;
using UnityEditor;

/// <summary>
/// ��ü���� ������ ������ ����
/// Game Data ���ַ� �ε�,����
/// </summary>
public class GameData : BehaviourSingleton<GameData>
{
    // ������ �ҷ�����
    private void Start()
    {
        DataSystem.Instance.LoadFromFile();
    }

    // ���� ����
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