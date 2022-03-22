namespace Survivor;

using NodeGroup = List<Node>;

public class Graph
{
    public NodeGroup AllNodes { get; private set; } = new NodeGroup();
    public List<NodeGroup> AllNodeGroups { get; private set; } = new List<NodeGroup>();

    public List<Triangle> FindTriangles()
    {
        List<Triangle> retVal = new List<Triangle>();
        foreach (Node n1 in AllNodes)
        {
            foreach (NodeGroup n1ng in GetGroupsConnectedToNode(n1))
            {
                foreach (Node n2 in n1ng)
                {
                    foreach (NodeGroup n2ng in GetGroupsConnectedToNode(n2))
                    {
                        if (n2ng.Contains(n1))
                            continue;
                        foreach (Node n3 in n2ng)
                        {
                            foreach (NodeGroup n3ng in GetGroupsConnectedToNode(n3))
                            {
                                if (n3ng.Contains(n1))
                                {
                                    retVal.Add(new Triangle(n1, n2, n3));
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        retVal = retVal.Distinct().OrderBy(t => t.ToString()).ToList();
        return retVal;
    }
    
    public void AddNodeGroup(params string[] groupNodes)
    {
        NodeGroup ng = groupNodes.Select(gn => GetNode(gn)).ToList();
        AllNodeGroups.Add(ng);
    }

    private Node GetNode(string name)
    {
        Node? retVal = AllNodes.Find(n => n.Name == name);
        if (retVal == null)
        {
            retVal = new Node(name);
            AllNodes.Add(retVal);
        }
        return retVal;
    }

    private List<NodeGroup> GetGroupsConnectedToNode(Node node)
    {
        return AllNodeGroups.Where(ng => ng.Contains(node)).Select(ng => ng.Where(n => !n.Equals(node)).ToList()).ToList();
    }
    
    public override string ToString()
    {
        List<string> nodes = AllNodes.OrderBy(n => n.Name).Select(n => n.ToString()).ToList();
        return (String.Join('\n', nodes));
    }
}