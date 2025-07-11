using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class CannoballsDamager : MonoBehaviour
    {
        [SerializeField]
        private List<CityStateTracker> _cities;
        private Settings _settings;
        private AudioPlayer _audioPlayer;

        [Inject]
        public void Construct(Settings settings, AudioPlayer.Settings audioSettings, AudioPlayer audioPlayer)
        {
            _settings = settings;
            _audioPlayer = audioPlayer;
            GetComponent<AudioSource>().volume = audioSettings.volume;
        }

        private void OnEnable()
        {
            _audioPlayer.BackgroundPlaying = false;
            StartCoroutine(HitCity());
        }

        private void OnDisable()
        {
            if (_audioPlayer != null)
                _audioPlayer.BackgroundPlaying = true;
                
            StopAllCoroutines();
        }

        // Update is called once per frame
        private IEnumerator HitCity()
        {
            while (true)
            {
                yield return new WaitForSeconds(_settings.timeBetweenHits);
                int i = UnityEngine.Random.Range(0, _cities.Count);
                _cities[i].Hit();
            }
        }

        [Serializable]
        public class Settings
        {
            public float timeBetweenHits;
        }
    }
}