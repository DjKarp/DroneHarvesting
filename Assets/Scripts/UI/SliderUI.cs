using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DroneHarvesting
{
    public class SliderUI : MonoBehaviour
    {
        protected Slider Slider;

        protected SignalBus SignalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            SignalBus = signalBus;
        }

        private void Awake()
        {
            Slider = GetComponentInChildren<Slider>();
            Slider.onValueChanged.AddListener(_ => SliderChangedValue(_));
        }

        protected virtual void SliderChangedValue(float newSpeedValue)
        {
            
        }
    }
}
