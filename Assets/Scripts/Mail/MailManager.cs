using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class MailManager : MonoBehaviour
    {
        [SerializeField]
        private List<Human> _clerks;
        private List<Letter> _letters = new();
        private Letter.Factory _letterFactory;
        private Settings _settings;
        private SignalBus _signalBus;
        private AudioPlayer _audioPlayer;
        private LetterTimer _letterTimer;

        [Inject]
        public void Construct(Letter.Factory letterFactory, Settings settings, SignalBus signalBus,
            AudioPlayer audioPlayer, LetterTimer letterTimer)
        {
            _letterFactory = letterFactory;
            _settings = settings;
            _signalBus = signalBus;
            _signalBus.Subscribe<LetterStatusChangedSignal>(OnLetterStatusChanged);
            _signalBus.Subscribe<OutOfTimeSignal>(OnOutOfTimeSignal);
            _audioPlayer = audioPlayer;
            _letterTimer = letterTimer;
        }

        public bool IsTimeLimited { get; set; } = true;

        private void Start()
        {
            Invoke("CreateLetter", _settings.timeBeforeSpawnNew);
        }

        private void OnLetterStatusChanged(LetterStatusChangedSignal signal)
        {
            if (_letters.Count > 0 && signal.letter == _letters[0])
            {
                _signalBus.Fire(new TrackableLetterStatusChangedSignal() { letter = signal.letter });

                switch (signal.letter.Status)
                {
                    case LetterStatus.Delivering:
                        _letterTimer.gameObject.SetActive(IsTimeLimited);
                        break;
                    case LetterStatus.Delivered:
                    case LetterStatus.Lost:
                        _letterTimer.gameObject.SetActive(false);
                        break;
                }
            }
        }

        private void OnOutOfTimeSignal()
        {
            if (_letters.Count == 0)
                return;

            _letters[0].Status = LetterStatus.Lost;
        }

        public void ReleaseLetter(Letter letter)
        {
            // if (letter.Status == LetterStatus.Delivering)
            _letters.Remove(letter);
            Invoke("CreateLetter", _settings.timeBeforeSpawnNew);
        }

        private void CreateLetter()
        {
            if (_clerks.Count < 2)
                return;

            int to, from = UnityEngine.Random.Range(0, _clerks.Count);
            do
            {
                to = UnityEngine.Random.Range(0, _clerks.Count);
            } 
            while (to == from);

            _letters.Add(_letterFactory.Create(_clerks[from].transform, _clerks[to].transform, 1f));
           // _audioPlayer.PlayShot(_settings.newLetterSound);
           // _letters.Add(new(this, _clerks[from].transform, _clerks[to].transform, 1f));
        }

        [Serializable]
        public class Settings
        {
            public Vector3 letterClerkOffset;
            public Vector3 letterPlayerOffset;
            public Vector3 letterPlayerScale;
            public Vector3 receiverOffset;
            public float letterRotateSpeed;
            public float timeBeforeSpawnNew;
            public AudioClip newLetterSound;
        }
    }
}
