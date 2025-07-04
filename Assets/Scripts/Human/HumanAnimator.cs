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
            {
                switch (_state)
                {
                    case HumanStates.Walking:
                        _animator.SetTrigger("Walk");
                        break;
                    case HumanStates.Standing:
                        _animator.SetTrigger("Stop");
                        break;
                }
            }
        }
    }
}
