using UnityEngine;
using Zenject;
using TMPro;

namespace DroneHarvesting
{
    public class DroneSpeedSlider : SliderUI
    {
        private TextMeshProUGUI _textMesh;

        private void Start()
        {
            _textMesh = GetComponentInChildren<TextMeshProUGUI>();
            SetTextSpeedValue(4);
        }

        protected override void SliderChangedValue(float newSpeedValue)
        {
            SetTextSpeedValue(newSpeedValue);
            SignalBus.Fire(new DroneSpeedSignal(Mathf.RoundToInt(newSpeedValue)));
        }

        private void SetTextSpeedValue(float speedValue)
        {
            _textMesh.text = "Drone Speed = " + speedValue.ToString();
        }
    }
}
