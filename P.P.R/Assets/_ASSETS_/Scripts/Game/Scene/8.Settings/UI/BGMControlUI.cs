public class BGMControlUI : ControlStepUI
{
    protected override void LeftControl()
    {
        if (this.currentStep > 0)
        {
            StepDown();
            DataFrame.Instance.dBGM = StepValue(this.currentStep);
        }

        UISFX.Instance.Play(UISFX.Instance.buttonClick);
    }

    protected override void RightControl()
    {
        if (this.currentStep < this.maxStep)
        {
            StepUp();
            DataFrame.Instance.dBGM = StepValue(this.currentStep);
        }

        UISFX.Instance.Play(UISFX.Instance.buttonClick);
    }
}
