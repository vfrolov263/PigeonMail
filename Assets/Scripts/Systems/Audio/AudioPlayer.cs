using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class AudioPlayer : MonoBehaviour
    {
        Settings _settings;
        [SerializeField]
        AudioSource _newLetterSound, _interactionWithScribeSound, _outOfTimeSound, _cityDamageSound;
        [SerializeField]
        AudioSource _playerBirdTakeoffSound, _playerBirdFlapsSound;
        [SerializeField]
        AudioSource _natureSound;

        [Inject]
        public void Construct(SignalBus signalBus, Settings settings)
        {
            signalBus.Subscribe<TrackableLetterStatusChangedSignal>(OnTrackableLetterStatusChanged);
            signalBus.Subscribe<CityDamageSignal>(OnCityDamage);
            _settings = settings;
        }

        private void Awake()
        {
            _newLetterSound.volume = _settings.volume;
            _interactionWithScribeSound.volume = _settings.volume;
            _outOfTimeSound.volume = _settings.volume;
            _cityDamageSound.volume = _settings.volume;
            _playerBirdTakeoffSound.volume = _settings.volume;
            _natureSound.volume = _settings.volume;
            // _playerBirdSoar.volume = _settings.volume;
            _playerBirdFlapsSound.volume = _settings.volume;
        }

        private void OnTrackableLetterStatusChanged(TrackableLetterStatusChangedSignal signal)
        {
            switch (signal.letter.Status)
            {
                case LetterStatus.Ready:
                    _newLetterSound.Play();
                    break;
                case LetterStatus.Delivering:
                case LetterStatus.Delivered:
                    _interactionWithScribeSound.Play();
                    break;
                case LetterStatus.Lost:
                    _outOfTimeSound.Play();
                    break;
            }
        }

        public void BirdPlay(PlayerBirdStates state)
        {
            switch (state)
            {
                case PlayerBirdStates.TakingOff:
                    _playerBirdTakeoffSound.Play();
                    break;
                case PlayerBirdStates.Flying:
                    _playerBirdFlapsSound.Play();
                    break;
            }
            //_oneShotSound.PlayOneShot(clip);
        }

        public void BirdStop()
        {
            _playerBirdFlapsSound.Stop();
        }

        public bool BackgroundPlaying
        {
            get
            {
                return _natureSound.volume > 0;
            }
            set
            {
                _natureSound.volume = value ? _settings.volume : 0;
            }
        }

        private void OnCityDamage()
        {
            _cityDamageSound.Play();
        }

        [Serializable]
        public class Settings
        {
            [Range(0f, 1f)]
            public float volume;
            public float decayRate;
        }
    }
}