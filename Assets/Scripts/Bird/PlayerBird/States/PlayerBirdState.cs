using System;
using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public abstract class PlayerBirdState : IDisposable
    {
        protected IInput _input;
        protected PlayerBird _playerBird;
        protected Vector3 _rotation;

        [Inject]
        protected virtual void Construct(IInput input, PlayerBird playerBird)
        {
            _input = input;
            _playerBird = playerBird;
        }

        public virtual void Start() 
        {
            _rotation = _playerBird.transform.localEulerAngles;
            //_rotation = _playerBird.AngleSettings;
            //_rotation.x %= 360f;// * (_rotation.x % 360f);
            //Debug.Log($"{_playerBird.State}, Rotation: {_rotation}");
        }

        public abstract void Update();

        public virtual void OnTriggerEnter(Collider other) {}

        public virtual void Dispose() {}
    }
}
