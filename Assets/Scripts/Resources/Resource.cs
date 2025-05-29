using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DroneHarvesting
{
    public class Resource : MonoBehaviour
    {
        private bool _isTaken = false;

        public bool IsTaken { get => _isTaken; set => _isTaken = value; }

        private Transform _transform;
        public Vector3 Position { get => _transform.position; }

        private ResourceService _resourceService;
        private ResourcePool _resourcePool;

        [Inject]
        public void Construct(ResourceService resourceService, ResourcePool resourcePool)
        {
            _resourceService = resourceService;
            _resourcePool = resourcePool;
        }

        private void Awake()
        {
            _transform = gameObject.transform;
        }

        public void Despawn()
        {
            _isTaken = false;
            _resourceService.RemoveResourceFromList(this);
            _resourcePool.Despawn(this);
        }

        public void SetPosition(Vector3 position)
        {
            _transform.position = position;
        }
    }
}
