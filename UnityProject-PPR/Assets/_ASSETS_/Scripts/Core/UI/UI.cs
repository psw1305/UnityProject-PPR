using UnityEngine;

/// <summary>
/// ��� UI ��ũ��Ʈ�� base class
/// </summary>
public abstract class UI : MonoBehaviour
{
    /// <summary>
    /// Update �󿡼� back button(�����) / esc(������) ������ �Լ� ȣ��
    /// </summary>
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnBackButtonClick();
        }
    }

    /// <summary>
    /// back Button/esc Ű ������ ȣ��
    /// </summary>
    protected abstract void OnBackButtonClick();
}
