using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace DroneHarvesting
{
    public class DroneMovement : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private Base _homeBase;

        public SignalBus SignalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            SignalBus = signalBus;
        }

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            SignalBus.Subscribe<DroneSpeedSignal>(ChangeSpeed);
        }

        public void Init(Base homeBase)
        {
            _homeBase = homeBase;
        }

        public void SetHomeBaseDestination()
        {
            SetTargetDestination(_homeBase.GetNextDroneUnloadPointPosition());
        }

        public void SetTargetDestination(Vector3 position)
        {
            _navMeshAgent.SetDestination(position);
        }

        public void StopMovement()
        {
            _navMeshAgent.isStopped = true;
        }

        public void ResumeMovement()
        {
            _navMeshAgent.isStopped = false;
        }

        private void ChangeSpeed(DroneSpeedSignal droneSpeedSignal)
        {
            _navMeshAgent.speed = droneSpeedSignal.DroneSpeed;
        }

        private void OnDisable()
        {
            if (SignalBus != null)
                SignalBus.TryUnsubscribe<DroneSpeedSignal>(ChangeSpeed);
        }
    }
}
