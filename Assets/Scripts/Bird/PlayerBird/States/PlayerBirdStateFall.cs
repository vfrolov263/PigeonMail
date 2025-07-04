using System;
using PigeonMail;
using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class PlayerBirdStateFall : PlayerBirdAirState
    {
        public PlayerBirdStateFall(Settings settings, BombHandler bombHandler) : base(settings, bombHandler)
        {
        }

        public override void Start()
        {
            base.Start();
            // _playerBird.GetComponent<CharacterController>().enabled = false;
            // _playerBird.GetComponent<Rigidbody>().isKinematic = false;
        }

        public override void Update()
        {
            //base.Update();
            Vector3 motion = new(0f, -_settings.gravityFallForce, 0f);
            _playerBird.Move(motion * Time.deltaTime);
            CheckLanding();
        }

        // public override void OnCollisionEnter(Vector3 moveDirection)
        // {
            
        // }

        // public override void Dispose()
        // {
        //     // _playerBird.GetComponent<CharacterController>().enabled = true;
        //     // _playerBird.GetComponent<Rigidbody>().isKinematic = true;
        // }

        public class Factory : PlaceholderFactory<PlayerBirdStateFall>
        {
        }
    }
}