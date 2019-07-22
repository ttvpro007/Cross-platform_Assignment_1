using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] float rotation_speed;

        void Start()
        {
            if (!target)
            {
                target = GameObject.FindWithTag("Player").GetComponent<Transform>();
            }

            if (rotation_speed == 0)
            {
                rotation_speed = 2.0f;
            }
        }

        void LateUpdate()
        {
            if (Input.GetMouseButton(2))
            {
                RotateAroundYAxis();
            }

            FollowTarget();
        }

        void RotateAroundYAxis()
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * rotation_speed, 0, Space.World);
        }

        void FollowTarget()
        {
            transform.position = target.position;
        }
    }
}