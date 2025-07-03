using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class PlayerBird : Bird
    {
        private CharacterController _controller;
        private PlayerBirdStateFactory _stateFactory;
        private PlayerBirdState _state;
        private PlayerBirdStates _stateId;
        private PlayerBirdAnimator _birdAnimator;

        [Inject]
        private void Construct(PlayerBirdStateFactory stateFactory, PlayerBirdAnimator birdAnimator)
        {
            _stateFactory = stateFactory;
            _birdAnimator = birdAnimator;
        }

        public bool IsGrounded
        {
            get 
            {
                return _controller.isGrounded; 
            }
        }

        public float Speed { get; set; }

        public PlayerBirdStates State
        {
            get 
            {
                return _stateId;
            }
        }

        public Animator Animator
        {
            get
            {
                //return TryGetComponent<Animator>(out Animator animator) ? animator : null;
                return GetComponentInChildren<Animator>();
            }
        }

        public Vector3 Velocity
        {
            get
            {
                return _controller.velocity;
            }
        }

        public override void Move(Vector3 motion)
        {
            _controller.Move(motion);
        }

        public void Move(Vector3 motion, out CollisionFlags flags)
        {
            flags = _controller.Move(motion);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _controller = GetComponent<CharacterController>();
            _stateId = PlayerBirdStates.Flying;
            ChangeState(_stateId);
        }

        // Update is called once per frame
        void Update()
        {             
            _state?.Update();

            //Debug.Log($"Speed: {_controller.transform.forward}");
           // Debug.Log($"Velocity: {_controller.velocity}, magnitude: {_controller.velocity.magnitude}");
        }

        public void ChangeState(PlayerBirdStates state)
        {
            _state?.Dispose();
            _state = null;
            _stateId = state;
            _state = _stateFactory.ChangeState(_stateId);
            _birdAnimator.Refresh(_stateId);
            _state.Start();
        }

        private void OnCollisionEnter(Collision other)
        {
            //other.impulse;
           // Debug.Log("Wall");
           // _state?.OnCollisionEnter(other);

            // var actualSpeed = _controller.velocity.z;

            // if (actualSpeed < 0.5f && Speed > 5f)
            //     ChangeState(PlayerBirdStates.Falling);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            _state?.OnCollisionEnter(hit.moveDirection);
            // if (_state is not PlayerBirdAirState || _state is PlayerBirdStateFall)
            //     return;

            // float angle = Vector3.Angle(hit.moveDirection, _controller.velocity);
            // //Debug.Log($"Angle: {Vector3.Angle(hit.moveDirection, _controller.velocity)}");

            // if (angle > 30f)
            //     ChangeState(PlayerBirdStates.Falling);
        }
    }
}
