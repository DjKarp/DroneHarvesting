using UnityEngine;
using Zenject;
using TMPro;

namespace DroneHarvesting
{
    public class ResourceGenerationInputField : MonoBehaviour
    {
        private TMP_InputField _inputField;
        private TextMeshProUGUI _textMesh;

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
            _textMesh = GetComponentInChildren<TextMeshProUGUI>();
            SetSpawnIntervalText(2);
        }

        private void NewValueOnInputField(string text)
        {
            float parsedValue;

            if (float.TryParse(text, out parsedValue))
            {
                parsedValue = Mathf.Clamp(parsedValue, 0.5f, 100.0f);
                SetSpawnIntervalText(parsedValue);
                _signalBus.Fire(new ResourcesGenerationTimeSignal(parsedValue));
            }
        }

        private void SetSpawnIntervalText(float spawnInterval)
        {
            _textMesh.text = "Resource Generation Time = " + spawnInterval.ToString();
        }
    }
}
