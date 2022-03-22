namespace Survivor;

using NodeGroup = List<Node>;

public class Node
{
    public string Name { get; private set; }

    public Node(string name)
    {
        if (String.IsNullOrWhiteSpace(name))
            throw new ArgumentException("name cannot be null");
        Name = name;
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