using System;
using System.Collections.Generic;
using System.Text;

namespace MazeVisualizer
{
    struct DrawingInfo
    {
        public (int y, int x) Cell;
        public (int y, int x) Neighbor;
        public Directions Direction;

        public DrawingInfo((int y, int x) cell, (int y, int x) neighbor, Directions direction)
        {
            Cell = cell;
            Neighbor = neighbor;
            Direction = direction;
        }
    }
}
