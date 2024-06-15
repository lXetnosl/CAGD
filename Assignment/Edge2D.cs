using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    internal class Edge2D
    {
        public Coordinate2D Start { get; set; }
        public Coordinate2D End { get; set; }

        public Edge2D(Coordinate2D start, Coordinate2D end)
        {
            Start = start;
            End = end;
        }

        internal void UpdateStart(Coordinate2D updatedStart)
        {
            Start = updatedStart;
        }

        internal void UpdateEnd(Coordinate2D updatedEnd)
        {
            End = updatedEnd;
        }
    }
}
