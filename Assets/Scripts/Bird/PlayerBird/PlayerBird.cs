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
                return TryGetComponent<Animator>(out Animator animator) ? animator : null;
            }
        }

        public override void Move(Vector3 motion)
        {
            _controller.Move(motion * Time.deltaTime);
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
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
                
            _state?.Update();
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
            _state?.OnTriggerEnter(other.collider);
        }
    }
}
