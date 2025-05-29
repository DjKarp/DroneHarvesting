using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace DroneHarvesting
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Drone : MonoBehaviour
    {
        private Transform _transform;
        public Vector3 Position { get => _transform.position; set => _transform.position = value; }

        private Resource _currentTargetResource;
        public Resource CurrentTargetResource { get => _currentTargetResource; set => _currentTargetResource = value; }

        private Base _homeBase;
        public Vector3 CurrentBasePosition { get => _homeBase.Position; }

        private NavMeshAgent _navMeshAgent;        
        private float _harvestTime = 5f;

        private IDroneState _currentDronState;
        private DroneData.DroneTeam _currentDroneTeam;
        private DroneView _droneView;
        private DronePool _dronePool;


        [Inject]
        public void Construct(DronePool dronePool)
        {
            _dronePool = dronePool;
        }

        private void Awake()
        {
            _transform = gameObject.transform;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _droneView = GetComponentInChildren<DroneView>();
        }

        public void Init(DroneData.DroneTeam droneTeam, Base homeBase)
        {
            _currentDroneTeam = droneTeam;
            _droneView.SetNewMaterialOnTeam(_currentDroneTeam);
            _homeBase = homeBase;
            _transform.position = _homeBase.DroneSpawnPointPosition + (Random.insideUnitSphere * 4.0f);

            ChangeState(new SearchingState());
        }

        public void Despawn()
        {
            _homeBase = null;
            _currentTargetResource.IsTaken = false;
            _currentTargetResource = null;
            _dronePool.Despawn(this);
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

        public void SetHomeBaseDestination()
        {
            SetTargetDestination(_homeBase.Position);
        }

        public void SetTargetDestination(Vector3 position)
        {
            _navMeshAgent.SetDestination(position);
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
