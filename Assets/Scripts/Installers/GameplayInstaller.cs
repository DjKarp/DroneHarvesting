using UnityEngine;
using Zenject;

namespace DroneHarvesting
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private ResourceService _resourceService;
        [SerializeField] private GameObject _resourcePrefab;

        public override void InstallBindings()
        {
            BindResource();
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