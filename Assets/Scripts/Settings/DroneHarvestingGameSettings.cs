using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneHarvesting
{
    [CreateAssetMenu(fileName = "DroneHarvestingGameSettings", menuName = "DroneHarvesting/GameSettings")]
    public class DroneHarvestingGameSettings : ScriptableObject
    {
        [Range(2, 20)]
        public int TotalDroneCount = 10;

        public GameObject DronePrefab;
        public GameObject ResourcePrefab;

        [Header("Base Team Blue and settings")]
        public Material BlueTeamMaterial;

        [Header("Base Team Red and settings")]
        public Material RedTeamMaterial;
    }
}
