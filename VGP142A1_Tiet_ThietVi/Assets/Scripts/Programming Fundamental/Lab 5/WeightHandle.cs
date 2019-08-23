using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WeightHandle : MonoBehaviour
{
    public Node<GameObject> self;
    public List<Vector3> textPositions;
    public List<float> weights;

    private void Start()
    {
        Initiate();
    }

    private void Initiate()
    {
        self = GetComponentInParent<PatrolPathGraph>().graph.FindNode(gameObject);

        WeightedEdge<GameObject> currentEdge;
        Vector3 textPosition = new Vector3();
        Vector3 neighborPosition;
        int edgeCount;
        float x, y, z;

        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;

        edgeCount = self.edges.Count;

        for (int i = 0; i < edgeCount; i++)
        {
            currentEdge = self.edges[i];

            if (self.data == currentEdge.home.data && !currentEdge.neighbor.visited)
            {
                neighborPosition = currentEdge.neighbor.data.transform.position;

                textPosition.x = (x + neighborPosition.x) / 2;
                textPosition.y = (y + neighborPosition.y) / 2;
                textPosition.z = (z + neighborPosition.z) / 2;

                textPositions.Add(textPosition);
                weights.Add(currentEdge.weight);

                currentEdge.neighbor.visited = true;
            }
        }
    }
}
