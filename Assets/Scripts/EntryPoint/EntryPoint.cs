using System.Collections;
using UnityEngine;
using Zenject;

namespace DroneHarvesting
{
    public class EntryPoint : MonoBehaviour
    {
        private ResourceService _resourceService;
        private DroneSpawnService _droneSpawnService;

        [Inject]
        public void Construct(ResourceService resourceService, DroneSpawnService droneService)
        {
            _resourceService = resourceService;
            _droneSpawnService = droneService;
        }

        private void Start()
        {
            StartCoroutine(StartingGameService());
        }

        private IEnumerator StartingGameService()
        {
            _resourceService.Init();

            yield return new WaitForEndOfFrame();

            _droneSpawnService.Init();
        }
    }
}
