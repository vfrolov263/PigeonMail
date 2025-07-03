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
        private CityStateTracker.Settings _settings;

        [Inject]
        public void Construct(CityStateTracker.Settings settings, SignalBus signalBus)
        {
            _settings = settings;
            signalBus.Subscribe<CityDamageSignal>(OnCityDamage);
            signalBus.Subscribe<CityReceivedLetterSignal>(OnCityReceivedLetter);
        }

        public void OnCityDamage(CityDamageSignal signal)
        {
            // Debug.Log("Damage for city");
            if (signal.cityTracker.Health <= _settings.healthOnFire)
                FireCity(signal.cityTracker.FlagColor);
           // if (signal.cityTracker.Health <= 0)
// GameOver
        }

        private void FireCity(CityColor cityColor)
        {
            // Debug.Log("Try to fire");
            foreach (var i in _cityFires)
            {
                Debug.Log($"{cityColor} and {i.color}");
                if (i.color == cityColor)
                {
                    Debug.Log("Activate fire");
                    i.fire.SetActive(true);
                    break;
                }
            }
        }

        public void OnCityReceivedLetter(CityReceivedLetterSignal signal)
        {

        }

        [Serializable]
        public struct CityFire
        {
            public GameObject fire;
            public CityColor color;
        }
    }
}