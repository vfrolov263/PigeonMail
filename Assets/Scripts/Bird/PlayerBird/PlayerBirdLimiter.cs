using System;
using Unity.VisualScripting;
using UnityEditor.SettingsManagement;
using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class PlayerBirdLimiter : ILateTickable
    {
        PlayerBird _playerBird;
        Settings _settings;

        [Inject]
        public void Construct(PlayerBird playerBird, Settings settings)
        {
            _playerBird = playerBird;
            _settings = settings;
        }

        public void LateTick()
        {
            _playerBird.transform.position = _playerBird.transform.position.
                Limit(_settings.minHabitatPoint, _settings.maxHabitatPoint);
        }

        [Serializable]
        public class Settings
        {
            public Vector3 minHabitatPoint, maxHabitatPoint;
        }
    }
}
