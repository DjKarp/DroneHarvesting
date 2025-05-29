using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DroneHarvesting
{
    public class DronCountSlider : SliderUI
    {
        private DroneHarvestingGameSettings _gameSettings;

        [Inject]
        public void Construct(DroneHarvestingGameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        private void Start()
        {
            Slider.maxValue = _gameSettings.TotalDroneCount;
            Slider.value = Mathf.Round(_gameSettings.TotalDroneCount / 2);
        }

        protected override void SliderChangedValue(float newSpeedValue)
        {
            SignalBus.Fire(new DroneCountSignal(Mathf.RoundToInt(newSpeedValue)));
        }
    }
}
