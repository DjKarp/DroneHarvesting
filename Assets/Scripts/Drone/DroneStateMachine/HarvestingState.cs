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

            _currentDrone.DroneStateUI.SetStateText("Harvesting Resource");
            _currentDrone.DroneStateUI.SetColor(Color.yellow);
        }

        public void UpdateState()
        {

        }

        public void ExitState()
        {
            _currentDrone.DroneStateUI.SetStateText("");
        }
    }
}
