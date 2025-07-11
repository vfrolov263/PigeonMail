using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace PigeonMail
{
    public class PlayerInput : MonoBehaviour, IInput
    {
        private IInput.Settings _settings;
        public event Action JumpActions;
        public event Action EscapeActions;

        [Inject]
        public void Construct(IInput.Settings settings) => _settings = settings;

        public Vector2 Axes
        {
            private set;
            get;
        }
        
        public Vector2 DirectionDelta
        {
            private set;
            get;
        }

        public Vector2 PointerDelta
        {
            private set;
            get;
        }
       
        private void OnDisable()
        {
            Axes = Vector2.zero;
            PointerDelta = Vector2.zero;
            DirectionDelta = Vector2.zero;
        }

        public void OnMove(InputValue value)
        {
            Axes = value.Get<Vector2>();
        }

        public void OnDirection(InputValue value)
        {
            DirectionDelta = GetDelta(value);;
        }

        public void OnLook(InputValue value)
        {
            PointerDelta = GetDelta(value);
            DirectionDelta = Vector2.zero;
        }

        private Vector2 GetDelta(InputValue value)
        {
            Vector2 delta = value.Get<Vector2>() * _settings.sensitivity;

            if (_settings.verticalInversion)
                delta.y = -delta.y;

            return delta;
        }

        public void OnJump(InputValue value)
        {
            JumpActions?.Invoke();
        }

        public void OnEscape(InputValue value)
        {
            EscapeActions?.Invoke();
        }
    }
}