using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

namespace PigeonMail
{
    public class CityStateTracker : MonoBehaviour
    {
        [SerializeField]
        private CityColor _cityColor;
        private List<Image> _healthBar = new();
        private List<Image> _lettersBar = new();
        private int _health, _letters;
        private Settings _settings;
        private SignalBus _signalBus;

        public CityColor FlagColor
        {
            get
            {
                return _cityColor;
            }
        }

        public int Health
        {
            get
            {
                return _health;
            }
        }

        public int Letters
        {
            get
            {
                return _letters;
            }
        }

        private void Awake()
        {
            Transform healthImages =  transform.GetChild(0);
            Transform lettersImages = transform.GetChild(1);

            for (int i = 0; i < _settings.health; i++)
                _healthBar.Add(Instantiate(_settings.healthIconPrefab, healthImages).GetComponent<Image>());

            for (int i = 0; i < _settings.mailCapacity; i++)
            {
                _lettersBar.Add(Instantiate(_settings.letterIconPrefab, lettersImages).GetComponent<Image>());
                _lettersBar[i].color = _settings.noneColor;
            }

            _health = _settings.health;
        }

        [Inject]
        public void Construct(Settings settings, SignalBus signalBus)
        {
            _settings = settings;
            _signalBus = signalBus;
            _signalBus.Subscribe<TrackableLetterStatusChangedSignal>(OnTrackableLetterStatusChanged);
        }

        private void OnTrackableLetterStatusChanged(TrackableLetterStatusChangedSignal signal)
        {
            if (signal.letter.To.GetComponent<Clerk>().CityFlagColor != _cityColor)
                return;

            if (signal.letter.Status == LetterStatus.Delivered)
            {
                _letters++;
                ProgressMail();
            }
            else if (signal.letter.Status == LetterStatus.Lost)
            {
                _health--;
                ProgressHealth();
            }
        }

        private void ProgressHealth()
        {
            _healthBar[_health].color = _settings.noneColor;
            _signalBus.Fire(new CityDamageSignal() { cityTracker = this });

            if (_health == 1)
                _health = 1;
        }

        private void ProgressMail()
        {
            _lettersBar[_letters - 1].color = Color.white;
            _signalBus.Fire(new CityReceivedLetterSignal() { cityTracker = this });

            if (_letters >= _settings.mailCapacity)
                _letters = 1;
        }

        [Serializable]
        public class Settings
        {
            public int health, mailCapacity;
            public GameObject healthIconPrefab, letterIconPrefab;
            public Color noneColor;
            public int healthOnFire;
        }
    }
}