using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;


public class Localisation : MonoBehaviour
{
    public TextMeshProUGUI output;

    public void HandleInput(int val)
    {
        string localeCode="en";
        if (val == 0)
        {
            localeCode="en";
        }
        if (val == 1)
        {
            localeCode = "hi";
        }
        if (val == 2)
        {
            localeCode = "vi";
        }
        if (val == 3)
        {
            localeCode = "th";
        }
        if (val == 4)
        {
            localeCode = "id";
        }
        PlayerPrefs.SetString("SelectedLocale", localeCode);
        SetLanguage(localeCode);
    }

    void SetLanguage(string localeCode)
    {
        // Set the selected locale
        var locale = LocalizationSettings.AvailableLocales.GetLocale(localeCode);
        if (locale != null)
        {
            LocalizationSettings.SelectedLocale = locale;
            Debug.Log("Language changed to: " + localeCode);
        }
        else
        {
            Debug.LogError("Locale not found: " + localeCode);
        }
    }

}
