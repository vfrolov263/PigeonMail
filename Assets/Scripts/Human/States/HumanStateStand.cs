using Cysharp.Threading.Tasks;
using PigeonMail;
using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class HumanStateStand : HumanState
    {
        public HumanStateStand(Human human, HumanStateManager stateManager) : base(human, stateManager)
        {
        }

        public async override void Start()
        {
            var currentPoint = _human.CurrentWaypoint;

            if (currentPoint == null)
                return;

            await UniTask.WaitForSeconds(currentPoint.WaitTime);
            _human.NextWaypoint();
            _stateManager.ChangeState(HumanStates.Walking);
        }
    }
}