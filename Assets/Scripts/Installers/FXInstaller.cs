using UnityEngine;
using Zenject;

namespace DroneHarvesting
{
    public class FXInstaller : MonoInstaller
    {
        [SerializeField] private UnloadingFX _unloadingFXPrefab;
        [SerializeField] private Transform _parentTransform;

        public override void InstallBindings()
        {
            BindUnloadFX();
        }

        private void BindUnloadFX()
        {
            Container
                .BindMemoryPool<UnloadingFX, UnloadingFXPool>()
                .WithInitialSize(10)
                .FromComponentInNewPrefab(_unloadingFXPrefab)
                .UnderTransform(_parentTransform);
        }
    }
}