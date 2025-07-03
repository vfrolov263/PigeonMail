using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class AudioPlayer : IInitializable, IDisposable
    {
        Camera _camera;
        Settings _settings;
        AudioSource _audioSource;

        private class AduioSoftBrake
        {
            public UniTask task;
            public CancellationTokenSource cancelToken;

            public AduioSoftBrake(UniTask task, CancellationTokenSource cancelToken)
            {
                this.task = task;
                this.cancelToken = cancelToken;
            }
        }

        private Dictionary<AudioClip, AudioSource> _loopedAudio = new();
        private Dictionary<AudioClip, AduioSoftBrake> _softStopedAudio = new();

        public AudioPlayer(Camera camera, Settings settings)
        {
            _camera = camera;
            _settings = settings;
        }

        public void Initialize()
        {
            _audioSource = _camera.GetComponent<AudioSource>();
        }

        public void PlayShot(AudioClip clip)
        {
            //_audioSource.PlayOneShot(clip, _settings.volume);
        }

        public AudioSource Play(AudioClip clip)
        {
            // RemoveLoopedAudio(clip);
            // var newLoopedAudio = _camera.gameObject.AddComponent<AudioSource>();
            // newLoopedAudio.clip = clip;
            // newLoopedAudio.loop = true;
            // newLoopedAudio.Play();
            // _loopedAudio.Add(clip, newLoopedAudio);
            // return newLoopedAudio;
            return null;
        }

        private bool RemoveLoopedAudio(AudioClip clip)
        {
            if (_loopedAudio.ContainsKey(clip))
            {
                GameObject.Destroy(_loopedAudio[clip]);
                _loopedAudio.Remove(clip);
                Debug.Log($"Remove looped audio. Now contains {_loopedAudio.Count} looped audio's.");
                return true;
            }

            return false;
        }

        public void Stop(AudioSource source)
        {
          //  RemoveLoopedAudio(source.clip);
            //source.Stop();
            //GameObject.Destroy(source);
        }

        public void SoftStop(AudioSource source)
        {
            // if (_softStopedAudio.ContainsKey(source.clip))
            // {
            //     _softStopedAudio[source.clip].cancelToken.Cancel();
            //     _softStopedAudio.Remove(source.clip);
            //     GameObject.Destroy(source);
            //     //Debug.Log($"Destroy soft {_softStopedAudio.Count}!");
            // }

            // _loopedAudio.Remove(source.clip);
            // CancellationTokenSource cancelToken = new();
            // _softStopedAudio.Add(source.clip, new AduioSoftBrake(StopAudioAsync(source, cancelToken), cancelToken));
        }

        public async UniTask StopAudioAsync(AudioSource source, CancellationTokenSource cancelToken)
        {
            try
            {
                while (source.volume > 0f)
                {
                    source.volume -= _settings.decayRate * Time.fixedDeltaTime;
                    await UniTask.WaitForFixedUpdate(cancelToken.Token);
                }
            }
            catch (OperationCanceledException e)
            {
                Debug.Log(e.Message);
            }

            _softStopedAudio.Remove(source.clip);
            GameObject.Destroy(source);
        }

        public void Dispose()
        {
            foreach (var i in _softStopedAudio)
                i.Value.cancelToken.Cancel();
        }

        [Serializable]
        public class Settings
        {
            [Range(0f, 1f)]
            public float volume;
            public float decayRate;
        }
    }
}