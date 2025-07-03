using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class PlayerBirdStateTakeOff : PlayerBirdState
    {
        private readonly Settings _settings;
        private Vector2 _speed;

        public PlayerBirdStateTakeOff(Settings settings)
        {
            _settings = settings;
        }

        public override void Start()
        {
            ReadyToFly();
            _audioPlayer.PlayShot(_settings.takeoffSound);
        }

        public override void Update()
        {
            Vector3 motion = _playerBird.transform.forward * _speed.x;
            motion.y = _speed.y;
            _playerBird.Move(motion * Time.deltaTime);
        }

        private async void ReadyToFly()
        {
            await UniTask.WaitForSeconds(_settings.moveDelayInSeconds);
            _speed = new Vector2(_settings.speed.x, _settings.speed.y);
            await UniTask.WaitForSeconds(_settings.moveTimeInSeconds);
            _playerBird.ChangeState(PlayerBirdStates.Flying);
        }

        public class Factory : PlaceholderFactory<PlayerBirdStateTakeOff>
        {
        }

        [Serializable]
        public class Settings
        {
            public Vector2 speed;
            public float moveDelayInSeconds;
            public float moveTimeInSeconds;
            public AudioClip takeoffSound;
        }
    }
}
