using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class LetterTimer : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _timer;
        private float _remainingTime;
        private Settings _settings;
        private SignalBus _signalBus;
        private float _initialTime;
        private ProjectSettingsInstaller.SavedPrefsNames _prefsNames;

        private void Awake() => _initialTime = PlayerPrefs.GetFloat(_prefsNames.timer, _settings.initialDeliveryTime);

        [Inject]
        public void Construct(SignalBus signalBus, Settings settings, ProjectSettingsInstaller.SavedPrefsNames prefsNames)
        {
            _settings = settings;
            _signalBus = signalBus;
            _prefsNames = prefsNames;
        }

        private void OnEnable() => StartCoroutine(Tick());

        private void OnDisable()
        {
            StopAllCoroutines();
            _initialTime -= _settings.initialDeliveryTimeDelta;

            if (_initialTime < _settings.initialDeliveryTimeMin)
                _initialTime = _settings.initialDeliveryTimeMin;

            PlayerPrefs.SetFloat(_prefsNames.timer, _initialTime);
        }

        private IEnumerator Tick()
        {
            _timer.color = Color.black;
            _remainingTime = _initialTime;

            while (_remainingTime > 0f)
            {
                SetText();
                yield return new WaitForSeconds(1f);
                _remainingTime -= 1f;
            }

            _signalBus.Fire<OutOfTimeSignal>();
        }

        private void SetText()
        {
            if (_remainingTime < _settings.voltageTime)
                _timer.color = _settings.voltageColor;

            int minutes = (int)(_remainingTime / 60f);
            int seconds = (int)_remainingTime - minutes * 60;
            _timer.text = $"{minutes}:" + seconds.ToString("D2");
        }

        [Serializable]
        public class Settings
        {
            public float initialDeliveryTime, initialDeliveryTimeDelta,
                initialDeliveryTimeMin;
            public float voltageTime;
            public Color voltageColor;
        }
    }
}