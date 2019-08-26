using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node<Data>
{
    Data m_data;
    List<Node<Data>> m_neighbors = new List<Node<Data>>();
    List<WeightedEdge<Data>> m_edges = new List<WeightedEdge<Data>>();
    bool m_visited = false;

    public Node(Data data) { m_data = data; }
    public Data data { get { return m_data; } }
    public List<Node<Data>> neighbors { get { return m_neighbors; } }
    public List<WeightedEdge<Data>> edges { get { return m_edges; } }
    public bool visited { get { return m_visited; } set { m_visited = value; } }

    public void AddEdge(WeightedEdge<Data> edge)
    {
        m_edges.Add(edge);
    }

    public override string ToString()
    {
        return string.Format("{0}", m_data.ToString());
    }
}

