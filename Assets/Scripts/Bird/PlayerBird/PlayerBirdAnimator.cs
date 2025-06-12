using UnityEngine;
using Zenject;


namespace PigeonMail
{
    public class PlayerBirdAnimator
    {
        private Animator _animator;
        private PlayerBirdStates _playerBirdState;

        [Inject]
        private void Construct(PlayerBird playerBird)
        {
            _animator = playerBird.Animator;
        }

        public void Refresh(PlayerBirdStates playerBirdState)
        {
            if (_playerBirdState == playerBirdState)
                return;

            _playerBirdState = playerBirdState;
            _animator.SetInteger("State", (int)_playerBirdState);
        }
    }
}
