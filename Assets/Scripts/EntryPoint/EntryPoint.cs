using System.Collections;
using UnityEngine;
using Zenject;

namespace DroneHarvesting
{
    public class EntryPoint : MonoBehaviour
    {
        private ResourceService _resourceService;
        private DroneService _droneService;

        [Inject]
        public void Construct(ResourceService resourceService, DroneService droneService)
        {
            _resourceService = resourceService;
            _droneService = droneService;
        }

        private void Start()
        {
            StartCoroutine(StartingGameService());
        }

        private IEnumerator StartingGameService()
        {
            _resourceService.Init();

            yield return new WaitForEndOfFrame();

            _droneService.Init();
        }
    }
}
