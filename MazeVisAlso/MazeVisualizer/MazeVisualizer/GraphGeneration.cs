using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Text;

using UnionFindLibrary;

namespace MazeVisualizer
{
    public class GraphGeneration
    {


        public static Graph<Point> Generate(Graph<Point> maingraph, int graphXMax, int graphYMax, int distance = 1)
        {
            Vertex<Point>[,] Points = new Vertex<Point>[graphXMax, graphYMax];
            for (int i = 0; i < graphXMax; i++)
            {
                for (int j = 0; j < graphYMax; j++)
                {
                    Points[i, j] = new Vertex<Point>(new Point(i, j));
                    maingraph.AddVertex(Points[i, j]);
                }
            }
            //connect [i, j] to [i - 1, j]

            for (int a = 0; a < graphXMax; a++)
            {
                int prevY = 0;
                for (int b = 0; b < graphYMax - 1; b++)
                {
                    maingraph.AddEdge(Points[a, prevY], Points[a, b + 1], distance);
                    prevY = b + 1;
                }
            }
            for (int a = 0; a < graphYMax; a++)
            {
                int prevX = 0;
                for (int b = 0; b < graphXMax - 1; b++)
                {
                    maingraph.AddEdge(Points[prevX, a], Points[b + 1, a], distance);
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
                        maingraph.AddEdge(Points[a - 1, prevY], Points[a - 1, b - 1], distance);
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
                        maingraph.AddEdge(Points[prevX, a - 1], Points[b - 1, a - 1], distance);
                        prevX = b - 1;
                    }
                }
            }





            return null;
        }

        public static double Manhattan(Vertex<Point> node, Vertex<Point> goal)
        {
            double dx = Math.Abs(node.Value.X - goal.Value.X);
            double dy = Math.Abs(node.Value.Y - goal.Value.Y); ;
            double dis = (dx + dy);
            return dis;
        }
        public static double Diagonal(Vertex<Point> node, Vertex<Point> goal)
        {

            double dx = Math.Abs(node.Value.X - goal.Value.X);
            double dy = Math.Abs(node.Value.Y - goal.Value.Y);
            double dis = (dx + dy) + (Math.Sqrt(2) - 2 * 1) * Math.Min(dx, dy);
            return dis;
        }

        public static double Euclidean(Vertex<Point> node, Vertex<Point> goal)
        {

            double dx = Math.Abs(node.Value.X - goal.Value.X);
            double dy = Math.Abs(node.Value.Y - goal.Value.Y);
            double dis = Math.Sqrt(dx * dx + dy * dy);
            return dis;
        }

     
        public static (int y, int x) GeneratePos(int rows, int cols, Random rand)
        {
            return (rand.Next(0, rows), rand.Next(0, cols));
        }

        private static (int y, int x)[] directions = new [] { (-1, 0), (1, 0), (0, 1), (0, -1) };
        public static (int y, int x) GenerateDirection(Random rand)
        {
            return directions[rand.Next(0, directions.Length)];
        }
        public static (int y, int x) AddDirection((int y, int x) ogPos, (int y, int x) direction)
        {
            return ((ogPos.y + direction.y), (ogPos.x + direction.x));
        }
    }
}
