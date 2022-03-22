using System;
using System.Collections.Generic;
using System.Linq;

namespace SurvivorUI.Models
{
    using NodeGroup = List<Node>;

    public class Triangle
    {
        public NodeGroup Nodes { get; private set; } = new NodeGroup();

        public Triangle(Node n1, Node n2, Node n3)
        {
            Nodes.Add(n1);
            Nodes.Add(n2);
            Nodes.Add(n3);
        }

        public override bool Equals(object? obj)
        {
            Triangle? other = obj as Triangle;
            if (other == null)
                return (false);
            return ToString().Equals(other.ToString());
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            List<string> nodeNames = Nodes.Select(n => n.Name).OrderBy(s => s).ToList();
            return String.Join('-', nodeNames);
        }
    }
}