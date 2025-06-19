using UnityEngine;

namespace PigeonMail
{
    public class HumanAnimator
    {
        private Animator _animator;
        private HumanStates _state;

        public HumanAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void Refresh(HumanStates state)
        {
            if (_state == state)
                return;

            _state = state;

            if (_animator != null)
                _animator.SetInteger("State", (int)_state);
        }
    }
}
