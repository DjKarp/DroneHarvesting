using Zenject;

namespace DroneHarvesting
{
    public class ResourcePool : MonoMemoryPool<Resource>
    {
        protected override void OnSpawned(Resource item)
        {
            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(Resource item)
        {
            item.gameObject.SetActive(false);
        }
    }
}
