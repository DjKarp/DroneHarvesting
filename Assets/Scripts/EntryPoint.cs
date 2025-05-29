using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DroneHarvesting
{
    public class EntryPoint : MonoBehaviour
    {
        private ResourceService _resourceService;

        [Inject]
        public void Construct(ResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        private void Start()
        {
            _resourceService.Init();
        }
    }
}
