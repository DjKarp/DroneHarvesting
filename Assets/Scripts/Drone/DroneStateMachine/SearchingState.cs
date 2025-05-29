using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneHarvesting
{
    public class SearchingState : IDroneState
    {
        private Drone _currentDrone;
        private ResourceService _resourceService;

        public void EnterState(Drone drone)
        {
            _currentDrone = drone;
            _resourceService = _currentDrone.ResourceService;

            _currentDrone.DroneStateUI.SetStateText("Searching");
            _currentDrone.DroneStateUI.SetColor(Color.white);
        }        

        public void UpdateState()
        {
            Resource targetResource = _resourceService.GetNearestFreeResource(_currentDrone.Position);

            if (targetResource != null)
            {
                targetResource.IsTaken = true;

                _currentDrone.CurrentTargetResource = targetResource;
                _currentDrone.SetTargetDestination(targetResource.Position);
                _currentDrone.ChangeState(new MovingToResourceState());
            }
        }

        public void ExitState()
        {
            _currentDrone.DroneStateUI.SetStateText("");
        }
    }
}
