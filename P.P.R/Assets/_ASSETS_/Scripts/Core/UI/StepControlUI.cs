using UnityEngine;
using UnityEngine.UI;

public class ControlStepUI : MonoBehaviour
{
    public ControlStep[] steps;

    [SerializeField] protected int currentStep = 0;

    /// <summary>
    /// 좌우 버튼으로 Step을 조정하는 컨트롤 박스
    /// 사용 예 => 배경음, 효과음, 기타 인게임내 Step 조절 (레벨업, 강화)
    /// </summary>
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    protected int maxStep;

    public virtual void Set(float currentValue) 
    {
        this.maxStep = steps.Length;

        for (int i = 0; i < this.maxStep; i++)
        {
            if (currentValue == 0)
            {
                this.currentStep = 0;
                break; 
            }
            else if (this.steps[i].value == currentValue)
            {
                this.currentStep = i + 1;
                SetSteps();
                break;
            }
        }

        this.leftButton.onClick.AddListener(LeftControl);
        this.rightButton.onClick.AddListener(RightControl);
    }

    private void SetSteps()
    {
        for (int i = 0; i < this.currentStep; i++)
        {
            steps[i].Up();
        }
    }

    /// <summary>
    /// 한 Step 상승
    /// </summary>
    protected void StepUp()
    {
        this.currentStep++;
        steps[this.currentStep - 1].Up();
    }

    /// <summary>
    /// 한 Step 하강
    /// </summary>
    protected void StepDown()
    {
        steps[this.currentStep - 1].Down();
        this.currentStep--;
    }

    /// <summary>
    /// 현 currentStep => 0 ~ 1 float 값으로 변환
    /// </summary>
    protected float StepValue(int currentStep)
    {
        return currentStep / (float)maxStep;
    }

    /// <summary>
    /// 누를시 Step 감소
    /// </summary>
    protected virtual void LeftControl() { }

    /// <summary>
    /// 누를시 Step 증가
    /// </summary>
    protected virtual void RightControl() { }
}
