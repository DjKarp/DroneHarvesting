using UnityEngine;
using Zenject;

namespace DroneHarvesting
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private ResourceService _resourceService;
        [SerializeField] private DroneSpawnService _droneSpawnService;

        [SerializeField] private DroneHarvestingGameSettings _gameSettings;
        

        public override void InstallBindings()
        {
            BindDrone();
            BindResource();
            BindGameSettings();
        }

        private void BindDrone()
        {
            Container
                .Bind<DroneSpawnService>()
                .FromInstance(_droneSpawnService)
                .AsSingle();

            Container
                .BindMemoryPool<Drone, DronePool>()
                .WithInitialSize(_gameSettings.TotalDroneCount)
                .FromComponentInNewPrefab(_gameSettings.DronePrefab)
                .UnderTransform(_droneSpawnService.gameObject.transform);
        }

        private void BindResource()
        {
            Container
                .Bind<ResourceService>()
                .FromInstance(_resourceService)
                .AsSingle();

            Container
                .BindMemoryPool<Resource, ResourcePool>()
                .WithInitialSize(20)
                .FromComponentInNewPrefab(_gameSettings.ResourcePrefab)
                .UnderTransform(_resourceService.gameObject.transform);
        }

        private void BindGameSettings()
        {
            Container
                .BindInstance(_gameSettings)
                .AsSingle();
        }
    }
}