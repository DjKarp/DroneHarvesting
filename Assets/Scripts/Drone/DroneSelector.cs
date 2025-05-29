using UnityEngine;

namespace DroneHarvesting
{
    public class DroneSelector : MonoBehaviour
    {
        [SerializeField] private LayerMask _droneLayer;
        private SelectedDroneView _selectedDroneView;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TrySelectedDrone();
            }
        }

        private void TrySelectedDrone()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, 100.0f, _droneLayer))
            {
                SelectedDroneView selectedDroneView = raycastHit.collider.GetComponentInParent<SelectedDroneView>();

                if (selectedDroneView != null)
                {
                    SelectDrone(selectedDroneView);
                }
            }
            else
            {
                DeselectDrone();
            }
        }

        private void SelectDrone(SelectedDroneView selectedDroneView)
        {
            DeselectDrone();
            _selectedDroneView = selectedDroneView;
            _selectedDroneView.ActivateOutline();
        }

        private void DeselectDrone()
        {
            if (_selectedDroneView != null)
            {
                _selectedDroneView.DeactivateOutline();
            }
        }
    }
}
