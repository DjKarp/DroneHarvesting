using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneHarvesting
{
    public class CameraController : MonoBehaviour
    {
        public float _moveSpeed = 50f;
        public float _zoomSpeed = 55f;
        public float _rotationSpeed = 0.1f;

        void Update()
        {

            transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * _moveSpeed * Time.deltaTime;

            transform.position += transform.forward * Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed;

            if (Input.GetMouseButton(1))
            {
                transform.RotateAround(Vector3.zero, Vector3.up, Input.GetAxis("Mouse X") * _rotationSpeed);
            }
        }
    }
}
