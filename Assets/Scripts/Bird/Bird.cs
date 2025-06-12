using UnityEngine;

namespace PigeonMail
{
    public abstract class Bird : MonoBehaviour, IMovable
    {
        public abstract void Move(Vector3 motion);
    }
}