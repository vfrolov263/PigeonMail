using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class GameSignalsInstaller : Installer<GameSignalsInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<LetterStatusChangedSignal>();
            Container.DeclareSignal<TrackableLetterStatusChangedSignal>();
            Container.DeclareSignal<NextLessonSignal>();
            Container.DeclareSignal<OutOfTimeSignal>();
            Container.DeclareSignal<CityDamageSignal>();
            Container.DeclareSignal<CityReceivedLetterSignal>();
        }
    }
}