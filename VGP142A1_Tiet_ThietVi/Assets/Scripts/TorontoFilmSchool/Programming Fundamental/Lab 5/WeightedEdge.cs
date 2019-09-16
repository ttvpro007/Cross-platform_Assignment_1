public class WeightedEdge<Data>
{
    Node<Data> m_home;
    Node<Data> m_neighbor;
    float m_weight;

    public float weight { get { return m_weight; } }
    public Node<Data> home { get { return m_home; } }
    public Node<Data> neighbor { get { return m_neighbor; } }

    public void RegisterProperties(Node<Data> home, Node<Data> neighbor, float weight)
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
