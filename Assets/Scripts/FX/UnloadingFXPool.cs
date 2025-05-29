using UnityEngine;
using Zenject;

namespace DroneHarvesting
{
    public class UnloadingFXPool : MonoMemoryPool<Vector3, UnloadingFX>
    {
        protected override void Reinitialize(Vector3 position, UnloadingFX item)
        {
            item.PlayEffect(position);
        }
    }
}
