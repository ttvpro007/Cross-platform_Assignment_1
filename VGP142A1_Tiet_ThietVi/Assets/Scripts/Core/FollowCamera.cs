using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;

    // Update is called once per frame
    void LateUpdate()
    {
        if (!target)
        {
            target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }

        transform.position = target.position;
    }
}
