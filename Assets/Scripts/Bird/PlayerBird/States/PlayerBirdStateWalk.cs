using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class PlayerBirdStateWalk : PlayerBirdGroundedState
    {
        public PlayerBirdStateWalk(Settings settings) : base(settings)
        {
        }

        public override void Update()
        {
            if (_input.Axes.y == 0f)
                _playerBird.ChangeState(PlayerBirdStates.Idle);

            base.Update();
            Vector3 motion = _input.Axes.y * _playerBird.transform.forward * _settings.walkSpeed;
            _playerBird.Move(motion * Time.deltaTime);
        }

        //public void 

        public class Factory : PlaceholderFactory<PlayerBirdStateWalk>
        {
        }
    }
}