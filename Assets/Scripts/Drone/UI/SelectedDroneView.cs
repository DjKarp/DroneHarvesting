using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneHarvesting
{
    public class SelectedDroneView : MonoBehaviour
    {
        [SerializeField] private DroneStateUI _droneStateUI;
        private Outline _outline;

        private void Awake()
        {
            _outline = GetComponent<Outline>();
            DeactivateOutline();
        }

        public void ActivateOutline()
        {
            _outline.enabled = true;
            _droneStateUI.ActivateInfo(new DroneShowHideInfoSignal(true));
        }

        public void DeactivateOutline()
        {
            _outline.enabled = false;
            _droneStateUI.ActivateInfo(new DroneShowHideInfoSignal(false));
        }
    }
}
