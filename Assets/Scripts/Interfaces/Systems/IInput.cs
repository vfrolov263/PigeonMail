using System;
using UnityEngine;

namespace PigeonMail
{
    public interface IInput
    {
        event Action JumpActions;
        Vector2 Axes { get; }
        Vector2 PointerDelta { get; }
    }
}