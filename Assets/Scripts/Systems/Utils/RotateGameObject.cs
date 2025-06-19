using UnityEngine;

public class RotateGameObject : MonoBehaviour
{
    [SerializeField]
    private float _defaultSpeed = 45f;

    public Transform Target { get; set; }
    public Vector3 Axis { get; set; }
    public float Speed { get; set; }

    private void Awake()
    {
        Target = transform;
        Axis = Vector3.up;
        Speed = _defaultSpeed;
    }

    private void Update()
    {
        transform.RotateAround(Target.position, Axis, Speed * Time.deltaTime);
    }
}