using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeightedDirectGraphs
{
    public struct point
    {
        public double x;
        public double y;
        public point(double X, double Y)
        {
            x = X;
            y = Y;
        }
    }
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

    public class Graph<T> 
    {
        public List<Vertex<T>> Vertices { get; private set; }
        public int VertexCount => Vertices.Count;
        public Graph()
        {
            Vertices = new List<Vertex<T>>();
        }

        public void AddVertex(Vertex<T> ver)
        {
            if (ver == null && ver.Edges == null && Vertices.Contains(ver))
            {
                return;
            }
            Vertices.Add(ver);
        }
        public bool RemoveVertex(Vertex<T> ver)
        {
            if (Vertices.Contains(ver))
            {
                ver.Edges.Clear();
                // remove all edning point
                //gotta loop through all those points
                //damn what
                for(int a = 0; a < Vertices.Count; a++)
                {
                   
                        RemoveEdge(Vertices[a], ver);

                }

                Vertices.Remove(ver);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddEdge(Vertex<T> verA, Vertex<T> verB, float distance = 1)
        {

            Edge<T> newEdge = new Edge<T>(verA, verB, distance);
            //check if new edge exists with search
            if (verA == null || verB == null || !Vertices.Contains(verA)
            || !Vertices.Contains(verB) || verA.Edges.Contains(newEdge)
            || verA.Edges.Any(edge => edge.EndPoint == verB))
            {
                return false;
            }
            verA.Edges.Add(newEdge);
            return true;
        }
        public bool RemoveEdge(Vertex<T> verA, Vertex<T> verB)
        {
            
            if (verA == null || verB == null)
            {
                return false;
            }

            for (int a = 0; a < verA.Edges.Count; a++)
            {
                if (verA.Edges[a].EndPoint == verB)
                {
                    verA.Edges.RemoveAt(a);
                    return true;
                }
            }
            return false;
        }

        public Vertex<T> Search(T value)
        {
            int index = -1;
            for(int a = 0; a < Vertices.Count; a++)
            {
                if (Vertices[a].Value.Equals(value))
                {
                    index = a;
                    break;
                }
            }
            if (index == -1)
            {
                return null;
            }
            else
            {
                return Vertices[index];
            }

        }
        public Edge<T> getEdge(Vertex<T> verA, Vertex<T> verB)
        {
            if (verA == null || verB == null || 
                !verA.Edges.Any(edge => edge.EndPoint == verB))
            {
                return null;
            }
            for (int a = 0; a < verA.Edges.Count; a++)
            {
                if (verA.Edges[a].EndPoint == verB)
                {
                    return verA.Edges[a];
                }
            }
            return null;
        }

       
        //depth search
        //
        public float DepthTraversal(Vertex<T> start, List<Vertex<T>> values, HashSet<Vertex<T>> set, float dis)
        {

            // go through children
            // add
            // if none return
            
            values.Add(start);
            set.Add(start);
       

            if (start.Edges == null)
            {
                return dis;
            }
            for (int a = 0; a < start.Edges.Count; a++)
            {
                if (!set.Contains(start.Edges[a].EndPoint))
                {
                    set.Add(start.Edges[a].EndPoint);
                    dis += start.Edges[a].Distance;
                    dis = DepthTraversal(start.Edges[a].EndPoint, values, set, dis);
                   
                }
                
            }
            return dis;
        }
        public List<Vertex<T>> CallDepth(Vertex<T> start)
        {
            List<Vertex<T>> order = new List<Vertex<T>>();
            HashSet<Vertex<T>> set = new HashSet<Vertex<T>>();
            float distance = 0;
            DepthTraversal(start, order, set, distance);
            return order;
        }

        //breadth first
      
       
        float breadthSearch(Vertex<T> starting,List<Vertex<T>> stored)
        {
            // Dictionary<Vertex<T>, Vertex<T>> parents = new Dictionary<Vertex<T>, Vertex<T>>();
            Queue<Vertex<T>> queued = new Queue<Vertex<T>>();
            HashSet<Vertex<T>> containedIn = new HashSet<Vertex<T>>();
            Vertex<T> current = starting;
            float totDistance = 0;
            queued.Enqueue(current);
            while (queued.Count != 0)
            {
                current = queued.Dequeue();
                for (int a = 0; a < current.Edges.Count; a++)
                {
                    if (!containedIn.Contains(current.Edges[a].EndPoint))
                    {
                        queued.Enqueue(current.Edges[a].EndPoint);
                        containedIn.Add(current.Edges[a].EndPoint);
                        totDistance += current.Edges[a].Distance;
                    }
                }
                stored.Add(current);
                containedIn.Add(current);
                //stored.Add(queued.Dequeue());
                //current = queued.Peek();



            }
            return totDistance;
        }
        public List<Vertex<T>> breadthCall(Vertex<T> starting)
        {
            List<Vertex<T>> breadthList = new List<Vertex<T>>();
            breadthSearch(starting, breadthList);
            return breadthList;
        }
        public float breadthCallDis(Vertex<T> starting)
        {
            List<Vertex<T>> breadthList = new List<Vertex<T>>();
            return breadthSearch(starting, breadthList);
        }
        public float depthFirstDis(Vertex<T> start)
        {
            List<Vertex<T>> order = new List<Vertex<T>>();
            HashSet<Vertex<T>> set = new HashSet<Vertex<T>>();
            float distance = 0;
            return DepthTraversal(start, order, set, distance);
        }





        //PATHFINDING vvv
        float breadthPathFind(Vertex<T> starting, Vertex<T> end, List<Vertex<T>> stored)
        {
            Queue<Vertex<T>> queued = new Queue<Vertex<T>>();
            HashSet<Vertex<T>> containedIn = new HashSet<Vertex<T>>();
            Vertex<T> current = starting;
            float totDistance = 0;
            queued.Enqueue(current);
            while (queued.Count != 0)
            {
                current = queued.Dequeue();
                for (int a = 0; a < current.Edges.Count; a++)
                {
                    if (!containedIn.Contains(current.Edges[a].EndPoint))
                    {
                        queued.Enqueue(current.Edges[a].EndPoint);
                        containedIn.Add(current.Edges[a].EndPoint);
                        totDistance += current.Edges[a].Distance;
                    }
                }
                stored.Add(current);
                containedIn.Add(current);
                if (current == end)
                {
                    return totDistance;
                }
                //stored.Add(queued.Dequeue());
                //current = queued.Peek();



            }
            return totDistance;
        }
        float depthPathFind(Vertex<T> start, Vertex<T> end , List<Vertex<T>> values, HashSet<Vertex<T>> set, float dis)
        {
            values.Add(start);
            set.Add(start);


            if (start.Edges == null || start == end)
            {
                return dis;
            }
            for (int a = 0; a < start.Edges.Count; a++)
            {
                if (!set.Contains(start.Edges[a].EndPoint))
                {
                    set.Add(start.Edges[a].EndPoint);
                    dis += start.Edges[a].Distance;
                    if (start.Edges[a].EndPoint == end)
                    {
                        return dis;
                    }
                    dis = DepthTraversal(start.Edges[a].EndPoint, values, set, dis);

                }

            }
            return dis;

        }

        public float breadthPathFindCall(Vertex<T> start, Vertex<T> end, List<Vertex<T>> breadthlist)
        {
          
            return breadthPathFind(start,end, breadthlist);
        }
        public float depthPathFindCall(Vertex<T> start, Vertex<T> end, List<Vertex<T>> depthlist)
        {

            HashSet<Vertex<T>> set = new HashSet<Vertex<T>>();
            float distance = 0;
            return depthPathFind(start, end, depthlist, set, distance);
        }

        //hw: debate




        // PATHFINDING 
        //dijark

        // tentative distances = current vertex's cumulative distance plus 
        //the weight to travel to that neighbor.
       

        //LOOK ALL THE WAY DOWN FOR WHAT DID LAST CLASS
        public LinkedList<Vertex<T>> DPathFind(Vertex<T> start, Vertex<T> end)
        {
            //tentative distances = current vertex's cumulative distance plus 
            //the weight to travel to that neighbor
            
            Vertex<T> current;
            Comparer<Vertex<T>> comp = Comparer<Vertex<T>>.Create((x,y) =>
            x.disFromStart.CompareTo(y.disFromStart));
            Priority<Vertex<T>> priorityQ = new Priority<Vertex<T>>(comp);
            start.disFromStart = 0;
            priorityQ.insert(start);

            while (end.visited == false || priorityQ.values.Length != 0)
            {
                current = priorityQ.pop();
                for (int a = 0; a < current.Edges.Count; a++)
                {
                    double tentDis = current.disFromStart +
                        current.Edges[a].Distance;

                    if (tentDis < current.Edges[a].EndPoint.disFromStart)
                    {
                        //do indexof here 
                        current.Edges[a].EndPoint.disFromStart = tentDis;
                        current.Edges[a].EndPoint.founder = current;
                    }

                    //Add all un-visited & un-queued neighbors to the priority queue.
                    if (current.Edges[a].EndPoint.visited == false && 
                        !priorityQ.contains(current.Edges[a].EndPoint))
                    {
                        priorityQ.insert(current.Edges[a].EndPoint);
                    }

                    //do indexOf instead of contains 
                }
                current.visited = true;
                if (end.visited == true)
                {
                    Vertex<T> temp = end;
                    LinkedList<Vertex<T>> path = new LinkedList<Vertex<T>>();
                    while (current != null)
                    {
                        path.AddFirst(current);
                        current = current.founder;
                    }
                    return path;
                }
            }
            return null;

        }



        //A* PATHFINDING STUFF vvvv

        //11/30
        //started working on a*, currently doing the heurstics 

        //heurstics are like 'estimates' 
        //Dijk counts the steps then chooses the lesser one
        //only difference between a* and dijk is that A* includes an estimate. 
        

        /* float Manhattan(Vertex<T> node, Vertex<T> goal)
         {
            float dx = abs();
            float dy = abs();
            return;
         }
        float Diagonal(Vertex<T> node, Vertex<T> goal)
        {
            float dx = abs();
            float dy = abs();
            return;
        }

        float Euclidean(Vertex<T> node, Vertex<T> goal)
        {
            float dx = abs();
            float dy = abs();
            return;
        }*/


        public LinkedList<Vertex<T>> AStarPF(Vertex<T> start, Vertex<T> end, 
            Func<Vertex<T>, Vertex<T>, double> Heuris)
        {
            //tentative distances = current vertex's cumulative distance plus 
            //the weight to travel to that neighbor
            for (int a = 0; a < VertexCount; a++)
            {
                Vertices[a].disFromEnd = double.PositiveInfinity;
                Vertices[a].disFromStart = double.PositiveInfinity;
                Vertices[a].founder = null;
                Vertices[a].visited = false;
            }
           

            Vertex<T> current;
            Comparer<Vertex<T>> comp = Comparer<Vertex<T>>.Create((x, y) =>
            x.disFromEnd.CompareTo(y.disFromEnd));
            Priority<Vertex<T>> priorityQ = new Priority<Vertex<T>>(comp);
            start.disFromStart = 0;

            start.disFromEnd = Heuris(start, end);
            priorityQ.insert(start);

            while (end.visited == false && priorityQ.values.Length != 0)
            {
                current = priorityQ.pop();
                for (int a = 0; a < current.Edges.Count; a++)
                {
                    if (!current.Edges[a].EndPoint.Equals(current))
                    {
                        double tentDis = current.disFromStart + current.Edges[a].Distance;

                        if (tentDis + Heuris(current.Edges[a].EndPoint, end) < current.Edges[a].EndPoint.disFromEnd)
                        {
                            //do indexof here 
                            current.Edges[a].EndPoint.disFromStart = tentDis;
                            current.Edges[a].EndPoint.founder = current;
                            current.Edges[a].EndPoint.disFromEnd = tentDis + Heuris(current.Edges[a].EndPoint, end);

                            //Add all un-visited & un-queued neighbors to the priority queue.
                            if (current.Edges[a].EndPoint.visited == false && current.Edges[a].EndPoint.blocked != true)
                            {
                                priorityQ.insert(current.Edges[a].EndPoint);
                            }
                        }
                    }

                    

                    //do indexOf instead of contains 
                }
                current.visited = true;
                if (end.visited == true )
                {
                    Vertex<T> temp = end;
                    LinkedList<Vertex<T>> path = new LinkedList<Vertex<T>>();
                    while (current != null)
                    {
                        path.AddFirst(current);
                        current = current.founder;
                    }
                    return path;
                }
            }
            return null;

        }
    }


//priority que only takes in the float distance which means
//the vertex is not attached to it 
//find a way to 

}
