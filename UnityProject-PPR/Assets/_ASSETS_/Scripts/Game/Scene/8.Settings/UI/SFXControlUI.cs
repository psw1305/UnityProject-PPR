public class SFXControlUI : ControlStepUI
{
    protected override void LeftControl()
    {
        if (this.currentStep > 0)
        {
            StepDown();
            DataFrame.Instance.dSFX = StepValue(this.currentStep);
        }
        else if(this.currentStep == 0)
        {
            DataFrame.Instance.dSFX = 0.0f;
        }

        UISFX.Instance.Play(UISFX.Instance.buttonClick);
    }

    protected override void RightControl()
    {
        if (this.currentStep < this.maxStep)
        {
            StepUp();
            DataFrame.Instance.dSFX = StepValue(this.currentStep);
        }
        else if (this.currentStep == this.maxStep)
        {
            DataFrame.Instance.dSFX = 1.0f;
        }

        UISFX.Instance.Play(UISFX.Instance.buttonClick);
    }
}
