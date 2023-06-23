using System.Collections.Generic;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

/// <summary>
/// Localization 언어 교체
/// </summary>
public static class LocaleLanguage
{
    internal static void LoadLocale(string langID)
    {
        LocaleIdentifier localeCode = new LocaleIdentifier(langID);
        List<Locale> locales = LocalizationSettings.AvailableLocales.Locales;

        for (int i = 0; i < locales.Count; i++)
        {
            if (locales[i].Identifier == localeCode)
            {
                LocalizationSettings.SelectedLocale = locales[i];
            }
        }
    }

    public static void Korean()
    {
        DataFrame.Instance.dLanguage = LocaleNames.Korean;
    }

    public static void English()
    {
        DataFrame.Instance.dLanguage = LocaleNames.English;
    }
}
