using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DroneHarvesting
{
    public class DroneView : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;
        private DroneData.DroneTeam _currentDroneTeam = DroneData.DroneTeam.Blue;
        private DroneHarvestingGameSettings _gameSettings;
        private Dictionary<DroneData.DroneTeam, Material> _droneTeamMaterials = new Dictionary<DroneData.DroneTeam, Material>();

        [Inject]
        public void Construct(DroneHarvestingGameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            
            _droneTeamMaterials.Add(DroneData.DroneTeam.Blue, _gameSettings.BlueTeamMaterial);             
            _droneTeamMaterials.Add(DroneData.DroneTeam.Red, _gameSettings.RedTeamMaterial);
        }

        public void SetNewMaterialOnTeam(DroneData.DroneTeam droneTeam)
        {
            _currentDroneTeam = droneTeam;
            _meshRenderer.material = GetTeamMaterial();
        }

        public Material GetTeamMaterial()
        {
            return _droneTeamMaterials[_currentDroneTeam];
        }
    }
}
