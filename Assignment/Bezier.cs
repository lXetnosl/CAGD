using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    internal interface Bezier
    {

        Coordinate2D GetCurvePoint(float t, bool isFirstIteration = true);

        List<Coordinate2D> GetControlPoints(int iteration);

        public List<Coordinate2D> IncreaseControlPoints();

        public List<List<Coordinate2D>> SplitCurve(List<float> t);

        public List<Coordinate2D> GetDerivationControlPoints();
        public Coordinate2D GetDerivationCurvePoint(float t);
    }
}
