using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;

namespace DroneHarvesting
{
    public class TeamResourcesCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textCounter;
        [SerializeField] private DroneData.DroneTeam _currentDroneTeam;

        private int _counter = 0;

        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Start()
        {
            _signalBus.Subscribe<UnloadResourceSignal>(AddedCount);
        }

        private void AddedCount(UnloadResourceSignal unloadResourceSignal)
        {
            if (_currentDroneTeam == unloadResourceSignal.DroneTeamResource)
            {
                _counter++;
                SetText();
            }
        }

        private void SetText()
        {
            _textCounter.text = _counter.ToString();
        }
    }
}
