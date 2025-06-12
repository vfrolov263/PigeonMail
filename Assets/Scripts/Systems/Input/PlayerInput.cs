using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PigeonMail
{
    public class PlayerInput : MonoBehaviour, IInput
    {
        public event Action JumpActions;
        public Vector2 Axes
        {
            private set;
            get;
        }
        private Vector2 pD;
        public Vector2 PointerDelta
        {
            private set {pD = value; }
            get { var ret = pD; ; return ret; }
        }
       
        public void OnMove(InputValue value)
        {
            Axes = value.Get<Vector2>();
        }

        public void OnLook(InputValue value)
        {
            PointerDelta = value.Get<Vector2>();
        }

        public void OnJump(InputValue value)
        {
            JumpActions?.Invoke();
        }
    }
}
