using System;
using UnityEngine;
using Zenject;

namespace PigeonMail
{
    [CreateAssetMenu(fileName = "ProjectSettingsInstaller", menuName = "Installers/ProjectSettingsInstaller")]
    public class ProjectSettingsInstaller : ScriptableObjectInstaller<ProjectSettingsInstaller>
    {
        public IInput.Settings InputSettings;
        public SavedPrefsNames PrefsNames;
        
        [Serializable]
        public class SavedPrefsNames
        {
            public string inversion, volume, sensitivity;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(InputSettings);
            Container.BindInstance(PrefsNames);
        }
    }
}