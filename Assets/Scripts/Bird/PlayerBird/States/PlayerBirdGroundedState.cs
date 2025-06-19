using System;
using UnityEngine;

namespace PigeonMail
{
    public abstract class PlayerBirdGroundedState : PlayerBirdState
    {
        protected readonly Settings _settings;

        public PlayerBirdGroundedState(Settings settings)
        {
            _settings = settings;
        }

        public override void Start()
        {
            base.Start();
            _playerBird.Speed = 0f;
            _input.JumpActions += TakeOff;
        }

        public override void Update()
        {
            _rotation.y += _input.PointerDelta.x * _settings.rotationSpeed * Time.deltaTime;
            _playerBird.transform.rotation = Quaternion.Euler(0f, _rotation.y, 0f);
            _playerBird.Move(new(0f, -_settings.gravityForce * Time.deltaTime, 0f));

            if (!_playerBird.IsGrounded)
                TakeOff();
        }

        public override void Dispose()
        {
            _input.JumpActions -= TakeOff;
        }

        public virtual void TakeOff()
        {
             _playerBird.ChangeState(PlayerBirdStates.TakingOff);
        }

        [Serializable]
        public class Settings
        {
            public float rotationSpeed;
            public float walkSpeed;
            public float gravityForce;
        }
    }
}