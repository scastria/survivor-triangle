using System;
using System.Collections.Generic;
using System.Linq;

namespace SurvivorUI.Models
{
    using NodeGroup = List<Node>;

    public class Graph
    {
        public NodeGroup AllNodes { get; private set; } = new NodeGroup();
        public List<NodeGroup> AllNodeGroups { get; private set; } = new List<NodeGroup>();

        public Graph()
        {
            AddNode("A", 169, 0);
            AddNode("B", 129, 82);
            AddNode("C", 155, 82);
            AddNode("D", 184, 82);
            AddNode("E", 211, 82);
            AddNode("F", 97, 148);
            AddNode("G", 144, 148);
            AddNode("H", 196, 148);
            AddNode("I", 228, 148);
            AddNode("J", 240, 137);
            AddNode("K", 245, 148);
            AddNode("L", 63, 218);
            AddNode("M", 132, 218);
            AddNode("N", 160, 218);
            AddNode("O", 201, 177);
            AddNode("P", 209, 218);
            AddNode("Q", 282, 218);
            AddNode("R", 153, 228);
            AddNode("S", 127, 254);
            AddNode("T", 215, 254);
            AddNode("U", 31, 284);
            AddNode("V", 98, 284);
            AddNode("W", 121, 284);
            AddNode("X", 221, 284);
            AddNode("Y", 286, 284);
            AddNode("Z", 316, 284);
            AddNode("1", 324, 300);
            AddNode("2", 0, 348);
            AddNode("3", 37, 348);
            AddNode("4", 111, 348);
            AddNode("5", 232, 348);
            AddNode("6", 349, 348);
            AddNodeGroup("A", "B", "F", "L", "U", "2");
            AddNodeGroup("A", "C", "G", "M", "S", "W", "4");
            AddNodeGroup("A", "D", "H", "O", "P", "T", "X", "5");
            AddNodeGroup("A", "E", "J", "K", "Q", "Z", "1", "6");
            AddNodeGroup("B", "C", "D", "E");
            AddNodeGroup("F", "G", "H", "I", "K");
            AddNodeGroup("L", "M", "N", "P", "Q");
            AddNodeGroup("U", "V", "W", "X", "Y", "Z");
            AddNodeGroup("2", "3", "4", "5", "6");
            AddNodeGroup("J", "I", "O", "N", "R", "S", "V", "3");
            AddNodeGroup("R", "T", "Y", "1");
        }

        public (int Width, int Height) GetSize()
        {
            (int Width, int Height) retVal = (-1, -1);
            foreach (Node n in AllNodes)
            {
                if ((retVal.Width == -1) || (n.X > retVal.Width))
                    retVal.Width = n.X;
                if ((retVal.Height == -1) || (n.Y > retVal.Height))
                    retVal.Height = n.Y;
            }
            return retVal;
        }
        
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

        public void AddNode(string name, int x, int y)
        {
            Node n = GetNode(name);
            n.SetPosition(x, y);
        }
        
        public void AddNodeGroup(params string[] groupNodes)
        {
            NodeGroup ng = groupNodes.Select(gn => GetNode(gn)).ToList();
            AllNodeGroups.Add(ng);
        }

        public Node GetNode(string name)
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
}