using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DroneHarvesting
{
    public class DroneSpeedSlider : SliderUI
    {
        protected override void SliderChangedValue(float newSpeedValue)
        {
            SignalBus.Fire(new DroneSpeedSignal(Mathf.RoundToInt(newSpeedValue)));
        }
    }
}
