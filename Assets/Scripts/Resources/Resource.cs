using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneHarvesting
{
    public class Resource : MonoBehaviour
    {
        private bool _isTaken = false;

        public bool IsTaken { get => _isTaken; set => _isTaken = value; }

        public Vector3 Position { get => transform.position; }
    }
}
