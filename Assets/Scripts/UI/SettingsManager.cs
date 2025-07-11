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
    private TMP_Dropdown _languageDropdown, _resolutionsDropdown;
    [SerializeField]
    private Slider _sensitivitySlider, _volumeSlider;
    [SerializeField]
    private Toggle _fullscreenToggle, _inversionToggle;
    private IInput.Settings _inputSettings;
    private AudioPlayer.Settings _audioSettings;
    private ProjectSettingsInstaller.SavedPrefsNames _prefsNames;
    
    [Inject]
    public void Construct(IInput.Settings inputSettings, AudioPlayer.Settings audioSettings, 
        ProjectSettingsInstaller.SavedPrefsNames prefsNames)
    {
        _inputSettings = inputSettings;
        _audioSettings = audioSettings;
        _prefsNames = prefsNames;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        InitLanguagesDropdown();
        InitResolutionsDropdown();
        InitScreenModeToggle();
        _inversionToggle.isOn = _inputSettings.verticalInversion = PlayerPrefs.GetInt(_prefsNames.inversion, 0) > 0;
        _inversionToggle.onValueChanged.AddListener(OnInversionChanged);
        _sensitivitySlider.value = _inputSettings.sensitivity = PlayerPrefs.GetFloat(_prefsNames.sensitivity, .6f);
        _sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
        _volumeSlider.value = _audioSettings.volume = PlayerPrefs.GetFloat(_prefsNames.volume, .7f);
        _volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    private void InitLanguagesDropdown()
    {
        _languageDropdown.ClearOptions();
        List<string> languages = new();
        int choosenLanguage = 0;

        foreach (var i in LocalizationSettings.AvailableLocales.Locales)
        {
            if (i == LocalizationSettings.SelectedLocale)
                choosenLanguage = languages.Count;

            languages.Add(i.Identifier.CultureInfo != null ? i.Identifier.CultureInfo.NativeName : i.name);
        }

        _languageDropdown.AddOptions(languages);
        _languageDropdown.SetValueWithoutNotify(choosenLanguage);
        _languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
    }

    private void InitResolutionsDropdown()
    {
        _resolutionsDropdown.ClearOptions();
        List<string> resolutions = new();
        int choosenResolution = 0;

        var startResolution = PlayerPrefs.GetString(_prefsNames.resolution, "");

        if (startResolution != "")
            SetResolution(startResolution);

        foreach (var i in Screen.resolutions)
        {
            if (i.width == Screen.currentResolution.width && i.height == Screen.currentResolution.height)
                choosenResolution = resolutions.Count;

            resolutions.Add($"{i.width}x{i.height}");
        }

        _resolutionsDropdown.AddOptions(resolutions);
        _resolutionsDropdown.SetValueWithoutNotify(choosenResolution);
        _resolutionsDropdown.onValueChanged.AddListener(OnResolutionChanged);
    }

    private void InitScreenModeToggle()
    {
        _fullscreenToggle.isOn = PlayerPrefs.GetInt(_prefsNames.fullscreen, 1) > 0;

        if (Screen.fullScreen != _fullscreenToggle.isOn)
            Screen.fullScreen = _fullscreenToggle.isOn;

        _fullscreenToggle.onValueChanged.AddListener(OnScreenModeChanged);
    }

    private void SetResolution(string resolution)
    {
        var size = resolution.Split('x');
        int width =  int.Parse(size[0]);
        int height = int.Parse(size[1]);

        if (Screen.currentResolution.width != width || Screen.currentResolution.height != height)    
            Screen.SetResolution(width, height, Screen.fullScreen);
    }

    private void OnLanguageChanged(int languageId)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageId];
        PlayerPrefs.SetString("selected-locale", LocalizationSettings.SelectedLocale.Identifier.Code);
    }

    private void OnResolutionChanged(int resolutionId)
    {
        var newResolution = Screen.resolutions[resolutionId];
        Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreen);
        PlayerPrefs.SetString(_prefsNames.resolution, $"{newResolution.width}x{newResolution.height}");
    }

    private void OnScreenModeChanged(bool isOn)
    {
        Screen.fullScreen = isOn;
        PlayerPrefs.SetInt(_prefsNames.fullscreen, Convert.ToInt32(isOn));
    }

    private void OnVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat(_prefsNames.volume, value);
        _audioSettings.volume = value;
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