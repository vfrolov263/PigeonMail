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
            //Debug.Log($"Status: {signal.letter.Status}, ColorTo: {signal.letter.To.GetComponent<Clerk>().CityFlagColor}, ColorCity: {_cityColor}");
            if (signal.letter.Status != LetterStatus.Delivered || signal.letter.To.GetComponent<Clerk>().CityFlagColor != _cityColor)
                return;

           // if (signal.letter.Status == LetterStatus.Delivered)
          //  {
                _letters++;
                ProgressMail();
           // }
            // else if (signal.letter.Status == LetterStatus.Lost)
            // {
            //     _health--;
            //     ProgressHealth();
            // }
        }

        public void Hit()
        {
            _health--;
            ProgressHealth();
        }

        private void ProgressHealth()
        {
            if (_health < 0)
                return;

            _healthBar[_health].color = _settings.noneColor;
            _signalBus.Fire(new CityDamageSignal() { cityTracker = this });
        }

        private void ProgressMail()
        {
            if (_letters > _settings.mailCapacity)
                return;
                
            _lettersBar[_letters - 1].color = Color.white;
            _signalBus.Fire(new CityReceivedLetterSignal() { cityTracker = this });
        }

        public void Initialise(int health, int letters)
        {
            _health = health;
            _letters = letters;
            InitialiseHelth();
            InitialiseMail();
        }

        private void InitialiseHelth()
        {
            for (int i = _health; i < _settings.health; i++)
                 _healthBar[i].color = _settings.noneColor;
        }

        private void InitialiseMail()
        {
            for (int i = 0; i < _letters; i++)
                 _lettersBar[i].color = Color.white;
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