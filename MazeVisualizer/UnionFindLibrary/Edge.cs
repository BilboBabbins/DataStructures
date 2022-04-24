namespace UnionFindLibrary
{
    public class Edge<T>
    {
        public Vertex<T> StartingPoint;
        public Vertex<T> EndPoint;
        public float Distance;
        public Edge(Vertex<T> startingPoint, Vertex<T> endingPoint, float distance = 0)
        {
            StartingPoint = startingPoint;
            EndPoint = endingPoint;
            Distance = distance;
        }
    }
}
