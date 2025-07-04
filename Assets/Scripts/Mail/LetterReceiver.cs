using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class LetterReceiver : MonoBehaviour
    {
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _signalBus.Subscribe<OutOfTimeSignal>(OnOutOfTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Letter")
            {
                Letter letter = other.gameObject.GetComponent<Letter>();
                letter.Status = LetterStatus.Delivered;
                OnOutOfTime();
            }
        }

        private void OnOutOfTime()
        {
            _signalBus.Unsubscribe<OutOfTimeSignal>(OnOutOfTime);
            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<LetterReceiver>
        {
        }
    }
}