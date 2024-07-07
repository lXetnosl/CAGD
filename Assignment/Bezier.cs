using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
	public class Bezier
	{
        public List<Coordinate2D> controlpoints = new();
		private Bezier? nextBezierIteration;

		public Bezier(List<Coordinate2D> coordList)
		{
			//set the controlpoints
			this.controlpoints = coordList;
			return;
		}

		public void AddPoint(Coordinate2D coord) 
		{
			this.controlpoints.Add(coord);
			return;
		}

		public void RemovePoint(int index)
		{
			//check if index is negative
			if (index < 0)
			{
                throw new ArgumentOutOfRangeException(nameof(index));
            }
			//check if index is greater than the number of controlpoints
			if (this.controlpoints.Count <= index)
			{
                throw new ArgumentOutOfRangeException(nameof(index));
            }
			//remove the controlpoint at the specified index
			controlpoints.RemoveAt(index);
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
                Coordinate2D newPoint = ((1 - t) * this.controlpoints[i]) + (t * this.controlpoints[i + 1]);
                nextIterationPoints.Add(newPoint);
            }

			//create a new Bezier object with the next iteration of control points
			this.nextBezierIteration = new Bezier(nextIterationPoints);
			return this.nextBezierIteration.GetCurvePoint(t);
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