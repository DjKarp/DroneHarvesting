using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace DroneHarvesting
{
    public class ShowDronePathService : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private NavMeshAgent _navMeshAgent;
        private DroneView _droneView;

        private bool _isPathShow = true;

        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _droneView = GetComponentInChildren<DroneView>();

            _lineRenderer = GetComponentInChildren<LineRenderer>();
            _lineRenderer.material = _droneView.GetTeamMaterial();
            ResetLine();

            _signalBus.Subscribe<DroneShowHidePathSignal>(ChangeShowDronePath);
        }

        private void Update()
        {
            if (_isPathShow) 
                UpdatePathLine();
        }

        private void UpdatePathLine()
        {
            if (_navMeshAgent.hasPath)
            {
                _lineRenderer.positionCount = _navMeshAgent.path.corners.Length;
                _lineRenderer.SetPositions(_navMeshAgent.path.corners);
            }
            else
            {
                ResetLine();
            }
        }

        private void ResetLine()
        {
            _lineRenderer.positionCount = 0;
        }

        private void ChangeShowDronePath(DroneShowHidePathSignal hidePathSignal)
        {
            _isPathShow = hidePathSignal.IsShowPath;

            if (_isPathShow == false)
                ResetLine();
        }
    }
}
