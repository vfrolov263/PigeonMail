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
            Vector3 motion = _playerBird.transform.forward * _settings.walkSpeed;
            motion.y = -1f;
            _playerBird.Move(motion * Time.deltaTime);
        }

        //public void 

        public class Factory : PlaceholderFactory<PlayerBirdStateWalk>
        {
        }
    }
}