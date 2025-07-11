using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace PigeonMail
{
    public class GamepadConnectHandler : MonoBehaviour
    {
        [Inject]
        IInput.Settings _settings;

        private void OnDeviceChanged(InputDevice device, InputDeviceChange change)
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                case InputDeviceChange.Removed:
                    CheckGamePadConnection();
                    break;
            }
        }

        private void Awake()
        {
            InputSystem.onDeviceChange += OnDeviceChanged;
            CheckGamePadConnection();
        }

        private void OnDisable() => InputSystem.onDeviceChange -= OnDeviceChanged;

        private void CheckGamePadConnection() => _settings.gamepadConnected = Gamepad.all.Count > 0;
    }
}