using PigeonMail;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class EscLogic : MonoBehaviour
{
    [SerializeField]
    GameObject _fire;
    GameObject _startScreen;
    IInput _input;

    
    [Inject]
    public void Construct(IInput input)
    {
        input.EscapeActions += SwitchMenu;
    }

    void Start()
    {
        _startScreen = transform.GetChild(0).gameObject;
        Time.timeScale = _startScreen.activeSelf ? 0f : 1f;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else if (Input.GetKeyDown(KeyCode.J))
            _fire.SetActive(!_fire.activeSelf);
        else if (Input.GetKeyDown(KeyCode.P))
            Application.Quit();
    }

    private void SwitchMenu()
    {
        if (_startScreen.activeSelf)
        {
            _startScreen.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            _startScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
