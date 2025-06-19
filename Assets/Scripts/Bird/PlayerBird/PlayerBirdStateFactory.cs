using ModestTree;
using UnityEngine;

namespace PigeonMail
{
    public enum PlayerBirdStates
    {
        Falling = -1,
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
        private PlayerBirdStateFall.Factory _fallFactory;

        public PlayerBirdStateFactory(PlayerBirdStateIdle.Factory idleFactory,
        PlayerBirdStateWalk.Factory walkFactory, PlayerBirdStateTakeOff.Factory takeoffFactory,
         PlayerBirdStateFly.Factory flyFactory, PlayerBirdStateSoar.Factory soarFactory,
         PlayerBirdStateFall.Factory fallFactory)
        {
            _idleFactory = idleFactory;
            _walkFactory = walkFactory;
            _takeoffFactory = takeoffFactory;
            _flyFactory = flyFactory;
            _soarFactory = soarFactory;
            _fallFactory = fallFactory;
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
                case PlayerBirdStates.Falling:
                    return _fallFactory.Create();
                default:
                    throw Assert.CreateException();
            }
        }
    }
}