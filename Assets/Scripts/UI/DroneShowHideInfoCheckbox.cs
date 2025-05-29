using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DroneHarvesting
{
    public class DroneShowHideInfoCheckbox : MonoBehaviour
    {
        private Toggle _toggle;

        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _toggle = GetComponentInChildren<Toggle>();
            _toggle.onValueChanged.AddListener(_ => ChangeToggle(_));
        }

        private void ChangeToggle(bool value)
        {
            _signalBus.Fire(new DroneShowHideInfoSignal(value));
        }
    }
}
