using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DroneHarvesting
{
    public class SliderUI : MonoBehaviour
    {
        private Slider _slider;

        protected SignalBus SignalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            SignalBus = signalBus;
        }

        private void Awake()
        {
            _slider = GetComponentInChildren<Slider>();
            _slider.onValueChanged.AddListener(_ => SliderChangedValue(_));
        }

        protected virtual void SliderChangedValue(float newSpeedValue)
        {
            
        }
    }
}
