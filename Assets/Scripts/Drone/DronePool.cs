using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DroneHarvesting
{
    public class DronePool : MonoMemoryPool<Drone>
    {
        protected override void OnSpawned(Drone item)
        {
            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(Drone item)
        {
            item.gameObject.SetActive(false);
        }
    }
}
