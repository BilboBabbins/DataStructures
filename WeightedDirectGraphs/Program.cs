using System;
using System.Collections.Generic;
using WeightedDirectGraphs;

namespace WeightedDirectGraphs
{
    public class Program
    {
        public static Graph<point> Generate(Graph<point> maingraph, int graphXMax, int graphYMax, int distance = 1)
        { 
            Vertex<point>[,] points = new Vertex<point>[graphXMax, graphYMax];
            for (int i = 0; i < graphXMax; i++)
            {
                for (int j = 0; j < graphYMax; j++)
                {
                    points[i, j] = new Vertex<point>(new point(i, j));
                    maingraph.AddVertex(points[i, j]);
                }
            }
            //connect [i, j] to [i - 1, j]

            for (int a = 0; a < graphXMax; a++)
            {
                int prevY = 0;
                for (int b = 0; b < graphYMax - 1; b++)
                {
                    maingraph.AddEdge(points[a, prevY], points[a, b + 1], distance);
                    prevY = b + 1;
                }
            }
            for (int a = 0; a < graphYMax; a++)
            {
                int prevX = 0;
                for (int b = 0; b < graphXMax - 1; b++)
                {
                    maingraph.AddEdge(points[prevX, a], points[b + 1, a], distance);
                    prevX = b + 1;
                }
            }

            for (int a = graphXMax; a > 0; a--)
            {
                int prevY = graphYMax - 1;
                for (int b = graphYMax; b > 0; b--)
                {
                    if (prevY != b - 1)
                    {
                        maingraph.AddEdge(points[a - 1, prevY], points[a - 1, b - 1], distance);
                        prevY = b - 1;
                    }
                }
            }
            for (int a = graphYMax; a > 0; a--)
            {
                int prevX = graphXMax - 1;
                for (int b = graphXMax; b > 0; b--)
                {
                    if (prevX != b - 1)
                    {
                        maingraph.AddEdge(points[prevX, a - 1], points[b - 1, a - 1], distance);
                        prevX = b - 1;
                    }
                }
            }





            return null;
        }

        public static double Manhattan(Vertex<point> node, Vertex<point> goal)
        {
            double dx = Math.Abs(node.Value.x - goal.Value.x);
            double dy = Math.Abs(node.Value.y - goal.Value.y); ;
            double dis = (dx + dy);
            return dis;
        }
        public static double Diagonal(Vertex<point> node, Vertex<point> goal)
        {

            double dx = Math.Abs(node.Value.x - goal.Value.x);
            double dy = Math.Abs(node.Value.y - goal.Value.y);
            double dis = (dx + dy) + (Math.Sqrt(2) - 2 * 1) * Math.Min(dx, dy);
            return dis;
        }

        public static double Euclidean(Vertex<point> node, Vertex<point> goal)
        {

            double dx = Math.Abs(node.Value.x - goal.Value.x);
            double dy = Math.Abs(node.Value.y - goal.Value.y);
            double dis = Math.Sqrt(dx * dx + dy * dy);
            return dis;
        }
        static void Main(string[] args)
        {

            //dont use this for disjstark , go to
            //seperate program and restart

            //copy + paste the work from there into here
            //once working
           


            LinkedList<Vertex<point>> dijark = new LinkedList<Vertex<point>>();
            LinkedList<Vertex<point>> astar = new LinkedList<Vertex<point>>();

           
            /*double Diagonal(Vertex<double> node, Vertex<double> goal)
            {

                double dx = abs();
                double dy = abs();
                double dis = (dx + dy) + ;
                return dis;
            }

            double Euclidean(Vertex<point> node, Vertex<double> goal)
            {
                double dx = abs(node.Value.x);
                double dy = abs();
                double dis = Math.Sqrt((dx * dx) + (dy * dy));
                return dis;
            }*/

            /*Vertex<point> oneone = new Vertex<point>(1,1);
            Vertex<double> onetwo = new Vertex<double>(1,2);
            Vertex<double> onethree = new Vertex<double>(1,3);
            Vertex<double> twoone = new Vertex<double>(2,1);
            Vertex<double> twotwo = new Vertex<double>(2,2);
            Vertex<double> twothree = new Vertex<double>(2, 3);
            Vertex<double> threeone = new Vertex<double>(3, 1);
            Vertex<double> threetwo = new Vertex<double>(3, 2);
            Vertex<double> threethree = new Vertex<double>(3, 3);*/
         
            Graph<point> maingraph = new Graph<point>();
            int graphXMax = 3;
            int graphYMax = 3;
            Vertex<point>[,] points = new Vertex<point>[graphXMax, graphYMax];
            for(int i = 0; i < graphXMax; i++)
            {
                for(int j = 0; j < graphYMax; j++)
                {
                    points[i, j] = new Vertex<point>(new point(i, j));
                    maingraph.AddVertex(points[i, j]);
                }
            }
            //connect [i, j] to [i - 1, j]
            
            for (int a = 0; a < graphXMax; a++)
            {
                int prevY = 0;
                for (int b = 0; b < graphYMax - 1; b++)
                { 
                    maingraph.AddEdge(points[a, prevY], points[a, b + 1]);
                    prevY = b + 1;
                }
            }
            for (int a = 0; a < graphYMax; a++)
            {
                int prevX = 0;
                for (int b = 0; b < graphXMax - 1; b++)
                {         
                    maingraph.AddEdge(points[prevX, a], points[b + 1, a]);
                    prevX = b + 1;
                }
            }

            for (int a = graphXMax; a > 0; a--)
            {
                int prevY = graphYMax - 1;
                for (int b = graphYMax; b > 0; b--)
                {

                    maingraph.AddEdge(points[a - 1, prevY], points[a - 1, b - 1]);
                    prevY = b - 1;
                }
            }
            for (int a = graphYMax; a > 0; a--)
            { 
                int prevX = graphXMax - 1;
                for (int b = graphXMax; b > 0; b--)
                {

                    maingraph.AddEdge(points[prevX, a - 1], points[b - 1, a - 1]);
                    prevX = b - 1;
                }
            }


            //add edges


           


            //dijark = maingraph.DPathFind(points[0,0], points[2,2]);

            //a* works, graph is broken, reowrk graph
            astar = maingraph.AStarPF(points[0,0], points[2,2], Manhattan);

           //visualizer next time
            


            //breathfirst = maingraph.breadthCall(one);
            //depthfirst = maingraph.CallDepth(three);

            //double breadthPathDis = maingraph.breadthPathFindCall(one, four, breathfirst);
            //double depthPathDis = maingraph.depthPathFindCall(one, four, depthfirst);

        }

      
    }
}
