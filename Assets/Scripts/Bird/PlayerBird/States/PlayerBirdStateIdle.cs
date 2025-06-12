using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class PlayerBirdStateIdle : PlayerBirdGroundedState
    {
        //private float _rotationY;
        // private IInput _input;
        // private PlayerBird _playerBird;

        // [Inject]
        // private void Construct(IInput input, PlayerBird playerBird)
        // {
        //     _input = input;
        //     _playerBird = playerBird;
        // }
        public PlayerBirdStateIdle(Settings settings) : base(settings)
        {
        }

        public override void Start()
        {
            //_rotationY = _playerBird.Rotation.eulerAngles.y;
            base.Start();
            _playerBird.transform.rotation = Quaternion.Euler(0f, _rotation.y, 0f);
        }

        public override void Update()
        {
            if (_input.Axes.y != 0f)
                _playerBird.ChangeState(PlayerBirdStates.Walking);

            base.Update();
        }

        // public override void Dispose()
        // {
        //     base.
        // }

        // public void TakeOff()
        // {
        //     _playerBird.ChangeState(PlayerBirdStates.Flying);
        // }

        public class Factory : PlaceholderFactory<PlayerBirdStateIdle>
        {
        }
    }
}
