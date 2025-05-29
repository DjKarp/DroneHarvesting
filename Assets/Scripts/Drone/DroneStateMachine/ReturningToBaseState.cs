using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneHarvesting
{
    public class ReturningToBaseState : IDroneState
    {
        private Drone _currentDrone;

        private float _stopDistanceToBase = 3.0f;

        public void EnterState(Drone drone)
        {
            _currentDrone = drone;
            _currentDrone.SetHomeBaseDestination();
        }

        public void UpdateState()
        {
            float tempDistance = Vector3.Distance(_currentDrone.Position, _currentDrone.CurrentBasePosition);

            if (tempDistance < _stopDistanceToBase)
            {
                _currentDrone.ChangeState(new UnloadingState());
            }
        }

        public void ExitState()
        {
            Debug.Log("Exit from ReturningToBaseState " + _currentDrone.name);
        }
    }
}
