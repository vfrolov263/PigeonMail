using UnityEngine;

namespace PigeonMail
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] protected float _debugDrawReadius = 1.0f;
        [SerializeField] private float _waitTime = .0f;
        public float WaitTime
        { 
            get { return _waitTime; }
            set { _waitTime = value; }
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _debugDrawReadius);
        }
    }
}