using UnityEngine;

namespace DroneHarvesting
{
    public class Base : MonoBehaviour
    {
        [SerializeField] private Transform _droneSpawnPoint;

        public Vector3 DroneSpawnPointPosition { get => _droneSpawnPoint.transform.position; }

        [SerializeField] private Transform[] _droneUnloadPoints;              

        public Vector3 Position { get => transform.position; }

        private int _currentUnloadPosition = -1;

        public Vector3 GetNextDroneUnloadPointPosition()
        {
            if (_currentUnloadPosition < _droneUnloadPoints.Length - 1) 
                _currentUnloadPosition++;
            else
                _currentUnloadPosition = 0;

            return _droneUnloadPoints[_currentUnloadPosition].transform.position; 
        }
    }
}
