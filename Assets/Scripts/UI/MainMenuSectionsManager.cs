using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSectionsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainScreen, _settingsScreen, _loadingScreen, _aboutScreen;
    [SerializeField]
    private Button _continueButton, _startButton, _settingsButton, _aboutButton, _exitButton,
    _backSettingsButton, _backAboutButton;

    private void Start()
    {
        _continueButton.onClick.AddListener(OnContinuePressed);
        _startButton.onClick.AddListener(OnStartPressed);
        _settingsButton.onClick.AddListener(OnSettingsPressed);
        _aboutButton.onClick.AddListener(OnAboutPressed);
        _exitButton.onClick.AddListener(() => Application.Quit() );
        _backSettingsButton.onClick.AddListener(OnBackPressed);
        _backAboutButton.onClick.AddListener(OnBackPressed);
    }

    private void OnContinuePressed()
    {
        // _mainScreen.SetActive(false);
        // _settingsScreen.SetActive(true);
        OnStartPressed();
    }

    private void OnStartPressed()
    {
        _mainScreen.SetActive(false);
        _settingsScreen.SetActive(false);
        _loadingScreen.SetActive(true);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnSettingsPressed()
    {
        _mainScreen.SetActive(false);
        _settingsScreen.SetActive(true);
    }

    private void OnAboutPressed()
    {
        _mainScreen.SetActive(false);
        _aboutScreen.SetActive(true);
    }

    private void OnBackPressed()
    {
        _settingsScreen.SetActive(false);
        _aboutScreen.SetActive(false);
        _mainScreen.SetActive(true);
    }
}
