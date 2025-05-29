using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneHarvesting
{
    public class DroneView : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;

        private Dictionary<DroneData.DroneTeam, Material> _droneTeamMaterials = new Dictionary<DroneData.DroneTeam, Material>();

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();

            Material blueMaterial = Resources.Load<Material>("Prototype_512x512_Blue1");
            Material redMaterial = Resources.Load<Material>("Prototype_512x512_Purple");

            if (blueMaterial != null)
                _droneTeamMaterials.Add(DroneData.DroneTeam.Blue, blueMaterial);           

            if (redMaterial != null)
                _droneTeamMaterials.Add(DroneData.DroneTeam.Red, redMaterial);
        }

        public void SetNewMaterialOnTeam(DroneData.DroneTeam droneTeam)
        {
            _meshRenderer.material = _droneTeamMaterials[droneTeam];
        }
    }
}
