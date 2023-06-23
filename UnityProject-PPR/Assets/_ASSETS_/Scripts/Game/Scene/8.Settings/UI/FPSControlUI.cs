using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPSControlUI : MonoBehaviour
{
    public int[] fpsSteps;

    [SerializeField] private TextMeshProUGUI stepText;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    private int currentStep = 0;

    public virtual void Set(int currentFPS)
    {
        for (int i = 0; i < this.fpsSteps.Length; i++)
        {
            if (this.fpsSteps[i] == currentFPS)
            {
                this.currentStep = i;
                this.stepText.text = this.fpsSteps[currentStep].ToString();
            }
        }

        this.leftButton.onClick.AddListener(LeftControl);
        this.rightButton.onClick.AddListener(RightControl);
    }

    /// <summary>
    /// 누를시 Step 감소
    /// </summary>
    private void LeftControl() 
    {        
        if (this.currentStep > 0)
        {
            this.currentStep--;
            this.stepText.text = this.fpsSteps[this.currentStep].ToString();
            DataFrame.Instance.dFPS = this.fpsSteps[this.currentStep];
        }

        UISFX.Instance.Play(UISFX.Instance.buttonClick);
    }

    /// <summary>
    /// 누를시 Step 증가
    /// </summary>
    private void RightControl() 
    {
        if (this.currentStep < this.fpsSteps.Length - 1)
        {
            this.currentStep++;
            this.stepText.text = this.fpsSteps[this.currentStep].ToString();
            DataFrame.Instance.dFPS = this.fpsSteps[this.currentStep];
        }

        UISFX.Instance.Play(UISFX.Instance.buttonClick);
    }
}
