using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph<Data>
{
    List<Node<Data>> m_nodes = new List<Node<Data>>();
    List<WeightedEdge<Data>> m_edges = new List<WeightedEdge<Data>>();

    public List<Node<Data>> nodes { get { return m_nodes; } }

    public List<WeightedEdge<Data>> edges { get { return m_edges; } }

    public int nodeCount { get { return m_nodes.Count; } }

    public int edgeCount { get { return m_edges.Count; } }

    public Node<Data> AddNode(Data data)
    {
        Node<Data> node = new Node<Data>(data);
        m_nodes.Add(node);
        return node;
    }

    public Node<Data> FindNode(Data data)
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

    public void AddEdge(Node<Data> node1, Node<Data> node2)
    {
        if (node1 == null || node2 == null)
        {
            return;
        }

        node1.neighbors.Add(node2);
        node2.neighbors.Add(node1);
    }

    public void AddEdge(Node<Data> home, Node<Data> neighbor, float weight)
    {
        WeightedEdge<Data> edge = new WeightedEdge<Data>();

        if (home == null || neighbor == null)
        {
            return;
        }

        home.neighbors.Add(neighbor);
        neighbor.neighbors.Add(home);
        
        home.AddEdge(edge);
        neighbor.AddEdge(edge);
        edge.RegisterProperties(home, neighbor, weight);
        m_edges.Add(edge);
    }

    public void AddEdge(Data data1, Data data2)
    {
        WeightedEdge<Data> edge = new WeightedEdge<Data>();
        AddEdge(FindNode(data1), FindNode(data2));
        edge.RegisterProperties(FindNode(data1), FindNode(data2), 0.0f);
        m_edges.Add(edge);
    }

    public void AddEdge(Data homeData, Data neighborData, float weight)
    {
        Node<Data> home = FindNode(homeData);
        Node<Data> neighbor = FindNode(neighborData);
        WeightedEdge<Data> edge = new WeightedEdge<Data>();

        AddEdge(home, neighbor);
        home.AddEdge(edge);
        neighbor.AddEdge(edge);
        edge.RegisterProperties(home, neighbor, weight);
        edges.Add(edge);
    }

    public float GetWeight(Data homeData, Data neighborData)
    {
        if (homeData != null && neighborData != null)
        {
            Node<Data> home = FindNode(homeData);
            Node<Data> neighbor = FindNode(neighborData);

            foreach (WeightedEdge<Data> edge in home.edges)
            {
                if (edge.neighbor == neighbor)
                {
                    return (edge.weight != 0.0f) ? edge.weight : 0.0f;
                }
            }
        }
        else
        {
            Debug.Log("Home data " + homeData.ToString() + " or Neighbor data " + neighborData.ToString() + " is null.");
            return 0.0f;
        }

        Debug.Log("No weight registered between " + homeData.ToString() + " and " + neighborData.ToString());
        return 0.0f;
    }

    public float GetWeight(Node<Data> home, Node<Data> neighbor)
    {
        if (home != null && neighbor != null)
        {
            foreach (WeightedEdge<Data> edge in home.edges)
            {
                if (edge.neighbor == neighbor)
                {
                    return (edge.weight != 0.0f) ? edge.weight : 0.0f;
                }
            }
        }
        else
        {
            Debug.Log("Home data " + home.ToString() + " or Neighbor data " + neighbor.ToString() + " is null.");
            return 0.0f;
        }

        Debug.Log("No weight registered between " + home.ToString() + " and " + neighbor.ToString());
        return 0.0f;
    }

    public void ResetNodesVisited()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].visited = false;
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