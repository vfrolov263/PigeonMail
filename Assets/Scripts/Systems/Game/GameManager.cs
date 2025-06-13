using UnityEngine;
using UnityEngine.SceneManagement;

namespace PigeonMail
{
    public enum GameStates
    {
        MainMenu,
        Playing,
        Pause
    }

    public class GameManager
    {
        private GameStates _state = GameStates.MainMenu;


        private void StartGame()
        {
            SceneManager.LoadScene("DemoSceneVladimir", LoadSceneMode.Additive);
        }
    }
}
