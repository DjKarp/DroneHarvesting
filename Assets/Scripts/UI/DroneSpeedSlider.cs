using UnityEngine;
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
