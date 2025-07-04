using System.Collections.Generic;
using PigeonMail;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SecretsOpener : MonoBehaviour
{
    [SerializeField]
    private List<Image> _secrets;

    [Inject]
    public void Construct(ProjectSettingsInstaller.SavedPrefsNames prefsNames)
    {
        string name = prefsNames.easterEgg;

        for (int i = 0;i < _secrets.Count; i++)
        {
            if (PlayerPrefs.HasKey(name + i.ToString()))
                _secrets[i].color = Color.white;
        }
    }
}
