using UnityEngine;

namespace PigeonMail
{
    public static class VectorExtensions
    {
        public static Vector3 Limit(this Vector3 vect, in Vector3 minVect, in Vector3 maxVect) =>
            Vector3.Min(Vector3.Max(vect, minVect), maxVect);
    }
}
