using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment
{
    public class Bezier_DeCasteljau : Bezier
    {
        public List<Coordinate2D> controlpoints = new();
        private Bezier_DeCasteljau? nextBezierIteration;

        public Bezier_DeCasteljau(List<Coordinate2D> coordList)
        {
            //set the controlpoints
            this.controlpoints = coordList;
            return;
        }

        public Coordinate2D GetCurvePoint(float t, bool isFirstIteration = true)
        {
            //return if controlpoints is empty
            if (this.controlpoints.Count == 0)
            {
                throw new InvalidOperationException("No control points");
            }
            //check if t is between -1 and 2
            if (t < -1 || t > 2)
            {
                throw new ArgumentOutOfRangeException(nameof(t));
            }
            //if there is only one control point left, return it
            if (this.controlpoints.Count == 1)
            {
                Coordinate2D result = controlpoints[0];
                result.X /= result.W;
                result.Y /= result.W;
                result.W = 1;
                return result;
            }

            List<Coordinate2D> weightedControlPoints = new();

            if (isFirstIteration)
            {
                foreach (Coordinate2D point in controlpoints)
                {
                    weightedControlPoints.Add(new Coordinate2D(point.X * point.W, point.Y * point.W, 1, point.W));
                }
            }
            else
            {
                weightedControlPoints = controlpoints;
            }

            List<Coordinate2D> nextIterationPoints = new();

            //calculate the next iteration of control points
            for (int i = 0; i < weightedControlPoints.Count - 1; i++)
            {
                Coordinate2D newPoint = new Coordinate2D(
                    ((1 - t) * weightedControlPoints[i].X) + (t * weightedControlPoints[i + 1].X),
                    ((1 - t) * weightedControlPoints[i].Y) + (t * weightedControlPoints[i + 1].Y), 
                    1,
                    ((1 - t) * weightedControlPoints[i].W) + (t * weightedControlPoints[i + 1].W));
                nextIterationPoints.Add(newPoint);
            }

            //create a new Bezier object with the next iteration of control points
            this.nextBezierIteration = new Bezier_DeCasteljau(nextIterationPoints);
            return this.nextBezierIteration.GetCurvePoint(t, false);
        }

        public void Derivation()
        {
            //return if controlpoints is empty
            if (this.controlpoints.Count == 0)
            {
                throw new InvalidOperationException("No control points");
            }
            //if there is only one control point left, return it
            if (this.controlpoints.Count == 1)
            {
                return;
            }
            //if there is no next iteration, throw an exception
            if (this.nextBezierIteration == null)
            {
                throw new InvalidOperationException("No next iteration");
            }
            //calculate the derivation of the next iteration
            this.nextBezierIteration.Derivation();
            return;
        }

        public List<Coordinate2D> GetControlPoints(int iteration)
        {
            //return if controlpoints is empty
            if (this.controlpoints.Count == 0)
            {
                throw new InvalidOperationException("No control points");
            }
            //check if iteration is negative
            if (iteration < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(iteration));
            }
            //if there is only one control point left, return it
            if (this.controlpoints.Count == 1)
            {
                return this.controlpoints;
            }
            //if iteration is 0, return the controlpoints
            if (iteration == 0)
            {
                return this.controlpoints;
            }
            //if there is no next iteration, throw an exception
            if (this.nextBezierIteration == null)
            {
                throw new InvalidOperationException("No next iteration");
            }
            //return the controlpoints of the next iteration
            return this.nextBezierIteration.GetControlPoints(iteration - 1);
        }



        public override string ToString()
        {
            string output = "";
            this.controlpoints.ForEach(x => output += x.ToString());
            return output;
        }

        public List<Coordinate2D> IncreaseControlPoints()
        {
            List<Coordinate2D> controlPointsVec = new List<Coordinate2D>();
            foreach (Coordinate2D coord in this.controlpoints)
            {
                controlPointsVec.Add(new Coordinate2D(coord, true));
            }

            List<Coordinate2D> newControlPoints = new List<Coordinate2D>();
            int n = controlpoints.Count - 1;

            // calculation of the new controlpoints
            newControlPoints.Add(controlPointsVec[0]);
            for (float i = 1; i < controlPointsVec.Count; i++)
            {
                float first = (i / (n + 1));
                float second = (1 - (i / (n + 1)));
                newControlPoints.Add(controlPointsVec[(int)i - 1] * first + controlPointsVec[(int)i] * second);
            }
            newControlPoints.Add(controlPointsVec[controlPointsVec.Count - 1]);

            // overwriting controlpoint buffers
            controlpoints.Clear();
            controlPointsVec.Clear();

            for (int i = 0; i < newControlPoints.Count; i++)
            {
                controlpoints.Add(new Coordinate2D(newControlPoints[i], true));
            }

            return controlpoints;
        }

        public List<List<Coordinate2D>> SplitCurve(List<float> t)
        {
            throw new NotImplementedException();
        }
    }
}