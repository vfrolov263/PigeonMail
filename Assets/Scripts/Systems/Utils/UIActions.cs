using UnityEngine;

namespace PigeonMail
{
    public class UIActions : MonoBehaviour
    {
        [SerializeField]
        private GameObject _gameObject;

        public void Activate() => _gameObject?.SetActive(true);

        public void Deactivate() => _gameObject?.SetActive(false);
    }
}