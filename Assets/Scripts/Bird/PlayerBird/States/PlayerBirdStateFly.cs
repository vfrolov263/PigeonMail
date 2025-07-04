using System;
using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class PlayerBirdStateFly : PlayerBirdAirState
    {
        private float _acc;
        private AudioSource _flyAudioSource;

        public PlayerBirdStateFly(Settings settings, BombHandler bombHandler) : base(settings, bombHandler)
        {
        }

        public override void Start()
        {
            base.Start();
            _audioPlayer.BirdPlay(PlayerBirdStates.Flying);
            //_flyAudioSource = _audioPlayer.Play(_settings.flySound);
        }
        
        public override void Update()
        {
            CalculateMotion();
            base.Update();
        }

        private void CalculateMotion()
        {
            if (_input.Axes.y <= 0f)
                _playerBird.ChangeState(PlayerBirdStates.Soaring);

            _acc = _input.Axes.y * _settings.accelaration;

            if (_playerBird.Speed < _settings.maxSpeed)
                _playerBird.Speed += _acc * Time.deltaTime;
        }

        public override void Dispose()
        {
            base.Dispose();
            _audioPlayer.BirdStop();
            //_audioPlayer.Stop(_flyAudioSource);
        }

        public class Factory : PlaceholderFactory<PlayerBirdStateFly>
        {
        }
    }
}