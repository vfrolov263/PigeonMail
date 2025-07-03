using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public abstract class PlayerBirdAirState : PlayerBirdState
    {
        protected readonly Settings _settings;

        private struct RotationZParams
        {
            public float speed;
            public float accuracy;
            public float currentMaxAccuracy;
        }

        private float _rotationZSpeed;
        // /* Test z */ private float settingsRotationZMaxSpeed = 250f, settingsRotationZMaxAccuracy = 500f, 
        // settingsRotationZAccuracyOffset = 200f, settingsRotationZAccuracyPerDelta = 850f;
        private RotationZParams _rotationZ;

        public PlayerBirdAirState(Settings settings)
        {
            _settings = settings;
        }

        public override void Start()
        {
            base.Start();
            _rotation.x = BringAnlge(_rotation.x);
            _rotation.z = BringAnlge(_rotation.z);

            if (_playerBird.Speed < _settings.minSpeed)
                _playerBird.Speed = _settings.minSpeed;
        }

        private float BringAnlge(float angle)
        {
            angle %= 360f;
            angle -= 360f * (float)Math.Truncate(angle / 180f);
            return angle;
        }

        public override void Update()
        {
            CalculateMotion();
            CheckLanding();
        }

        private void CalculateMotion()
        {
            _rotation.x -= _input.DirectionDelta.y * _settings.rotationSpeed.x * Time.deltaTime;
            _rotation.x = Math.Clamp(_rotation.x, _settings.minAngleX, _settings.maxAngleX);

            /*  Test z */
//             float rotationZAcc = _input.DirectionDelta.x * _settings.rotationSpeed.z;
//             rotationZAcc = Math.Clamp(rotationZAcc, -1000f, 1000f);
//             _rotationZSpeed += rotationZAcc * Time.deltaTime;
//             _rotationZSpeed = Math.Clamp(_rotationZSpeed, -230f, 230f);
            
//             _rotationZSpeed -= _rotationZSpeed * Time.deltaTime;

//             if (Mathf.Approximately(_rotationZSpeed, 0.01f))
//                 _rotationZSpeed = 0f;

// Debug.Log($"Mouse x: {_input.DirectionDelta.x}");
//             Debug.Log(_rotationZSpeed);

            /* inertional variant */

           // CalculateZAxis();

            // float maxRotatationZSpeed = 250f;
            // float maxSlow = 600f;
            // float rotationZAcc = _input.DirectionDelta.x * 350f;//_settings.rotationSpeed.z;
            // rotationZAcc = Math.Clamp(rotationZAcc, -1500f, 1500f);
            // _rotationZSpeed += rotationZAcc * Time.deltaTime;
            // _rotationZSpeed = Math.Clamp(_rotationZSpeed, -maxRotatationZSpeed, maxRotatationZSpeed);

            // _rotation.z -= _rotationZSpeed * Time.deltaTime;

            // float slowdown = (float)Math.Pow(1f - (Math.Abs(_rotationZSpeed) / maxRotatationZSpeed), 2.0) * maxSlow;

            // if (!Mathf.Approximately(_rotationZSpeed, 0f))
            //     _rotationZSpeed -= (_rotationZSpeed/(Math.Abs(_rotationZSpeed))) * slowdown * Time.deltaTime;
            // else
            //     _rotationZSpeed = 0f;


            // if (_rotation.z > _settings.maxAngleZ)
            // {
            //     if (_rotationZSpeed < 0f)
            //         _rotationZSpeed = 0f;

            //     _rotation.z = _settings.maxAngleZ;
            // }
            // else if (_rotation.z < -_settings.maxAngleZ)
            // {
            //     if (_rotationZSpeed > 0f)
            //         _rotationZSpeed = 0f;

            //     _rotation.z = -_settings.maxAngleZ;
            // }


            /* inertioanl variant */

            /* 2nd varinant */
        //     float maxRotSpeed = 250f;
        //     float newRotationSpeed = _input.DirectionDelta.x * 20f;// _settings.rotationSpeed.z; // rotationPerDelta
        //     //Debug.Log(_input.DirectionDelta.x);

        //     if (Mathf.Approximately(_rotationZSpeed, 0f) || _rotationZSpeed * newRotationSpeed < 0f)
        //         _rotationZSpeed = newRotationSpeed;//Mathf.Clamp(newRotationSpeed, -maxRotSpeed, maxRotSpeed);

        //     _rotation.z -= Mathf.Clamp(_rotationZSpeed, -maxRotSpeed, maxRotSpeed) * Time.deltaTime;
        //     _rotation.z = Math.Clamp(_rotation.z, -_settings.maxAngleZ, _settings.maxAngleZ);

        //    // if (_rotationZSpeed )

        //     if (Math.Abs(_rotationZSpeed) < maxRotSpeed * 2)
        //         _rotationZSpeed = 0f;
        //     else
        //     {
        //         Debug.Log(_rotationZSpeed);
        //         float roationSpeedSlowdown = 0.999f * _rotationZSpeed * Time.deltaTime;
        //         _rotationZSpeed -= roationSpeedSlowdown;
        //     }

            /*2nd variant end */

            /* 1 variant with normilize */
            _rotation.z -= _input.DirectionDelta.x * _settings.rotationSpeed.z * Time.deltaTime;
            _rotation.z = Math.Clamp(_rotation.z, -_settings.maxAngleZ, _settings.maxAngleZ);
            /* 1st variant with normilize */

            var oldRotation = _playerBird.transform.localEulerAngles;
            oldRotation.x = _rotation.x;
            oldRotation.z = _rotation.z;
            _playerBird.transform.localEulerAngles = oldRotation;

            _rotation.y = -(_rotation.z / _settings.maxAngleZ) * _settings.rotationSpeed.y * Time.deltaTime;
            _playerBird.transform.Rotate(new(0f, _rotation.y, 0f), Space.World);

            Vector3 motion = _playerBird.transform.forward * _playerBird.Speed;
            //Debug.Log($"Motion: {motion}");
            _playerBird.Move(motion * Time.deltaTime, out var flags);

            // if (flags != CollisionFlags.None)
            //     Debug.Log($"Side collision at {Time.timeSinceLevelLoad}");
        }

        private void CalculateZAxis()
        {
            float settingsRotationZMaxSpeed = 250f, settingsRotationZMaxAccuracy = 500f, 
            settingsRotationZAccuracyOffset = 200f, settingsRotationZAccuracyPerDelta = 850f;

            _rotationZ.currentMaxAccuracy = settingsRotationZAccuracyOffset + (1f - Math.Abs(_rotationZ.speed) / 
                settingsRotationZMaxSpeed) * settingsRotationZMaxAccuracy;
            _rotationZ.accuracy = _input.DirectionDelta.x * settingsRotationZAccuracyPerDelta;
            
            _rotationZ.accuracy = Math.Clamp(_rotationZ.accuracy, -_rotationZ.currentMaxAccuracy, _rotationZ.currentMaxAccuracy);

            _rotationZ.speed += _rotationZ.accuracy * Time.deltaTime;
            _rotationZ.speed = Math.Clamp(_rotationZ.speed, -settingsRotationZMaxSpeed, settingsRotationZMaxSpeed);
            Debug.Log(_rotationZ.speed);

            _rotation.z -= _rotationZ.speed * Time.deltaTime;

            float slowdown = (float)Math.Pow(1f - (Math.Abs(_rotationZ.speed) / settingsRotationZMaxSpeed), 2.0) * 400f;

            if (!Mathf.Approximately(_rotationZ.speed, 0f))
                _rotationZ.speed -= (_rotationZ.speed/(Math.Abs(_rotationZ.speed))) * slowdown * Time.deltaTime;
            else
                _rotationZ.speed = 0f;


            if (_rotation.z > _settings.maxAngleZ)
            {
                if (_rotationZ.speed < 0f)
                    _rotationZ.speed = 0f;

                _rotation.z = _settings.maxAngleZ;
            }
            else if (_rotation.z < -_settings.maxAngleZ)
            {
                if (_rotationZ.speed > 0f)
                    _rotationZ.speed = 0f;

                _rotation.z = -_settings.maxAngleZ;
            }
        }

        protected void CheckLanding()
        {
            if (_playerBird.IsGrounded)
                _playerBird.ChangeState(PlayerBirdStates.Idle);
        }

        public override void OnCollisionEnter(Vector3 moveDirection)
        {
            float angle = Vector3.Angle(moveDirection, _playerBird.Velocity);

            if (angle > 30f && _playerBird.Speed > 10f)
                _playerBird.ChangeState(PlayerBirdStates.Falling);
        }

        [Serializable]
        public class Settings
        {
            public float maxSpeed, minSpeed;
            public float accelaration, deceleration, activeDeceleration;
            public Vector3 rotationSpeed;
            public float minAngleX, maxAngleX, maxAngleZ;
            public float gravityForce, gravityFuncPower, gravityFallForce;
            public AudioClip flySound, soarSound;
        }
    }
}