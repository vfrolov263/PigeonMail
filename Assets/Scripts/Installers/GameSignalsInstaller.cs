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
        }
    }
}