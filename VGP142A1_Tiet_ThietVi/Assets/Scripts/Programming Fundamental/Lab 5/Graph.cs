using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph<Data>
{
    List<Node> nodes = new List<Node>();
    List<WeightedEdge> edges = new List<WeightedEdge>();

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
        node2.neighbors.Add(node1);
    }

    public void AddEdge(Node home, Node neighbor, float weight)
    {
        WeightedEdge edge = new WeightedEdge();

        if (home == null || neighbor == null)
        {
            return;
        }

        home.neighbors.Add(neighbor);
        neighbor.neighbors.Add(home);
        
        home.AddEdge(edge);
        neighbor.AddEdge(edge);
        edge.RegisterProperties(home, neighbor, weight);
        edges.Add(edge);
    }

    public void AddEdge(Data data1, Data data2)
    {
        AddEdge(FindNode(data1), FindNode(data2));
    }

    public void AddEdge(Data homeData, Data neighborData, float weight)
    {
        Node home = FindNode(homeData);
        Node neighbor = FindNode(neighborData);
        WeightedEdge edge = new WeightedEdge();

        AddEdge(home, neighbor);
        home.AddEdge(edge);
        neighbor.AddEdge(edge);
        edge.RegisterProperties(home, neighbor, weight);
        edges.Add(edge);
    }

    public class Node
    {
        Data m_data;
        List<Node> m_neighbors = new List<Node>();
        List<WeightedEdge> m_edges = new List<WeightedEdge>();

        public Node(Data data) { m_data = data; }
        public Data data { get { return m_data; } }
        public List<Node> neighbors { get { return m_neighbors; } }
        public List<WeightedEdge> edges { get { return m_edges; } }

        public void AddEdge(WeightedEdge edge)
        {
            m_edges.Add(edge);
        }

        public override string ToString()
        {
            return string.Format("{0}", m_data.ToString());
        }
    }

    public class WeightedEdge
    {
        Node m_home;
        Node m_neighbor;
        float m_weight;

        public float weight { get { return m_weight; } }
        public Node home { get { return m_home; } }
        public Node neighbor { get { return m_neighbor; } }

        public void RegisterProperties(Node home, Node neighbor, float weight)
        {
            m_home = home;
            m_neighbor = neighbor;
            m_weight = weight;
        }

        public override string ToString()
        {
            return string.Format("Home: {0} - Distance: {1} meters - Neighbor: {2}", m_home.ToString(), weight.ToString("0.000"), m_neighbor.ToString());
        }
    }
}

public static class IListExtensions
{
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        int count = ts.Count;
        int last = count - 1;
        for (int i = 0; i < last; i++)
        {
            int r = Random.Range(i, count);
            var temp = ts[i];
            ts[i] = ts[r];
            ts[r] = temp;
        }
    }
}