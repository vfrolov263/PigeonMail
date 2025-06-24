using System;
using System.Collections.Generic;
using System.Linq;
using PigeonMail;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using Zenject;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown _languageDropdown;
    [SerializeField]
    private Slider _sensitivitySlider, _volumeSlider;
    [SerializeField]
    private Toggle _inversionToggle;
    private IInput.Settings _inputSettings;
    private ProjectSettingsInstaller.SavedPrefsNames _prefsNames;
    
    [Inject]
    public void Construct(IInput.Settings settings, ProjectSettingsInstaller.SavedPrefsNames prefsNames)
    {
        _inputSettings = settings;
        _prefsNames = prefsNames;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _languageDropdown.ClearOptions();
        List<string> languages = new();
        int choosenLanguage = 0;

        foreach (var i in LocalizationSettings.AvailableLocales.Locales)
        {
            languages.Add(i.Identifier.CultureInfo != null ? i.Identifier.CultureInfo.NativeName : i.name);

            if (i == LocalizationSettings.SelectedLocale)
                choosenLanguage = languages.Count - 1;
        }

        _languageDropdown.AddOptions(languages);
        _languageDropdown.SetValueWithoutNotify(choosenLanguage);
        _languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
        _inversionToggle.isOn = _inputSettings.verticalInversion = PlayerPrefs.GetInt(_prefsNames.inversion, 0) > 0;
        _inversionToggle.onValueChanged.AddListener(OnInversionChanged);
        _sensitivitySlider.value = _inputSettings.sensitivity = PlayerPrefs.GetFloat(_prefsNames.sensitivity, .5f);
        _sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
        _volumeSlider.value = PlayerPrefs.GetFloat(_prefsNames.volume, .5f);
        _volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    private void OnLanguageChanged(int languageId)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageId];
        PlayerPrefs.SetString("selected-locale", LocalizationSettings.SelectedLocale.Identifier.Code);
    }

    private void OnVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat(_prefsNames.volume, value);
       // _settings
    }

    private void OnSensitivityChanged(float value)
    {
        PlayerPrefs.SetFloat(_prefsNames.sensitivity, value);
        _inputSettings.sensitivity = value;
    }

    private void OnInversionChanged(bool isOn)
    {
        PlayerPrefs.SetInt(_prefsNames.inversion, Convert.ToInt32(isOn));
        _inputSettings.verticalInversion = isOn;
    }
}
