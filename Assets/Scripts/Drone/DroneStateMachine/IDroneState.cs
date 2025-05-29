using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneHarvesting
{
    public interface IDroneState
    {
        void EnterState(Drone drone);
        void UpdateState();
        void ExitState();
    }
}
