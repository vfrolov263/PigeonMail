using PigeonMail;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PigeonMail
{
    public class SensetivityHandle : MonoBehaviour
    {
        private IInput.Settings _settings;
        private Slider _sensitivitySlider;
        private ProjectSettingsInstaller.SavedPrefsNames _prefsNames;

        [Inject]
        public void Construct(IInput.Settings settings, ProjectSettingsInstaller.SavedPrefsNames prefsNames)
        {
            _settings = settings;
            _prefsNames = prefsNames;
        }

        private void Awake()
        {
            _sensitivitySlider = GetComponent<Slider>();
            _sensitivitySlider.value = _settings.sensitivity = PlayerPrefs.GetFloat(_prefsNames.sensitivity, .5f);
            _sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
        }

        private void OnSensitivityChanged(float value)
        {
            PlayerPrefs.SetFloat(_prefsNames.sensitivity, value);
            _settings.sensitivity = value;
        }
    }
}