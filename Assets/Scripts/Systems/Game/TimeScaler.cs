using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class TimeScaler
    {
        private PlayerBird _playerBird;
        private PlayerInput _playerInput;

        public TimeScaler(PlayerBird playerBird, PlayerInput playerInput)
        {
            _playerBird = playerBird;
            _playerInput = playerInput;
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
            _playerInput.gameObject.SetActive(false);
            Time.timeScale = 0f;
        }

        public void PlayGame()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            _playerInput.gameObject.SetActive(true);
            _playerBird.enabled = true;
        }

        //private void Player
    }
}