using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PigeonMail
{
    public class SelectButtonOnEnable : MonoBehaviour
    {
        private static IInput.Settings _settings;

        [Inject]
        public void Construct(IInput.Settings settings)
        {
            _settings ??= settings;
        }

        private void OnEnable()
        {
            if (_settings.gamepadConnected)
                GetComponent<Button>().Select();
        }
    }
}