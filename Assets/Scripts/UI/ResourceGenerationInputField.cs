using UnityEngine;
using Zenject;
using TMPro;

namespace DroneHarvesting
{
    public class ResourceGenerationInputField : MonoBehaviour
    {
        private TMP_InputField _inputField;

        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Start()
        {
            _inputField = GetComponentInChildren<TMP_InputField>();
            _inputField.onValueChanged.AddListener(_ => NewValueOnInputField(_));
        }

        private void NewValueOnInputField(string text)
        {
            float parsedValue;

            if (float.TryParse(text, out parsedValue))
            {
                _signalBus.Fire(new ResourcesGenerationTimeSignal(parsedValue));
            }
        }
    }
}
