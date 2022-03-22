using Survivor;

Graph graph = new Graph();
graph.AddNodeGroup("A", "B", "F", "L", "U", "2");
graph.AddNodeGroup("A", "C", "G", "M", "S", "W", "4");
graph.AddNodeGroup("A", "D", "H", "O", "P", "T", "X", "5");
graph.AddNodeGroup("A", "E", "J", "K", "Q", "Z", "1", "6");
graph.AddNodeGroup("B", "C", "D", "E");
graph.AddNodeGroup("F", "G", "H", "I", "K");
graph.AddNodeGroup("L", "M", "N", "P", "Q");
graph.AddNodeGroup("U", "V", "W", "X", "Y", "Z");
graph.AddNodeGroup("2", "3", "4", "5", "6");
graph.AddNodeGroup("J", "I", "O", "N", "R", "S", "V", "3");
graph.AddNodeGroup("R", "T", "Y", "1");
List<Triangle> answer = graph.FindTriangles();
foreach (Triangle tri in answer)
{
    Console.WriteLine(tri);
}
Console.WriteLine("Num Triangles: " + answer.Count);
