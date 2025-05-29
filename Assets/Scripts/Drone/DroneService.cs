using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace DroneHarvesting
{
    public class DroneService : MonoBehaviour
    {
        [SerializeField] private Base _blueBase;
        [SerializeField] private Base _redBase;

        private List<Drone> _activeRedDrones = new List<Drone>();
        private List<Drone> _activeBlueDrones = new List<Drone>();

        private DronePool _dronePool;
        private SignalBus _signalBus;


        [Inject]
        public void Construct(DronePool dronePool, SignalBus signalBus)
        {
            _dronePool = dronePool;
            _signalBus = signalBus;
    }

        public void Init()
        {
            SetupDrones(_dronePool.NumTotal);
            _signalBus.Subscribe<DroneCountSignal>(SetupDrones);
        }

        private void SetupDrones(DroneCountSignal droneCountSignal)
        {
            SetupDrones(droneCountSignal.DronCount);
        }

        public void SetupDrones(int count)
        {
            int countPerTeam = count / 2;

            UpdateTeamDrones(DroneData.DroneTeam.Blue, countPerTeam, _activeBlueDrones);
            UpdateTeamDrones(DroneData.DroneTeam.Red, count - countPerTeam, _activeRedDrones);
        }

        private void UpdateTeamDrones(DroneData.DroneTeam droneTeam, int desiredCount, List<Drone> teamDrones)
        {

            while (teamDrones.Count < desiredCount)
            {
                Drone drone = _dronePool.Spawn();
                drone.Init(droneTeam, droneTeam == DroneData.DroneTeam.Blue ? _blueBase : _redBase);
                teamDrones.Add(drone);
            }

            // Despawn unnecessary
            while(teamDrones.Count > desiredCount)
            {
                Drone lastDrone = teamDrones.LastOrDefault();
                teamDrones.Remove(lastDrone);
                lastDrone.Despawn();
            }
        }
    }
}
