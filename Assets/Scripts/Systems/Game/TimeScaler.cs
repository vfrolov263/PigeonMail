using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class TimeScaler
    {
        private PlayerBird _playerBird;

        public TimeScaler(PlayerBird playerBird)
        {
            _playerBird = playerBird;
        }

        public bool IsPaused
        {
            get
            {
                return Mathf.Approximately(Time.timeScale, 0f);
            }
        }

        public void PauseGame()
        {
            _playerBird.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }

        public void PlayGame()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            _playerBird.enabled = true;
        }
    }
}