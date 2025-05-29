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

        [Inject]
        public void Construct(ResourcePool resourcePool)
        {
            _resourcePool = resourcePool;
        }

        public void Init()
        {
            StartCoroutine(SpawnResourced());
        }

        private IEnumerator SpawnResourced()
        {
            while (true)
            {
                Vector3 spawnPosition;
                //do
                {
                    spawnPosition = new Vector3(Random.Range(_spawnAreaMin.x, _spawnAreaMax.x), 0.0f, Random.Range(_spawnAreaMin.z, _spawnAreaMax.z));
                }
                //while ();
                Resource resource = _resourcePool.Spawn();

                if (resource != null)
                {
                    resource.SetPosition(spawnPosition);
                    _resources.Add(resource);
                }

                yield return new WaitForSeconds(_spawnInterval);
            }
        }

        public void RemoveResourceFromList(Resource resource)
        {
            _resources.Remove(resource);
        }

        public Resource GetNearestFreeResource(Vector3 dronPosition)
        {
            Resource closestResource = null;
            float minDistane = Mathf.Infinity;

            foreach (Resource resource in _resources)
            {
                if (resource.IsTaken == false)
                {
                    float tempDistance = Vector3.Distance(dronPosition, resource.Position);

                    if (tempDistance < minDistane)
                    {
                        minDistane = tempDistance;
                        closestResource = resource;
                    }
                }
            }

            return closestResource;
        }
    }
}
