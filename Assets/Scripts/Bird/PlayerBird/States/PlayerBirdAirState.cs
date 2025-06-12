using System;
using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public abstract class PlayerBirdAirState : PlayerBirdState
    {
        protected readonly Settings _settings;

        public PlayerBirdAirState(Settings settings)
        {
            _settings = settings;
        }

        public override void Start()
        {
            base.Start();
            _rotation.x = BringAnlge(_rotation.x);
            _rotation.z = BringAnlge(_rotation.z);

            if (_playerBird.Speed < _settings.minSpeed)
                _playerBird.Speed = _settings.minSpeed;
        }

        private float BringAnlge(float angle)
        {
            angle %= 360f;
            angle -= 360f * (float)Math.Truncate(angle / 180f);
            return angle;
        }

        public override void Update()
        {
            CalculateMotion();
            CheckLanding();
        }

        private void CalculateMotion()
        {
            _rotation.x -= _input.PointerDelta.y * _settings.rotationSpeed.x * Time.deltaTime;
            _rotation.x = Math.Clamp(_rotation.x, _settings.minAngleX, _settings.maxAngleX);

            _rotation.z -= _input.PointerDelta.x * _settings.rotationSpeed.z * Time.deltaTime;
            _rotation.z = Math.Clamp(_rotation.z, -_settings.maxAngleZ, _settings.maxAngleZ);

            var oldRotation = _playerBird.transform.localEulerAngles;
            oldRotation.x = _rotation.x;
            oldRotation.z = _rotation.z;
            _playerBird.transform.localEulerAngles = oldRotation;

            _rotation.y = -(_rotation.z / _settings.maxAngleZ) * _settings.rotationSpeed.y * Time.deltaTime;
            _playerBird.transform.Rotate(new(0f, _rotation.y, 0f), Space.World);

            Vector3 motion = _playerBird.transform.forward * _playerBird.Speed;
            _playerBird.Move(motion * Time.deltaTime);
        }

        private void CheckLanding()
        {
            if (_playerBird.IsGrounded)
                _playerBird.ChangeState(PlayerBirdStates.Idle);
        }

        [Serializable]
        public class Settings
        {
            public float maxSpeed, minSpeed;
            public float accelaration, deceleration, activeDeceleration;
            public Vector3 rotationSpeed;
            public float minAngleX, maxAngleX, maxAngleZ;
        }
    }
}