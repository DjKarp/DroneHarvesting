using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DroneHarvesting
{
    public class ResourceService : MonoBehaviour
    {
        [SerializeField] private Vector3 _spawnAreaMin;
        [SerializeField] private Vector3 _spawnAreaMax;

        private float _spawnInterval = 5.0f;
        private float _minSpawnDistance = 5.0f;

        private List<Resource> _resources = new List<Resource>();

        private ResourcePool _resourcePool;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(ResourcePool resourcePool, SignalBus signalBus)
        {
            _resourcePool = resourcePool;
            _signalBus = signalBus;
        }

        public void Init()
        {
            _signalBus.Subscribe<ResourcesGenerationTimeSignal>(ChangeSpawnInterval);

            StartCoroutine(SpawnResourced());
        }

        private IEnumerator SpawnResourced()
        {
            while (true)
            {
                Vector3 spawnPosition;
                float distanceToNearestResource = 0.0f;

                yield return new WaitForSeconds(_spawnInterval);

                do
                {
                    spawnPosition = new Vector3(Random.Range(_spawnAreaMin.x, _spawnAreaMax.x), 0.0f, Random.Range(_spawnAreaMin.z, _spawnAreaMax.z));
                    Resource nearestResource = GetNearestFreeResource(spawnPosition, true);

                    if (nearestResource != null)
                    {
                        distanceToNearestResource = Vector3.Distance(spawnPosition, nearestResource.Position);
                    }
                    else
                    {
                        break;
                    }
                }
                while (distanceToNearestResource < _minSpawnDistance);

                Resource resource = _resourcePool.Spawn();

                if (resource != null)
                {
                    resource.SetPosition(spawnPosition);
                    _resources.Add(resource);
                }
            }
        }

        public void RemoveResourceFromList(Resource resource)
        {
            _resources.Remove(resource);
        }

        public Resource GetNearestFreeResource(Vector3 position, bool isNeedResourcedFree = false)
        {
            Resource closestResource = null;
            float minDistane = Mathf.Infinity;

            foreach (Resource resource in _resources)
            {
                if (isNeedResourcedFree || resource.IsTaken == false)
                {
                    float tempDistance = Vector3.Distance(position, resource.Position);

                    if (tempDistance < minDistane)
                    {
                        minDistane = tempDistance;
                        closestResource = resource;
                    }
                }
            }

            return closestResource;
        }

        private void ChangeSpawnInterval(ResourcesGenerationTimeSignal generationTimeSignal)
        {
            _spawnInterval = Mathf.Clamp(generationTimeSignal.GenerationTime, 1.0f, 100.0f);
        }
    }
}
