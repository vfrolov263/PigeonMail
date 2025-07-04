using System;
using System.Collections;
using PigeonMail;
using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class TailHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject _tail1, _tail2;
        private PlayerBird _playerBird;
        private Settings _settings;

        [Inject]
        public void Construct(PlayerBird playerBird, Settings settings)
        {
            _playerBird = playerBird;
            _settings = settings;
        }

        private void Start()
        {
            StartCoroutine(CheckTail());
        }

        private IEnumerator CheckTail()
        {
            while (true)
            {
                
                if (_playerBird.State == PlayerBirdStates.Soaring && _playerBird.Speed >= _settings.speedOnTail)
                {
                    if (!_tail1.activeSelf)
                    {
                        _tail1.SetActive(true);
                        _tail2.SetActive(true);
                    }
                    // if (!_tail1.isEmitting)
                    // {
                    //     Debug.Log("Play");
                    //     _tail1.Play();
                    //     _tail2.Play();
                    // }
                }
                else if (_tail1.activeSelf)
                {
                    _tail1.SetActive(false);
                    _tail2.SetActive(false);
                    // Debug.Log("Stop");
                    // _tail1.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                    // _tail2.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                }

                yield return new WaitForSeconds(0.5f);
            }
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        public bool TailOn
        {
            set
            {
                if (_tail1.gameObject.activeSelf != value)
                {
                    if (value)
                    {
                        Debug.Log("Start trails");
                        // _tail1.Play();
                        // _tail2.Play();
                        _tail1.gameObject.SetActive(true);
                        _tail2.gameObject.SetActive(true);
                    }
                    else
                    {
                        _tail1.gameObject.SetActive(false);
                        _tail2.gameObject.SetActive(false);
                        Debug.Log("Stop trails");
                        // _tail1.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                        // _tail1.ena
                        // _tail2.Pause();
                        // var q = _tail1.emission;
                        // q.enabled = false;
                    }
                }
            }
        }

        [Serializable]
        public class Settings
        {
            public float speedOnTail;
        }
    }
}