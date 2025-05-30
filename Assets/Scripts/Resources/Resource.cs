using UnityEngine;
using Zenject;

namespace DroneHarvesting
{
    public class Resource : MonoBehaviour
    {
        public bool IsTaken { get; set; }
        public Vector3 Position { get => transform.position; }

        private ResourceService _resourceService;
        private ResourcePool _resourcePool;

        [Inject]
        public void Construct(ResourceService resourceService, ResourcePool resourcePool)
        {
            _resourceService = resourceService;
            _resourcePool = resourcePool;
        }

        public void Despawn()
        {
            IsTaken = false;
            _resourceService.RemoveResourceFromList(this);
            _resourcePool.Despawn(this);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}
