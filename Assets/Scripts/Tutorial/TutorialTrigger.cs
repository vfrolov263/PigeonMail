using PigeonMail;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class TutorialTrigger : MonoBehaviour
{
    public UnityEvent actions;

    [Inject] SignalBus _signalBus;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            actions?.Invoke();
            _signalBus.Fire<NextLessonSignal>();
            Destroy(gameObject);
        }
    }
}
