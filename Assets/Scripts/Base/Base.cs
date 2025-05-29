using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneHarvesting
{
    public class Base : MonoBehaviour
    {
        [SerializeField] private Transform _droneSpawnPoint;

        public Vector3 DroneSpaenPointPosition { get => _droneSpawnPoint.transform.position; }

        public Vector3 Position { get => transform.position; }
    }
}
