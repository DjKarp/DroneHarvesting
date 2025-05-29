using System.Collections;
using UnityEngine;

namespace DroneHarvesting
{
    public class UnloadingFX : MonoBehaviour
    {
        private float _lifeTime = 1.5f;

        public void PlayEffect(Vector3 position)
        {
            transform.position = position;
            gameObject.SetActive(true);

            StartCoroutine(DeactivateEffect());
        }

        private IEnumerator DeactivateEffect()
        {
            yield return new WaitForSeconds(_lifeTime);

            gameObject.SetActive(false);
        }
    }
}
