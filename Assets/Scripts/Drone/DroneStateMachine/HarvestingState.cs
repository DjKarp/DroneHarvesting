using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneHarvesting
{
    public class HarvestingState : IDroneState
    {
        private Drone _currentDrone;

        public void EnterState(Drone drone)
        {
            _currentDrone = drone;
            _currentDrone.StartHarvesting();
        }

        public void UpdateState()
        {

        }

        public void ExitState()
        {
            Debug.Log("Exit from HarvestingState " + _currentDrone.name);
        }
    }
}
