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

            _currentDrone.DroneStateUI.SetStateText("Moving To Resource");
            _currentDrone.DroneStateUI.SetColor(Color.green);
        }

        public void UpdateState()
        {
            if (_currentDrone.CurrentTargetResource == null)
            {
                _currentDrone.ChangeState(new SearchingState());
            }
            else
            {
                float tempDistance = Vector3.Distance(_currentDrone.Position, _currentDrone.CurrentTargetResource.Position);

                if (tempDistance < _stopDistanceToResource)
                {
                    _currentDrone.ChangeState(new HarvestingState());
                }
            }
        }
    }
}
