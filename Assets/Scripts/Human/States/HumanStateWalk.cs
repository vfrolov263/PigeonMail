using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class HumanStateWalk : HumanState
    {
        private Vector3 target;

        public HumanStateWalk(Human human, HumanStateManager stateManager) : base(human, stateManager)
        {
        }

        public override async void Start()
        {
            var waypoint = _human.CurrentWaypoint;

            if (waypoint == null)
                return;

            await _human.MoveAsync(waypoint.transform.position);
            _stateManager.ChangeState(HumanStates.Standing);
        }
    }
}