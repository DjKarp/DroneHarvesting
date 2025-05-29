using UnityEngine;

namespace DroneHarvesting
{
    public class ClickEscToExit : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
