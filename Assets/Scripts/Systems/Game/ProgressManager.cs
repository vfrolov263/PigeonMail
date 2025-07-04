using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class ProgressManager : MonoBehaviour
    {
        [SerializeField]
        private List<CityFire> _cityFires;
        [SerializeField]
        private GameObject _cannonballs;
        [SerializeField]
        private GameObject _goodEndPrefab, _badEndPrefab;
        [SerializeField]
        private List<GameObject> _destroyOnLoadProgressList, _enableOnLoadProgressList;
        [SerializeField]
        private List<GameObject> _disableOnEndGameList;
        [SerializeField]
        private List<CityStateTracker> _citiesTrackers;
        [SerializeField]
        private GameObject _winMessage, _defeatMessage;
        private CityStateTracker.Settings _settings;
        private Dictionary<CityColor, bool> _cityFilled = new();
        private Dictionary<CityColor, string> _cityPrefsKeys = new();
        private bool _loading = false;
        private ProjectSettingsInstaller.SavedPrefsNames _savedPrefsNames;

        
        [Inject]
        public void Construct(CityStateTracker.Settings settings, 
            ProjectSettingsInstaller.SavedPrefsNames savedPrefsNames, SignalBus signalBus)
        {
            _settings = settings;
            signalBus.Subscribe<CityDamageSignal>(OnCityDamage);
            signalBus.Subscribe<CityReceivedLetterSignal>(OnCityReceivedLetter);
            signalBus.Subscribe<OutOfTimeSignal>(OnOutOfTime);
            _cityPrefsKeys.Add(CityColor.Red, savedPrefsNames.redCity);
            _cityPrefsKeys.Add(CityColor.Yellow, savedPrefsNames.yellowCity);
            _cityPrefsKeys.Add(CityColor.Blue, savedPrefsNames.blueCity);
            _savedPrefsNames = savedPrefsNames;
        }

        private void Awake()
        {
            if (_destroyOnLoadProgressList.Count == 0)
                return;

            foreach (var i in _cityPrefsKeys)
            {
                if (PlayerPrefs.HasKey(i.Value))
                {
                    foreach (var gameObject in _destroyOnLoadProgressList)
                        Destroy(gameObject);

                    foreach (var gameObject in _enableOnLoadProgressList)
                        gameObject.SetActive(true);

                    _loading = true;
                    break;
                }
            }
        }

        private void Start()
        {
            foreach (var i in _cityFires)
                _cityFilled.Add(i.color, false);

            if (_loading)
                LoadProgress();
        }

        public void OnCityDamage(CityDamageSignal signal)
        {
            // Debug.Log("Damage for city");
            if (signal.cityTracker.Health <= _settings.healthOnFire)
                FireCity(signal.cityTracker.FlagColor);

            
            if (signal.cityTracker.Health <= 0)
            {
                _defeatMessage.SetActive(true);
                Invoke("GameOver", 2f);
            }
            else
                SaveProgress(signal.cityTracker);
        }

        private void SaveProgress(CityStateTracker cityTracker)
        {
            Debug.Log("Saving progress");

            PlayerPrefs.SetInt(_cityPrefsKeys[cityTracker.FlagColor],
                cityTracker.Health * 10 + cityTracker.Letters);
        }

        private void LoadProgress()
        {
            Debug.Log("Loading progress");

            foreach (var city in _citiesTrackers)
            {
                if (PlayerPrefs.HasKey(_cityPrefsKeys[city.FlagColor]))
                {
                    int stats = PlayerPrefs.GetInt(_cityPrefsKeys[city.FlagColor]);
                    int health = stats / 10;
                    int letters = stats % 10;

                    // if (city.FlagColor != CityColor.Yellow)
                    //     letters = 1;
                    city.Initialise(health, letters);

                    if (health <= _settings.healthOnFire)
                        FireCity(city.FlagColor);

                    if (letters >= _settings.mailCapacity)
                        _cityFilled[city.FlagColor] = true;
                }
            }

            if (PlayerPrefs.HasKey(_savedPrefsNames.cannonballs))
                _cannonballs.SetActive(true);
        }

        private void ClearProgress()
        {
            PlayerPrefs.DeleteKey(_cityPrefsKeys[CityColor.Red]);
            PlayerPrefs.DeleteKey(_cityPrefsKeys[CityColor.Yellow]);
            PlayerPrefs.DeleteKey(_cityPrefsKeys[CityColor.Blue]);
            PlayerPrefs.DeleteKey(_savedPrefsNames.cannonballs);
        }

        private void GameOver()
        {
            EndGame();
            Instantiate(_badEndPrefab);
        }

        private void EndGame()
        {
            ClearProgress();

            foreach (var i in _disableOnEndGameList)
                i.SetActive(false);
        }

        public void OnOutOfTime()
        {
            _cannonballs.SetActive(true);
            PlayerPrefs.SetInt(_savedPrefsNames.cannonballs, 1);
        }

        private void FireCity(CityColor cityColor)
        {
            foreach (var i in _cityFires)
            {
                if (i.color == cityColor)
                {
                    i.fire.SetActive(true);
                    break;
                }
            }
        }

        public void OnCityReceivedLetter(CityReceivedLetterSignal signal)
        {
            if (_cannonballs.activeSelf)
            {
                _cannonballs.SetActive(false);
                PlayerPrefs.DeleteKey(_savedPrefsNames.cannonballs);
            }

            _cityFilled[signal.cityTracker.FlagColor] = signal.cityTracker.Letters == _settings.mailCapacity;

            //  foreach (var i in _cityFilled)
            //     Debug.Log($"City {i.Key} is {i.Value}");
            CheckForWin(signal.cityTracker);
        }

        private void CheckForWin(CityStateTracker cityTracker)
        {
            bool win = true;

            foreach (var i in _cityFilled)
                win &= i.Value;

            if (win)
            {
                _winMessage.SetActive(true);
                Invoke("Win", 2f);
            }
            else
                SaveProgress(cityTracker);
        }

        private void Win()
        {
            EndGame();
            Instantiate(_goodEndPrefab);
        }

        [Serializable]
        public struct CityFire
        {
            public GameObject fire;
            public CityColor color;
        }
    }
}