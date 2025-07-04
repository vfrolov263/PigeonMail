using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class GoldenEgg : MonoBehaviour
    {
        [SerializeField]
        private int _eggId;
        [SerializeField]
        GameObject _easterEggUI;
        private string _name;

        [Inject] 
        public void Construct(ProjectSettingsInstaller.SavedPrefsNames prefsNames)
        {
            _name = prefsNames.easterEgg + _eggId.ToString();
        }

        private void Awake()
        {
            if (PlayerPrefs.HasKey(_name))
                Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Player")
                return;

            PlayerPrefs.SetInt(_name, 1);
            _easterEggUI.SetActive(true);
            Destroy(gameObject);
        }
    }
}
