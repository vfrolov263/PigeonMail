using UnityEngine;
using Zenject;
using System;

namespace PigeonMail
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public GameInstaller.Settings GameInstaller;
        public PlayerBirdSettings PlayerBird;
        public MailSettings Mail;
        public CityStateTracker.Settings CitySettings;
        public CannoballsDamager.Settings CannoballsSettings;

        [Serializable]
        public class PlayerBirdSettings
        {
            public PlayerBirdAirState.Settings FlySettings;
            public PlayerBirdGroundedState.Settings GroundSettings;
            public PlayerBirdStateTakeOff.Settings TakeOffSettings;
            public PlayerBirdLimiter.Settings LimitSettings;
            public TailHandler.Settings EffectsSettings;
        }

        [Serializable]
        public class MailSettings
        {
            public MailManager.Settings ManagerSettings;
            public MailUI.Settings UISettings;
            public LetterTimer.Settings TimerSettings;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(GameInstaller);
            Container.BindInstance(PlayerBird.FlySettings);
            Container.BindInstance(PlayerBird.GroundSettings);
            Container.BindInstance(PlayerBird.TakeOffSettings);
            Container.BindInstance(PlayerBird.LimitSettings);
            Container.BindInstance(PlayerBird.EffectsSettings);
            Container.BindInstance(Mail.ManagerSettings);
            Container.BindInstance(Mail.UISettings);
            Container.BindInstance(Mail.TimerSettings);
            Container.BindInstance(CitySettings);
            Container.BindInstance(CannoballsSettings);
        }
    }
}