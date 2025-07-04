using Cysharp.Threading.Tasks;
using PigeonMail;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class MainMenuSectionsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainScreen, _settingsScreen, _loadingScreen, _aboutScreen, _secretsScreen;
    [SerializeField]
    private Button _continueButton, _startButton, _settingsButton, _aboutButton, _exitButton,
    _backSettingsButton, _backAboutButton, _secretsButton, _backSecretsButton;
    [Inject]
    private ProjectSettingsInstaller.SavedPrefsNames _prefsNames;

    private void Start()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        if (IsFirstPlay())
        {
            Destroy(_continueButton.gameObject);
            _continueButton = null;
            _startButton.AddComponent<SelectButtonOnEnable>();
        }
        else
        {
            _continueButton.onClick.AddListener(OnContinuePressed);
            _continueButton.AddComponent<SelectButtonOnEnable>();
        }

        _startButton.onClick.AddListener(OnStartPressed);
        _settingsButton.onClick.AddListener(OnSettingsPressed);
        _aboutButton.onClick.AddListener(OnAboutPressed);
        _exitButton.onClick.AddListener(() => Application.Quit() );
        _backSettingsButton.onClick.AddListener(OnBackPressed);
        _backSettingsButton.AddComponent<SelectButtonOnEnable>();
        _backAboutButton.onClick.AddListener(OnBackPressed);
        _backAboutButton.AddComponent<SelectButtonOnEnable>();
        _secretsButton.onClick.AddListener(OnSecretsPressed);
        _backSecretsButton.AddComponent<SelectButtonOnEnable>();
        _backSecretsButton.onClick.AddListener(OnBackPressed);
    }

    private bool IsFirstPlay()
    {
        return !(PlayerPrefs.HasKey(_prefsNames.redCity) ||
            PlayerPrefs.HasKey(_prefsNames.blueCity) ||
            PlayerPrefs.HasKey(_prefsNames.yellowCity));
    }

    private void OnContinuePressed()
    {
        StartGame();
    }

    private void OnStartPressed()
    {
        PlayerPrefs.DeleteKey(_prefsNames.redCity);
        PlayerPrefs.DeleteKey(_prefsNames.blueCity);
        PlayerPrefs.DeleteKey(_prefsNames.yellowCity);
        PlayerPrefs.DeleteKey(_prefsNames.cannonballs);
        StartGame();
    }

    private void StartGame()
    {
        _mainScreen.SetActive(false);
        //_settingsScreen.SetActive(false);
        _loadingScreen.SetActive(true);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnSecretsPressed()
    {
        _mainScreen.SetActive(false);
        _secretsScreen.SetActive(true);
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
        _secretsScreen.SetActive(false);
        _mainScreen.SetActive(true);
    }
}
