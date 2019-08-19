using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointProperty : MonoBehaviour
{
    const float waypointGizmoRadius = 0.3f;

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, waypointGizmoRadius);
    }
}
