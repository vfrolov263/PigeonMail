using System;
using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class PlayerBirdStateSoar : PlayerBirdAirState
    {
        private float _deceleration;
        //private AudioSource _soarAudioSource;

        public PlayerBirdStateSoar(Settings settings, BombHandler bombHandler) : base(settings, bombHandler)
        {
        }

        public override void Start()
        {
            base.Start();
            //_soarAudioSource = _audioPlayer.Play(_settings.soarSound);
        }

        public override void Update()
        {
            CalculateMotion();
            base.Update();
        }

        private void CalculateMotion()
        {
            if (_input.Axes.y > 0f)
                _playerBird.ChangeState(PlayerBirdStates.Flying);

            _deceleration = _input.Axes.y < 0f ? _settings.activeDeceleration * _input.Axes.y : -_settings.deceleration;

            if (_playerBird.Speed > _settings.minSpeed)
                _playerBird.Speed += _deceleration * Time.deltaTime;

           _playerBird.Move(new(0f, 
           -_settings.gravityForce * (1f - (float)Math.Pow(_playerBird.Speed / _settings.maxSpeed, _settings.gravityFuncPower)) * Time.deltaTime,
            0f));
        }

        // public override void Dispose()
        // {
        //     //_audioPlayer.Stop(_soarAudioSource);
        // }

        public class Factory : PlaceholderFactory<PlayerBirdStateSoar>
        {
        }
    }
}
