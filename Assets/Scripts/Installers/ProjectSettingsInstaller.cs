using System;
using UnityEngine;
using Zenject;

namespace PigeonMail
{
    [CreateAssetMenu(fileName = "ProjectSettingsInstaller", menuName = "Installers/ProjectSettingsInstaller")]
    public class ProjectSettingsInstaller : ScriptableObjectInstaller<ProjectSettingsInstaller>
    {
        public IInput.Settings InputSettings;
        public AudioPlayer.Settings AudioSettings;
        public SavedPrefsNames PrefsNames;
        
        [Serializable]
        public class SavedPrefsNames
        {
            public string resolution, fullscreen, inversion, volume, sensitivity;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(InputSettings);
            Container.BindInstance(AudioSettings);
            Container.BindInstance(PrefsNames);
        }
    }
}