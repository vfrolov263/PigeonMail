using UnityEngine;

namespace PigeonMail
{
    public static class TransformExtensions
    {
        public static void SetX(this Transform transform, float x)
        {
            Vector3 position = transform.position;
            position.x = x;
            transform.position = position;
        }

        public static void SetY(this Transform transform, float y)
        {
            Vector3 position = transform.position;
            position.y = y;
            transform.position = position;
        }

        public static void SetZ(this Transform transform, float z)
        {
            Vector3 position = transform.position;
            position.z = z;
            transform.position = position;
        }

        public static void SetLocalX(this Transform transform, float x)
        {
            Vector3 position = transform.localPosition;
            position.x = x;
            transform.localPosition = position;
        }

        public static void SetLocalY(this Transform transform, float y)
        {
            Vector3 position = transform.localPosition;
            position.y = y;
            transform.localPosition = position;
        }

        public static void SetLocalZ(this Transform transform, float z)
        {
            Vector3 position = transform.localPosition;
            position.z = z;
            transform.localPosition = position;
        }
    }
}