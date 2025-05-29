using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneHarvesting
{
    public class Base : MonoBehaviour
    {
        [SerializeField] private Transform _droneSpawnPoint;

        public Vector3 DroneSpawnPointPosition { get => _droneSpawnPoint.transform.position; }

        [SerializeField] private Transform _droneUnloadPoint;

        public Vector3 DroneUnloadPointPosition { get => _droneUnloadPoint.transform.position; }

        public Vector3 Position { get => transform.position; }
    }
}
