using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] float xDegreeMin;
        [SerializeField] float xDegreeMax;
        [SerializeField] float zDegreeMin;
        [SerializeField] float zDegreeMax;
        [SerializeField] float rotationSpeed;

        private void Start()
        {
            if (!target)
            {
                target = GameObject.FindWithTag("Player").GetComponent<Transform>();
            }

            if (xDegreeMin == 0)
            {
                xDegreeMin = 15.0f;
            }

            if (xDegreeMax == 0)
            {
                xDegreeMax = 90.0f;
            }

            if (zDegreeMin == 0)
            {
                zDegreeMin = 15.0f;
            }

            if (zDegreeMax == 0)
            {
                zDegreeMax = 90.0f;
            }

            if (rotationSpeed == 0)
            {
                rotationSpeed = 2.0f;
            }
        }

        private void LateUpdate()
        {
            if (PauseMenu.GameIsPaused) return;

            if (Input.GetMouseButton(1))
            {
                RotateAroundYAxis();
                //RotateAroundXAxis();
                //RotateAroundZAxis();
            }

            //FollowTarget();
        }

        private void RotateAroundYAxis()
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * rotationSpeed, 0, Space.World);
        }

        private void RotateAroundXAxis()
        {
            float x = Mathf.Clamp(Input.GetAxis("Mouse Y") * rotationSpeed, xDegreeMin, xDegreeMax);
            transform.Rotate(x, 0, 0, Space.World);
        }

        private void RotateAroundZAxis()
        {
            float z = Mathf.Clamp(Input.GetAxis("Mouse Y") * rotationSpeed, zDegreeMin, zDegreeMax);
            transform.Rotate(0, 0, z, Space.World);
        }

        private void FollowTarget()
        {
            transform.position = target.position;
        }
    }
}