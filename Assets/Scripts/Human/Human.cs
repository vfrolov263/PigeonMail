using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ModestTree;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace PigeonMail
{
    public class Human : MonoBehaviour, IMovable
    {
        [SerializeField]
        private List<Waypoint> _route;
        private int _currentWaypoint;
        private NavMeshAgent _navMeshAgent;
        private HumanStateManager _stateManager;
        private HumanAnimator _animator;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            Assert.IsNotNull(_navMeshAgent);
            _stateManager = new(this);
            _stateManager.ChangeState(HumanStates.Walking);
            _animator = new(GetComponentInChildren<Animator>());
        }

        public void Move(Vector3 destination)
        {
            _navMeshAgent.SetDestination(destination);

        }

        public async UniTask MoveAsync(Vector3 destination)
        {
            Move(destination);
            
            while (_navMeshAgent != null && (_navMeshAgent.pathPending || _navMeshAgent.remainingDistance > 0.01f))
                await UniTask.Yield();
        }

        public void OnDestroy()
        {
            Destroy(_navMeshAgent);
            _navMeshAgent = null;
            _route.Clear();
        }

        public Waypoint CurrentWaypoint
        {
            get 
            {
                return _route.IsEmpty() ? null : _route[_currentWaypoint];
            }
        }

        public void NextWaypoint()
        {
            _currentWaypoint++;
                
            if (_currentWaypoint >= _route.Count)
                _currentWaypoint = 0;
        }
    }
}
