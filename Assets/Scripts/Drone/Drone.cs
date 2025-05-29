using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace DroneHarvesting
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Drone : MonoBehaviour
    {
        private Transform _transform;
        public Transform Transform { get => _transform; }
        public Vector3 Position { get => _transform.position; set => _transform.position = value; }


        private Resource _currentTargetResource;
        public Resource CurrentTargetResource { get => _currentTargetResource; set => _currentTargetResource = value; }


        private Base _homeBase;
        public Vector3 CurrentBaseUnloadingPosition { get => _homeBase.DroneUnloadPointPosition; }


        private ResourceService _resourceService;
        public ResourceService ResourceService { get => _resourceService; }


        private DroneStateUI _droneStateUI;
        public DroneStateUI DroneStateUI { get => _droneStateUI; }


        private NavMeshAgent _navMeshAgent;
        public NavMeshAgent NavMeshAgent { get => _navMeshAgent; }

        
        private IDroneState _currentDronState;
        public DroneData.DroneTeam CurrentDroneTeam { get; private set; }
        private DroneView _droneView;
        private DronePool _dronePool;

        private Transform _cameraTransform;

        public SignalBus SignalBus { get; private set; }
        public UnloadingFXPool UnloadingFXPool { get; private set; }


        [Inject]
        public void Construct(DronePool dronePool, SignalBus signalBus, UnloadingFXPool unloadingFXPool, ResourceService resourceService)
        {
            _dronePool = dronePool;
            SignalBus = signalBus;
            UnloadingFXPool = unloadingFXPool;
            _resourceService = resourceService;
        }

        private void Awake()
        {
            _transform = gameObject.transform;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _droneView = GetComponentInChildren<DroneView>();
            _cameraTransform = Camera.main.transform;
            _droneStateUI = GetComponentInChildren<DroneStateUI>();
        }

        public void Init(DroneData.DroneTeam droneTeam, Base homeBase)
        {
            CurrentDroneTeam = droneTeam;
            _droneView.SetNewMaterialOnTeam(CurrentDroneTeam);
            _homeBase = homeBase;
            _transform.position = _homeBase.DroneSpawnPointPosition + (Random.insideUnitSphere * 4.0f);

            SignalBus.Subscribe<DroneSpeedSignal>(ChangeDronSpeed);

            _droneStateUI.SetLookTarget(_cameraTransform);

            ChangeState(new SearchingState());
        }

        public void Despawn()
        {
            _homeBase = null;

            if (_currentTargetResource != null)
            {
                _currentTargetResource.IsTaken = false;
                _currentTargetResource = null;
            }
            SignalBus.TryUnsubscribe<DroneSpeedSignal>(ChangeDronSpeed);

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
            SetTargetDestination(_homeBase.DroneUnloadPointPosition);
        }

        public void SetTargetDestination(Vector3 position)
        {
            _navMeshAgent.SetDestination(position);
        }

        private void ChangeDronSpeed(DroneSpeedSignal droneSpeedSignal)
        {
            _navMeshAgent.speed = droneSpeedSignal.DroneSpeed;
        }

        private void OnDisable()
        {
            if (SignalBus != null) 
                SignalBus.TryUnsubscribe<DroneSpeedSignal>(ChangeDronSpeed);
        }
    }
}
