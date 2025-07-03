using System.Collections.Generic;
using ModestTree;
using PigeonMail;
using UnityEngine;
using Zenject;

public class TutorialActions : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _lessonScreens;
    [SerializeField]
    private List<GameObject> _lessonTriggers;
    [SerializeField]
    private GameObject _yellowCityTrigger, _redCityTrigger, _blueCityTrigger;
    [SerializeField]
    private int fromCityTriggerIndex, fromClerkTriggerIndex, toCityTriggerIndex, toClerkTriggerIndex;

    private int _currentLesson;
    private SignalBus _signalBus;
    private GameObject _mailManager;
    private TimeScaler _timeScaler;

    [Inject]
    public void Construct(SignalBus signalBus, MailManager mailManager, TimeScaler timeScaler)
    {
        _signalBus = signalBus;
        _mailManager = mailManager.gameObject;
        _timeScaler = timeScaler;
    }

    private void Start()
    {
        _signalBus.Subscribe<NextLessonSignal>(NextLesson);
        _signalBus.Subscribe<TrackableLetterStatusChangedSignal>(OnMailNextLesson);
        Invoke("ActivateCurrentLesson", .5f);
    }

    public void NextLesson()
    {
        _lessonTriggers[_currentLesson].SetActive(false);
        _currentLesson++;
        
        if (_currentLesson == fromCityTriggerIndex || _currentLesson == toCityTriggerIndex)
            return;
        else if (_currentLesson < _lessonScreens.Count)
            ActivateCurrentLesson();
        else
            FinishTutorial();
    }

    public void HideScreen()
    {
        _lessonScreens[_currentLesson].SetActive(false);
        _timeScaler.PlayGame();
    }

    private void ActivateCurrentLesson()
    {
        _lessonScreens[_currentLesson].SetActive(true);
        
        if (_lessonTriggers[_currentLesson] != null)
            _lessonTriggers[_currentLesson].SetActive(true);

        _timeScaler.PauseGame();
    }

    private void OnMailNextLesson(TrackableLetterStatusChangedSignal signal)
    {
        switch (signal.letter.Status)
        {
            case LetterStatus.Ready:
                ChooseLessonCities(ref signal);
                break;
            default:
                _currentLesson++;
                break;
        }

        ActivateCurrentLesson();
    }

    private void ChooseLessonCities(ref TrackableLetterStatusChangedSignal signal)
    {
        var fromClerk = signal.letter.From.GetComponent<Clerk>();
        var toClerk = signal.letter.To.GetComponent<Clerk>();
        _lessonTriggers[fromCityTriggerIndex] = ChooseTrigger(fromClerk.CityFlagColor);
        _lessonTriggers[toCityTriggerIndex] = ChooseTrigger(toClerk.CityFlagColor);
        _lessonTriggers[fromClerkTriggerIndex].transform.parent = fromClerk.transform;
        _lessonTriggers[fromClerkTriggerIndex].transform.localPosition = Vector3.zero;
        _lessonTriggers[toClerkTriggerIndex].transform.parent = toClerk.transform;
        _lessonTriggers[toClerkTriggerIndex].transform.localPosition = Vector3.zero;
    }

    private GameObject ChooseTrigger(CityColor color)
    {
        switch (color)
        {
            case CityColor.Red:
                return _redCityTrigger;
            case CityColor.Yellow:
                return _yellowCityTrigger;
            case CityColor.Blue:
                return _blueCityTrigger;
            default:
                throw Assert.CreateException();
        }
    }

    private void OnDestroy()
    {
        foreach (GameObject i in _lessonTriggers)
            Destroy(i);

        _signalBus.Unsubscribe<TrackableLetterStatusChangedSignal>(OnMailNextLesson);
    }

    public void StartMailService()
    {
        _mailManager.SetActive(true);
    }

    public void FinishTutorial()
    {
        _mailManager.SetActive(true);

        if (_timeScaler.IsPaused)
            _timeScaler.PlayGame();

        Destroy(gameObject);
    }
}
