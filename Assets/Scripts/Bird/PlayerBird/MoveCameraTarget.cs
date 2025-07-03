using UnityEngine;
using Zenject;

// Feauture not working for now
namespace PigeonMail
{
    public class MoveCameraTarget : MonoBehaviour
    {
        [SerializeField]
        private Transform _bird, _target;

        private IInput _input;
        
        [Inject]
        public void Construct(IInput input)
        {
            _input = input;
        }

        private void Update()
        {
            var rotation = _input.PointerDelta * 300f * Time.deltaTime;
            transform.position = _bird.position;
            //_target.SetLocalY(_target.position.y + rotation.y);
            transform.Rotate(new Vector3(-rotation.y, rotation.x, 0f), Space.World);
            //_target.RotateAround(_bird.position, Vector3.up, rotation.x);
            //_target.RotateAround(_bird.position, Vector3.right, rotation.y);
        }
    }
}