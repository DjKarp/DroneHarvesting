using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneHarvesting
{
    public class UnloadingState : IDroneState
    {
        private Drone _currentDrone;

        public void EnterState(Drone drone)
        {
            _currentDrone = drone;
            _currentDrone.StartUnloading();
        }

        public void UpdateState()
        {

        }

        public void ExitState()
        {
            Debug.Log("Exit from UnloadingState " + _currentDrone.name);
        }
    }
}
