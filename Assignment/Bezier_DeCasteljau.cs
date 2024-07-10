using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

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

		public Coordinate2D GetCurvePoint(float t)
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
                return this.controlpoints[0];
            }

			List<Coordinate2D> nextIterationPoints = new();

			//calculate the next iteration of control points
            for (int i = 0; i < this.controlpoints.Count - 1; i++)
            {
                Coordinate2D newPoint = new Coordinate2D(
                    ((1 - t) * controlpoints[i].X) + (t * controlpoints[i + 1].X),
                    ((1 - t) * controlpoints[i].Y) + (t * controlpoints[i + 1].Y), 1);
                nextIterationPoints.Add(newPoint);
            }

			//create a new Bezier object with the next iteration of control points
			this.nextBezierIteration = new Bezier_DeCasteljau(nextIterationPoints);
			return this.nextBezierIteration.GetCurvePoint(t);
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

    }
}