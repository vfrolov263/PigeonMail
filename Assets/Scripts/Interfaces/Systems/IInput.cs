using System;
using Unity.VisualScripting;
using UnityEngine;

namespace PigeonMail
{
    public interface IInput
    {
        event Action JumpActions;
        Vector2 Axes { get; }
        Vector2 PointerDelta { get; }
        Vector2 DirectionDelta { get; }

        event Action EscapeActions;

        [Serializable]
        public class Settings
        {
            public bool verticalInversion;
            [Range(.2f, 1.6f)]
            public float sensitivity;
            public bool gamepadConnected;
        }
    }
}