using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PigeonMail
{
    public class MailUI : MonoBehaviour
    {
        [SerializeField]
        private Image _flagImage;
        [SerializeField]
        private Image _goalImage;
        private Dictionary<CityColor, Sprite> _flags = new();

        private Settings _settings;

        [Inject]
        public void Construct(Settings settings, SignalBus signalBus)
        {
            _settings = settings;
            signalBus.Subscribe<TrackableLetterStatusChangedSignal>(OnTrackableLetterStatusChanged);
        }

        private void OnTrackableLetterStatusChanged(TrackableLetterStatusChangedSignal signal)
        {
            LetterStatus status = signal.letter.Status;
            Clerk clerk;

            switch (status)
            {
                case LetterStatus.Ready:
                    clerk = signal.letter.From.GetComponent<Clerk>();
                    break;
                case LetterStatus.Delivering:
                    clerk = signal.letter.To.GetComponent<Clerk>();
                    break;
                default:
                    clerk = null;
                    break;
            }

            CityColor flagColor = clerk == null ? CityColor.Red : clerk.CityFlagColor;
            NewGoal(flagColor, status);
        }

        private void Awake()
        {
            _flags.Add(CityColor.Red, _settings.redFlag);
            _flags.Add(CityColor.Blue, _settings.blueFlag);
            _flags.Add(CityColor.Yellow, _settings.yellowFlag);

        }

        public void NewGoal(CityColor color, LetterStatus status)
        {
            _flagImage.enabled = true;
            _goalImage.enabled = true;
            
            switch (status)
            {
                case LetterStatus.Ready:
                    _flagImage.sprite = _flags[color];
                    _goalImage.sprite = _settings.getGoal;
                    break;
                case LetterStatus.Delivering:
                    _flagImage.sprite = _flags[color];
                    _goalImage.sprite = _settings.giveGoal;
                    break;
                default:
                    _flagImage.enabled = false;
                    _goalImage.enabled = false;
                    break;
            }
        }

        [Serializable]
        public class Settings
        {
            public Sprite redFlag, blueFlag, yellowFlag;
            public Sprite giveGoal, getGoal;
        }
    }
}
