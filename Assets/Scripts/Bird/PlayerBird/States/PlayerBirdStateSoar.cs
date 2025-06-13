using System;
using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class PlayerBirdStateSoar : PlayerBirdAirState
    {
        private float _deceleration;

        public PlayerBirdStateSoar(Settings settings) : base(settings)
        {
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

        public class Factory : PlaceholderFactory<PlayerBirdStateSoar>
        {
        }
    }
}
