using System.Collections.Generic;

namespace UnionFindLibrary
{
    public class Vertex<T>
    {
        public T Value { get; set; }
        public List<Edge<T>> Edges { get; set; }
        public int EdgesCount => Edges.Count;
        public bool visited = false;
        public Vertex<T> founder = null;
        public double disFromStart = double.PositiveInfinity;
        public double disFromEnd = double.PositiveInfinity;
        public bool blocked = false;

        public Vertex(T value)
        {
            Value = value;
            Edges = new List<Edge<T>>();
        }
    }
}
