using UnityEngine;

/// <summary>
/// 모든 UI 스크립트의 base class
/// </summary>
public abstract class UI : MonoBehaviour
{
    /// <summary>
    /// Update 상에서 back button(모바일) / esc(윈도우) 누를시 함수 호출
    /// </summary>
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnBackButtonClick();
        }
    }

    /// <summary>
    /// back Button/esc 키 누를시 호출
    /// </summary>
    protected abstract void OnBackButtonClick();
}
