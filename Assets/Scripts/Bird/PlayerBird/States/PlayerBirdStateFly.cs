using System;
using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class PlayerBirdStateFly : PlayerBirdAirState
    {
        private float _acc;

        public PlayerBirdStateFly(Settings settings) : base(settings)
        {
        }

        public override void Start()
        {
            base.Start();
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
        }

        public class Factory : PlaceholderFactory<PlayerBirdStateFly>
        {
        }
    }
}