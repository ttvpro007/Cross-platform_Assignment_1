using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph<Data>
{
    List<Node> nodes = new List<Node>();

    public int size
    {
        get { return nodes.Count; }
    }

    public Node AddNode(Data data)
    {
        Node node = new Node(data);
        nodes.Add(node);
        return node;
    }

    public Node FindNode(Data data)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].data.Equals(data))
            {
                return nodes[i];
            }
        }

        return null;
    }

    public void AddEdge(Node node1, Node node2)
    {
        if (node1 == null || node2 == null)
        {
            return;
        }

        node1.neighbors.Add(node2);
        node1.neighbors.Add(node1);
    }

    public void AddEdge(Data data1, Data data2)
    {
        AddEdge(FindNode(data1), FindNode(data2));
    }

    public class Node
    {
        public Node(Data data)
        {
            m_data = data;
        }

        public Data data
        {
            get { return m_data; }
        }

        public List<Node> neighbors
        {
            get { return m_neighbors; }
        }

        Data m_data;
        List<Node> m_neighbors = new List<Node>();
    }
}
