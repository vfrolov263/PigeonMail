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

        // For showcase only
        event Action EscapeActions;

        [Serializable]
        public class Settings
        {
            public bool verticalInversion;
            [Range(.1f, 1f)]
            public float sensitivity;
        }
    }
}