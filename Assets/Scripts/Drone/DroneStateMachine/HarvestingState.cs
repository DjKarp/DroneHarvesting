using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace DroneHarvesting
{
    public class HarvestingState : IDroneState
    {
        private Drone _currentDrone;
        private float _harvestTime = 2.0f;
        private Tween _tween;

        public void EnterState(Drone drone)
        {
            _currentDrone = drone;

            _currentDrone.StartCoroutine(Harvesting());

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

        private IEnumerator Harvesting()
        {
            _currentDrone.NavMeshAgent.isStopped = true;
            _tween = _currentDrone.Transform.DOShakeScale(_harvestTime, strength: 0.5f, vibrato: 5);

            yield return new WaitForSeconds(_harvestTime);

            if (_currentDrone.CurrentTargetResource != null)
                _currentDrone.CurrentTargetResource.Despawn();

            _currentDrone.CurrentTargetResource = null;
            _currentDrone.NavMeshAgent.isStopped = false;
            _currentDrone.ChangeState(new ReturningToBaseState());
        }

        private void OnDestroy()
        {
            _tween.Kill(true);
        }
    }
}
