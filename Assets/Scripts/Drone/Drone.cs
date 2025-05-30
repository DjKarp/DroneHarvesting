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

        public Resource CurrentTargetResource { get; set; }

        private Base _homeBase;
        public Vector3 CurrentBaseUnloadingPosition { get => _homeBase.GetNextDroneUnloadPointPosition(); }
                
        public ResourceService ResourceService { get; private set; }

        public DroneStateUI DroneStateUI { get; private set; }

        public DroneMovement DroneMovement { get; private set; }


        private IDroneState _currentDronState;
        public DroneData.DroneTeam CurrentDroneTeam { get; private set; }
        private DroneView _droneView;
        private DronePool _dronePool;

        public SignalBus SignalBus { get; private set; }
        public UnloadingFXPool UnloadingFXPool { get; private set; }


        [Inject]
        public void Construct(DronePool dronePool, SignalBus signalBus, UnloadingFXPool unloadingFXPool, ResourceService resourceService)
        {
            _dronePool = dronePool;
            SignalBus = signalBus;
            UnloadingFXPool = unloadingFXPool;
            ResourceService = resourceService;
        }

        private void Awake()
        {
            _transform = gameObject.transform;
            _droneView = GetComponentInChildren<DroneView>();
            DroneStateUI = GetComponentInChildren<DroneStateUI>();
            DroneMovement = GetComponent<DroneMovement>();
        }

        public void Init(DroneData.DroneTeam droneTeam, Base homeBase)
        {
            CurrentDroneTeam = droneTeam;
            _droneView.SetNewMaterialOnTeam(CurrentDroneTeam);

            _homeBase = homeBase;
            DroneMovement.Init(_homeBase);

            _transform.position = _homeBase.DroneSpawnPointPosition + (Random.insideUnitSphere * 4.0f);

            ChangeState(new SearchingState());
        }

        public void Despawn()
        {
            _homeBase = null;

            if (CurrentTargetResource != null)
            {
                CurrentTargetResource.IsTaken = false;
                CurrentTargetResource = null;
            }

            _dronePool.Despawn(this);
        }

        private void Update()
        {
            _currentDronState?.UpdateState();
        }

        public void ChangeState(IDroneState newDroneState)
        {
            _currentDronState = newDroneState;
            _currentDronState?.EnterState(this);
        }
    }
}
