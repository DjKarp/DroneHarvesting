using UnityEngine;
using TMPro;
using Zenject;

namespace DroneHarvesting
{
    public class DroneStateUI : MonoBehaviour
    {
        [SerializeField] private GameObject _canvas;
        [SerializeField] private TextMeshProUGUI _stateText;
        private Transform _cameraTransform;

        private bool _isShowedInfo;

        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Start()
        {
            _signalBus.Subscribe<DroneShowHideInfoSignal>(ActivateInfo);
        }

        private void LateUpdate()
        {
            RotateToTarget();
        }

        public void SetStateText(string text)
        {
            _stateText.text = text;
        }

        public void SetColor(Color color)
        {
            _stateText.color = color;
        }

        public void SetLookTarget(Transform cameraTransform)
        {
            _cameraTransform = cameraTransform;
        }

        private void RotateToTarget()
        {
            if (_cameraTransform != null)
            {
                transform.rotation = Quaternion.LookRotation(transform.position - _cameraTransform.position);
            }
        }

        public void ActivateInfo(DroneShowHideInfoSignal droneShowHideInfoSignal)
        {
            _isShowedInfo = droneShowHideInfoSignal.IsShowInfo;            
            _canvas.gameObject.SetActive(_isShowedInfo);
        }
    }
}
