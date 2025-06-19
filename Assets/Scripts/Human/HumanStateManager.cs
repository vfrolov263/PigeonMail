using ModestTree;
using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public enum HumanStates
    {
        Standing,
        Walking,
        Give,
        Get
    }

    public class HumanStateManager
    {
        private HumanState _currentState;
        private HumanStateStand _standState;
        private HumanStateWalk _walkState;

        public HumanStateManager(Human human)
        {
            _standState = new(human, this);
            _walkState = new(human, this);
        }

         public void ChangeState(HumanStates state)
        {
            _currentState?.Exit();

            switch (state)
            {
                case HumanStates.Standing:
                    _currentState = _standState;
                    break;
                case HumanStates.Walking:
                    _currentState = _walkState;
                    break;
                default:
                    throw Assert.CreateException();
            }

            _currentState?.Start();
        }
    }
}


    //     HumanStateStand _standFactory;
    //     HumanStateWalk _walkFactory;

    //     [Inject]
    //     public void Construct(HumanStateStand standFactory, HumanStateWalk walkFactory)
    //     {
    //         _standFactory = standFactory;
    //         _walkFactory = walkFactory;
    //     }

    //     public void ChangeState(HumanStates state)
    //     {
    //         switch (state)
    //         {
    //             case HumanStates.Standing:
    //                 _standFactory.Start();
    //                 break;
    //             case HumanStates.Walking:
    //                 _walkFactory.Start();
    //                 break;
    //             default:
    //                 throw Assert.CreateException();
    //         }
    //     }
    // }
