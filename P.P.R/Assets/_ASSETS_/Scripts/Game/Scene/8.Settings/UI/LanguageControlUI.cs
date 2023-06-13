using UnityEngine;
using UnityEngine.UI;

public class LanguageControlUI : MonoBehaviour
{
    [SerializeField] private Toggle koreanToggle;
    [SerializeField] private Toggle englishToggle;

    /// <summary>
    /// Language 언어 설정
    /// </summary>    
    public void Set(string currentLanguage)
    {
        switch (currentLanguage)
        {
            case "ko":
                this.koreanToggle.isOn = true;
                break;
            case "en":
                this.englishToggle.isOn = true;
                break;
        }

        this.koreanToggle.onValueChanged.AddListener(this.ChangeKorean);
        this.englishToggle.onValueChanged.AddListener(this.ChangeEnglish);
    }

    private void ChangeKorean(bool isOn)
    {
        if (isOn)
        {
            UISFX.Instance.Play(UISFX.Instance.buttonClick);
            DataFrame.Instance.dLanguage = LocaleNames.Korean;
        }
    }

    private void ChangeEnglish(bool isOn)
    {
        if (isOn)
        {
            UISFX.Instance.Play(UISFX.Instance.buttonClick);
            DataFrame.Instance.dLanguage = LocaleNames.English;
        }
    }
}
