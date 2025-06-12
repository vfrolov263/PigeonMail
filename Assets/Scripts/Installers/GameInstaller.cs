using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallPlayerBird();
        }

        private void InstallPlayerBird()
        {
            Container.Bind<PlayerBirdStateFactory>().AsSingle();
            Container.BindFactory<PlayerBirdStateIdle, PlayerBirdStateIdle.Factory>().WhenInjectedInto<PlayerBirdStateFactory>();
            Container.BindFactory<PlayerBirdStateWalk, PlayerBirdStateWalk.Factory>().WhenInjectedInto<PlayerBirdStateFactory>();
            Container.BindFactory<PlayerBirdStateTakeOff, PlayerBirdStateTakeOff.Factory>().WhenInjectedInto<PlayerBirdStateFactory>();
            Container.BindFactory<PlayerBirdStateFly, PlayerBirdStateFly.Factory>().WhenInjectedInto<PlayerBirdStateFactory>();
            Container.BindFactory<PlayerBirdStateSoar, PlayerBirdStateSoar.Factory>().WhenInjectedInto<PlayerBirdStateFactory>();
            Container.Bind<PlayerBirdAnimator>().AsSingle();
        }
    }
}