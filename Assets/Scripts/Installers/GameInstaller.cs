using System;
using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class GameInstaller : MonoInstaller
    {
        [Inject]
        Settings _settings = null;

        public override void InstallBindings()
        {
            InstallPlayerBird();
            InstallHuman();
            InstallMail();
            InstallSystems();
            GameSignalsInstaller.Install(Container);
        }

        private void InstallPlayerBird()
        {
            Container.Bind<PlayerBirdStateFactory>().AsSingle();
            Container.BindFactory<PlayerBirdStateIdle, PlayerBirdStateIdle.Factory>().WhenInjectedInto<PlayerBirdStateFactory>();
            Container.BindFactory<PlayerBirdStateWalk, PlayerBirdStateWalk.Factory>().WhenInjectedInto<PlayerBirdStateFactory>();
            Container.BindFactory<PlayerBirdStateTakeOff, PlayerBirdStateTakeOff.Factory>().WhenInjectedInto<PlayerBirdStateFactory>();
            Container.BindFactory<PlayerBirdStateFly, PlayerBirdStateFly.Factory>().WhenInjectedInto<PlayerBirdStateFactory>();
            Container.BindFactory<PlayerBirdStateSoar, PlayerBirdStateSoar.Factory>().WhenInjectedInto<PlayerBirdStateFactory>();
            Container.BindFactory<PlayerBirdStateFall, PlayerBirdStateFall.Factory>().WhenInjectedInto<PlayerBirdStateFactory>();
            Container.Bind<PlayerBirdAnimator>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerBirdLimiter>().AsSingle().NonLazy();
        }

        private void InstallHuman()
        {
           // Container.Bind<HumanStateFactory>().AsSingle();
           // Container.BindFactory<HumanStateStand, HumanStateStand.Factory>().WhenInjectedInto<HumanStateFactory>();
            //Container.BindFactory<HumanStateWalk, HumanStateWalk.Factory>().WhenInjectedInto<HumanStateFactory>();

           // Container.Bind<HumanStateFactory>().
           // Container.Bind(Human)
        }

        private void InstallMail()
        {
            Container.BindFactory<Transform, Transform, float, Letter, Letter.Factory>().
            FromPoolableMemoryPool<Transform, Transform, float, Letter, LetterPool>(poolBinder => poolBinder.
            FromComponentInNewPrefab(_settings.LetterPrefab));

            //Container.BindFactory<LetterReceiver, LetterReceiver.Factory>().FromNewComponentOnNewGameObject();
            Container.BindFactory<LetterReceiver, LetterReceiver.Factory>().FromComponentInNewPrefab(_settings.ReceiverPrefab);

           // Container.Bind<MailManager>().AsSingle();
        }

        private void InstallSystems()
        {
            Container.BindInterfacesAndSelfTo<AudioPlayer>().AsSingle();
            Container.Bind<TimeScaler>().AsSingle();
        }

        class LetterPool : MonoPoolableMemoryPool<Transform, Transform, float, IMemoryPool, Letter>
        {
        }

        [Serializable]
        public class Settings
        {
            public GameObject LetterPrefab;
            public GameObject ReceiverPrefab;
        }

    }
}