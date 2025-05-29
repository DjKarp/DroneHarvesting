using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DroneHarvesting
{
    public class Drone : MonoBehaviour
    {
        public NavMeshAgent _navMeshAgent;
        public Base _homeBase;
        public Resource _currentTargetResource;
        public float _harvestTime = 5f;

        public Vector3 Position { get => transform.position; }

        private IDroneState _currentDronState;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            ChangeState(new SearchingState());
        }

        private void Update()
        {
            _currentDronState?.UpdateState();
        }

        public void ChangeState(IDroneState newDroneState)
        {
            _currentDronState?.ExitState();
            _currentDronState = newDroneState;
            _currentDronState?.EnterState(this);
        }

        public void StartHarvesting()
        {
            StartCoroutine(Harvesting());
        }

        private IEnumerator Harvesting()
        {
            _navMeshAgent.isStopped = true;

            yield return new WaitForSeconds(_harvestTime);

            if (_currentTargetResource != null)
                _currentTargetResource.Despawn();

            _currentTargetResource = null;
            _navMeshAgent.isStopped = false;
            ChangeState(new ReturningToBaseState());
        }

        public void StartUnloading()
        {
            StartCoroutine(Unloading());
        }

        private IEnumerator Unloading()
        {
            _navMeshAgent.isStopped = true;

            yield return new WaitForSeconds(_harvestTime / 2.0f);

            _navMeshAgent.isStopped = false;
            ChangeState(new SearchingState());
        }
    }
}
