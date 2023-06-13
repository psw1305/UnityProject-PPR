using UnityEngine;

public partial class DataFrame
{
    public float dBGM
    {
        get
        {
            return DataSystem.Instance.GetFloat(DataKey.Settings.BGM, 1.0f);
        }
        set
        {
            DataSystem.Instance.SetValue(DataKey.Settings.BGM, value);
            AudioBGM.Instance.VolumeBGMScale = value;
        }
    }

    public float dSFX
    {
        get
        {
            return DataSystem.Instance.GetFloat(DataKey.Settings.SFX, 1.0f);
        }
        set
        {
            DataSystem.Instance.SetValue(DataKey.Settings.SFX, value);
            AudioSFX.Instance.VolumeSFXScale = value;
        }
    }

    public string dLanguage
    {
        get
        {
            return DataSystem.Instance.GetString(DataKey.Settings.LANGUAGE, "ko");
        }
        set
        {
            DataSystem.Instance.SetValue(DataKey.Settings.LANGUAGE, value);
            LocaleLanguage.LoadLocale(value);
        }
    }

    public int dFPS
    {
        get
        {
            return DataSystem.Instance.GetInt(DataKey.Settings.FPS, 60);
        }
        set
        {
            DataSystem.Instance.SetValue(DataKey.Settings.FPS, value);
            Application.targetFrameRate = value;
        }
    }
}
