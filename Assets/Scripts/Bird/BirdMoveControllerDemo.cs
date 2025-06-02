using System;
using UnityEngine;

public class BirdMoveControllerDemo : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 6.0f;
    [SerializeField] private float _accelarationPower = 1.0f;
    [SerializeField] private float _rotateSpeed = 2.0f;
    [SerializeField] private float _upSpeed = 2.0f;
    [SerializeField] private float _maxGravityForce = 1.0f;

    private Vector3 _moveDirection = Vector3.zero;
    private CharacterController _controller;
    private float _speed, _acc;
    private Animator _animator;
    private float _gravityForce = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _controller = GetComponent<CharacterController>();        
        _animator = GetComponent<Animator>();
        _speed = _maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        _acc = Input.GetAxis("Vertical") * _accelarationPower;// * Time.deltaTime;

        if (_speed < _maxSpeed)
            _speed += _acc * Time.deltaTime;;

        if (_acc == 0f && _speed > 0f)
            _speed -= _accelarationPower * Time.deltaTime;

        transform.Rotate(0, Input.GetAxis("Mouse X") * _rotateSpeed, 0);

        _animator.SetBool("Grounded", _controller.isGrounded);
        _animator.SetBool("Accelerate", _acc > 0f);
        _animator.SetBool("Move", _speed > 0);

       // transform.Rotate(0, Input.GetAxis("Mouse X") * _rotateSpeed, 0);
        _moveDirection = transform.forward * _speed;

        if (_controller.isGrounded)
            _speed = 0f;

        if (Input.GetKey("space"))
            _upSpeed = 5f;
        else
            _upSpeed = 1f;

        _gravityForce = _maxGravityForce * (1f - (float)Math.Pow(_speed / _maxSpeed, 2f));


        if (_acc <= 0f)
            _moveDirection.y -= _gravityForce;


        if (_acc > 0f)
            _moveDirection.y += _upSpeed;

        _controller.Move(_moveDirection * Time.deltaTime);
    }
}

