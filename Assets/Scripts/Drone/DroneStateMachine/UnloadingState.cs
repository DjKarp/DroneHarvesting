using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace DroneHarvesting
{
    public class UnloadingState : IDroneState
    {
        private Drone _currentDrone;

        private Tween _tween;

        public void EnterState(Drone drone)
        {
            _currentDrone = drone;
            _currentDrone.StartCoroutine(Unloading());

            _currentDrone.DroneStateUI.SetStateText("Unloading Resource");
            _currentDrone.DroneStateUI.SetColor(Color.red);
        }

        public void UpdateState()
        {

        }

        private IEnumerator Unloading()
        {
            _currentDrone.DroneMovement.StopMovement();            

            yield return new WaitForSeconds(1.0f);

            _tween = _currentDrone.Transform.DOPunchScale(_currentDrone.Transform.localScale * 1.1f, 1.0f, vibrato: 1);
            _currentDrone.UnloadingFXPool.Spawn(_currentDrone.Position);
            _currentDrone.SignalBus.Fire(new UnloadResourceSignal(_currentDrone.CurrentDroneTeam));
            _currentDrone.DroneMovement.ResumeMovement();
            _currentDrone.ChangeState(new SearchingState());
        }

        private void OnDestroy()
        {
            _tween.Kill(true);
        }
    }
}
