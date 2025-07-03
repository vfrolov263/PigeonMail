using PigeonMail;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Pause : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenu;
    TimeScaler _timeScaler;

    [Inject]
    public void Construct(TimeScaler timeScaler, IInput input)
    {
        _timeScaler = timeScaler;
        input.EscapeActions += PauseSwitch;
    }
    
    public bool Paused
    {
        get
        {
            return _timeScaler.IsPaused;
        }
        set
        {
            if (value)
            {
                if (!_timeScaler.IsPaused)
                {
                    _pauseMenu.SetActive(value);
                    _timeScaler.PauseGame();
                }
            }
            else
            {
                _pauseMenu.SetActive(value);
                _timeScaler.PlayGame();
            }
        }
    }

    public void PauseOff() => Paused = false;
    
    private void PauseSwitch() => Paused = !Paused;

    public void ExitMenu()
    {
        SceneManager.LoadScene(0);
    }
}
