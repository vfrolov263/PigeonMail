using System;
using UnityEditor.SettingsManagement;
using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public enum LetterStatus
    {
        Ready,
        Delivering,
        Delivered,
        Lost
    }

    public class Letter : MonoBehaviour, IPoolable<Transform, Transform, float, IMemoryPool>, IDisposable
    {
        public Transform From { private set; get; }
        public Transform To { private set; get; }
        public float TimeForDelivary { private set; get; }
        private IMemoryPool _pool;
        private LetterStatus _status;
        private PlayerBird _player;
        private MailManager _mailManager;
        private MailManager.Settings _settings;
        private LetterReceiver.Factory _receiverFactory;
        private MailUI _mailUI;
        private SignalBus _signalBus;

        public LetterStatus Status
        { 
            set
            {
                if (_status < value)
                {
                    _status = value;
                    _signalBus.Fire(new LetterStatusChangedSignal() { letter = this });

                    if (_status > LetterStatus.Delivering)
                    {
                        _mailManager.ReleaseLetter(this);
                        Destroy(gameObject);
                    }
                }
            }
            get
            {
                return _status;
            }
        }

        [Inject]
        public void Construct(PlayerBird player, MailManager mailManager, 
        MailManager.Settings settings, LetterReceiver.Factory receiverFactory,
        MailUI mailUI, SignalBus signalBus)
        {
            _mailManager = mailManager;
            _settings = settings;
            _player = player;
            _receiverFactory = receiverFactory;
            _mailUI = mailUI;
            _signalBus = signalBus;
        }

        private void Start()
        {
           _signalBus.Fire(new LetterStatusChangedSignal() { letter = this });
           var rotation = gameObject.AddComponent<RotateGameObject>();
           rotation.Speed = _settings.letterRotateSpeed;
        }

        public void OnDespawned()
        {
            _pool = null;
        }

        public void OnSpawned(Transform from, Transform to, float timeForDelivary, IMemoryPool pool)
        {
            From = from;
            To = to;
            TimeForDelivary = timeForDelivary;
            _pool = pool;
            transform.parent = from;
            transform.localPosition = _settings.letterClerkOffset;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && Status == LetterStatus.Ready)
            {
                Status = LetterStatus.Delivering;
                transform.parent = _player.transform;
                transform.localPosition = _settings.letterPlayerOffset;
                transform.localEulerAngles = Vector3.zero;
                transform.localScale = _settings.letterPlayerScale;
                Destroy(GetComponent<RotateGameObject>());
                GetComponent<Light>().enabled = false;
                var receiver = _receiverFactory.Create();
                receiver.transform.parent = To;
                receiver.transform.localPosition = _settings.receiverOffset;
            }
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }

        public class Factory : PlaceholderFactory<Transform, Transform, float, Letter>
        {
        }
    }
}
