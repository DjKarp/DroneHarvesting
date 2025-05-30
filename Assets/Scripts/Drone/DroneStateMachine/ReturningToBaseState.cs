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
            _currentDrone.DroneMovement.SetHomeBaseDestination();

            _currentDrone.DroneStateUI.SetStateText("Returning To Base");
            _currentDrone.DroneStateUI.SetColor(Color.magenta);
        }

        public void UpdateState()
        {
            float tempDistance = Vector3.Distance(_currentDrone.Position, _currentDrone.CurrentBaseUnloadingPosition);

            if (tempDistance < _stopDistanceToBase)
            {
                _currentDrone.ChangeState(new UnloadingState());
            }
        }
    }
}
