using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DroneHarvesting
{
    public class DronCountSlider : SliderUI
    {
        protected override void SliderChangedValue(float newSpeedValue)
        {
            SignalBus.Fire(new DroneCountSignal(Mathf.RoundToInt(newSpeedValue)));
        }
    }
}
