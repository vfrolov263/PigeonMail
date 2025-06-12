using UnityEngine;
using Zenject;
using System;

namespace PigeonMail
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public PlayerBirdSettings PlayerBird;

        [Serializable]
        public class PlayerBirdSettings
        {
            public PlayerBirdAirState.Settings FlySettings;
            public PlayerBirdGroundedState.Settings GroundSettings;
            public PlayerBirdStateTakeOff.Settings TakeOffSettings;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(PlayerBird.FlySettings);
            Container.BindInstance(PlayerBird.GroundSettings);
            Container.BindInstance(PlayerBird.TakeOffSettings);
        }
    }
}