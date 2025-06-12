using ModestTree;
using UnityEngine;

namespace PigeonMail
{
    public enum PlayerBirdStates
    {
        Idle,
        Walking,
        TakingOff,
        Flying,
        Soaring,
        Landing        
    }

    public class PlayerBirdStateFactory
    {
        private PlayerBirdStateIdle.Factory _idleFactory;
        private PlayerBirdStateWalk.Factory _walkFactory;
        private PlayerBirdStateTakeOff.Factory _takeoffFactory;
        private PlayerBirdStateFly.Factory _flyFactory;
        private PlayerBirdStateSoar.Factory _soarFactory;

        public PlayerBirdStateFactory(PlayerBirdStateIdle.Factory idleFactory,
        PlayerBirdStateWalk.Factory walkFactory, PlayerBirdStateTakeOff.Factory takeoffFactory,
         PlayerBirdStateFly.Factory flyFactory, PlayerBirdStateSoar.Factory soarFactory)
        {
            _idleFactory = idleFactory;
            _walkFactory = walkFactory;
            _takeoffFactory = takeoffFactory;
            _flyFactory = flyFactory;
            _soarFactory = soarFactory;
        }

        public PlayerBirdState ChangeState(PlayerBirdStates state)
        {
            switch (state)
            {
                case PlayerBirdStates.Idle:
                    return _idleFactory.Create();
                case PlayerBirdStates.Walking:
                    return _walkFactory.Create();  
                case PlayerBirdStates.TakingOff:
                    return _takeoffFactory.Create();
                case PlayerBirdStates.Flying:
                    return _flyFactory.Create();
                case PlayerBirdStates.Soaring:
                    return _soarFactory.Create();
                default:
                    throw Assert.CreateException();
            }
        }
    }
}