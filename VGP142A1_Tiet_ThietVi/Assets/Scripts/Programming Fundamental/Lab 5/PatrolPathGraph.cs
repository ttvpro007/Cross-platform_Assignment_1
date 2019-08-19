using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPathGraph : MonoBehaviour
{
    [SerializeField] List<GameObject> waypoints = new List<GameObject>();
    Graph<GameObject> graph = new Graph<GameObject>();

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            int j = Random.Range(0, transform.GetChild(i).childCount);

            graph.AddNode(transform.GetChild(i).transform.GetChild(j).gameObject);

            waypoints.Add(transform.GetChild(i).transform.GetChild(j).gameObject);
        }

        for (int i = 0; i < graph.size; i++)
        {
            int j = GetNextIndex(transform.childCount, i);
            graph.AddEdge(waypoints[i], waypoints[j]);
        } 
    }
    
    private void OnDrawGizmos()
    {
        for (int i = 0; i < graph.size; i++)
        {
            int j = GetNextIndex(graph.size, i);
            Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
        }
    }

    private int GetNextIndex(int maxSize, int currentIndex)
    {
        return (currentIndex == maxSize - 1) ? 0 : currentIndex + 1;
    }

    public int GetNextWaypointIndex(int currentIndex)
    {
        return (currentIndex == graph.size - 1) ? 0 : currentIndex + 1;
    }

    public Vector3 GetWaypoint(int i)
    {
        return graph.FindNode(waypoints[i]).data.transform.position;
    }
}
