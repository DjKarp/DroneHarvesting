using UnityEngine;
using Zenject;

namespace DroneHarvesting
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private ResourceService _resourceService;
        [SerializeField] private GameObject _resourcePrefab;

        [SerializeField] private GameObject _dronePrefab;
        [SerializeField] private DroneService _droneService;

        public override void InstallBindings()
        {
            BindDrone();
            BindResource();
        }

        private void BindDrone()
        {
            Container
                .Bind<DroneService>()
                .FromInstance(_droneService)
                .AsSingle();

            Container
                .BindMemoryPool<Drone, DronePool>()
                .WithInitialSize(10)
                .FromComponentInNewPrefab(_dronePrefab)
                .UnderTransform(_droneService.gameObject.transform);
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
                .FromComponentInNewPrefab(_resourcePrefab)
                .UnderTransform(_resourceService.gameObject.transform);
        }
    }
}