using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneHarvesting
{
    public class ResourceService : MonoBehaviour
    {
        [SerializeField] private List<Resource> _resources;

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
