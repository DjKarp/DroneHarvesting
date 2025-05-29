using UnityEngine;
using Zenject;

namespace DroneHarvesting
{
    public class SignalsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            BindHarvestResourceSignal();
            BindUISignals();
        }

        private void BindHarvestResourceSignal()
        {
            Container
                .DeclareSignal<UnloadResourceSignal>()
                .OptionalSubscriber();
        }

        private void BindUISignals()
        {
            Container
                .DeclareSignal<DroneCountSignal>()
                .OptionalSubscriber();

            Container
                .DeclareSignal<DroneSpeedSignal>()
                .OptionalSubscriber();

            Container
                .DeclareSignal<ResourcesGenerationTimeSignal>()
                .OptionalSubscriber();

            Container
                .DeclareSignal<DroneShowHidePathSignal>()
                .OptionalSubscriber();
        }
    }
}