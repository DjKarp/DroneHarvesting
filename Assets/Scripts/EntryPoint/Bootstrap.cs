using UnityEngine;
using UnityEngine.SceneManagement;

namespace CafeOfFear
{
    public class Bootstrap : MonoBehaviour
    {
        private const string GameplaySceneName = "Gameplay";

        private void Start()
        {
            SceneManager.LoadScene(GameplaySceneName);
        }
    }
}
