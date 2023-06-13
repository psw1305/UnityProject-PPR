using UnityEngine;
using UnityEngine.UI;

public class ControlStepUI : MonoBehaviour
{
    public ControlStep[] steps;

    [SerializeField] protected int currentStep = 0;

    /// <summary>
    /// �¿� ��ư���� Step�� �����ϴ� ��Ʈ�� �ڽ�
    /// ��� �� => �����, ȿ����, ��Ÿ �ΰ��ӳ� Step ���� (������, ��ȭ)
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
    /// �� Step ���
    /// </summary>
    protected void StepUp()
    {
        this.currentStep++;
        steps[this.currentStep - 1].Up();
    }

    /// <summary>
    /// �� Step �ϰ�
    /// </summary>
    protected void StepDown()
    {
        steps[this.currentStep - 1].Down();
        this.currentStep--;
    }

    /// <summary>
    /// �� currentStep => 0 ~ 1 float ������ ��ȯ
    /// </summary>
    protected float StepValue(int currentStep)
    {
        return currentStep / (float)maxStep;
    }

    /// <summary>
    /// ������ Step ����
    /// </summary>
    protected virtual void LeftControl() { }

    /// <summary>
    /// ������ Step ����
    /// </summary>
    protected virtual void RightControl() { }
}
