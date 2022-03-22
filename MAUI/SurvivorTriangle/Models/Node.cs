using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvivorTriangle.Models
{
    public class Node
    {
        public string Name { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public Node(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("name cannot be null");
            Name = name;
        }

        public void SetPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object? obj)
        {
            Node? other = obj as Node;
            if (other == null)
                return (false);
            return Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
