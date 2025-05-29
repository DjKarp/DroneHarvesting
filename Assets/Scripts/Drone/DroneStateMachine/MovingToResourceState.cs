using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneHarvesting
{
    public class MovingToResourceState : IDroneState
    {
        private Drone _currentDrone;

        private float _stopDistanceToResource = 2.0f;

        public void EnterState(Drone drone)
        {
            _currentDrone = drone;

            Debug.Log("Enter on MovingToResourceState " + _currentDrone.name);
        }

        public void UpdateState()
        {
            if (_currentDrone._currentTargetResource == null)
            {
                _currentDrone.ChangeState(new SearchingState());
            }
            else
            {
                float tempDistance = Vector3.Distance(_currentDrone.Position, _currentDrone._currentTargetResource.Position);

                if (tempDistance < _stopDistanceToResource)
                {
                    _currentDrone.ChangeState(new HarvestingState());
                }
            }
        }

        public void ExitState()
        {
            Debug.Log("Exit from MovingToResourceState " + _currentDrone.name);
        }
    }
}
