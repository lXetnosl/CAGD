using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    internal class Bezier_Bernstein : Bezier
    {
        private List<Coordinate2D> _controlPoints;
        private List<Coordinate2D> _controlPointsVec;

        public List<Coordinate2D> ControlPoints
        {
            get => _controlPoints;
            set => _controlPoints = value;
        }
        private List<Coordinate2D> ControlPointsVec
        {
            get => _controlPointsVec;
            set => _controlPointsVec = value;
        }

        public Bezier_Bernstein(List<Coordinate2D> coordList)
        {
            ControlPoints = coordList;
            ControlPointsVec = new List<Coordinate2D>();
            foreach(Coordinate2D coord in ControlPoints)
            {
                ControlPointsVec.Add(new Coordinate2D(coord, true));
            }
        }

        public void AddControlPoint(Coordinate2D controlPoint, int index)
        {
            ControlPoints.Insert(index, controlPoint);
            ControlPointsVec.Insert(index, new Coordinate2D(controlPoint, true));
        }

        public void AddControlPoint(Coordinate2D controlPoint)
        {
            ControlPoints.Add(controlPoint);
            ControlPointsVec.Add(new Coordinate2D(controlPoint, true));
        }

        public void ClearControlPoints()
        {
            ControlPoints = new List<Coordinate2D>();
            ControlPointsVec = new List<Coordinate2D>();
        }



        public Coordinate2D GetCurvePoint(float t)
        {
            var bezierCurve = CalculateBezierCurve(ControlPoints);
            //return if controlpoints is empty
            if (ControlPoints.Count == 0)
            {
                throw new InvalidOperationException("No control points");
            }
            //check if t is between -1 and 2
            if (t < -1 || t > 2)
            {
                throw new ArgumentOutOfRangeException(nameof(t));
            }
            //if there is only one control point left, return it
            if (ControlPoints.Count == 1)
            {
                return ControlPoints[0];
            }

            return new Coordinate2D(bezierCurve(t).x, bezierCurve(t).y);
        }

        // Rekursive Funktion zum Berechnen des Bernsteinpolynoms B_{i,n}(t)
        public static float BernsteinRecursive(int n, int i, float t)
        {
            if (n == 0)
                return (i == 0) ? 1.0f : 0.0f;

            float a = (i > 0) ? t * BernsteinRecursive(n - 1, i - 1, t) : 0.0f;
            float b = (i < n) ? (1 - t) * BernsteinRecursive(n - 1, i, t) : 0.0f;

            return a + b;
        }

        // Funktion zum Berechnen der Bernsteinpolynome für einen gegebenen Grad n
        public static List<Func<float, float>> CalculateBernsteinPolynomials(int n)
        {
            List<Func<float, float>> polynomials = new List<Func<float, float>>();

            for (int i = 0; i <= n; i++)
            {
                int index = i; // Lokale Kopie von i für den Lambda-Ausdruck
                polynomials.Add(t => BernsteinRecursive(n, index, t));
            }

            return polynomials;
        }

        // Funktion zum Berechnen der Bezierkurve
        public static Func<float, (float x, float y)> CalculateBezierCurve(List<Coordinate2D> controlPoints)
        {
            int n = controlPoints.Count - 1;
            var bernsteinPolynomials = CalculateBernsteinPolynomials(n);

            return t =>
            {
                float x = 0, y = 0;
                for (int i = 0; i <= n; i++)
                {
                    float b = bernsteinPolynomials[i](t);
                    x += b * controlPoints[i].X;
                    y += b * controlPoints[i].Y;
                }
                return (x, y);
            };
        }

        public List<Coordinate2D> GetControlPoints(int iteration)
        {
            if (iteration > ControlPoints.Count)
                return null;

            var bernsteinPolynomials = CalculateBernsteinPolynomials(ControlPoints.Count - 1);

            var output = new List<Coordinate2D>();
            for (int i = 0; i <= 10; i++)
            {
                float t = 1f / 10 * i;


                output.Add(new Coordinate2D(t, bernsteinPolynomials[iteration](t)));

                Coordinate2D calcedCoord;
                //switch (ControlPoints.Count)
                //{
                //    case 0:
                //    case 1:
                //        return null;
                //    case 2:
                //        switch(iteration)
                //        {
                //            case 1:
                //                output.Add(new Coordinate2D(t, 1 - t, 1));
                //                break;
                //            case 2:
                //                output.Add(new Coordinate2D(t, t, 1));
                //                break;
                //            default:
                //                return null;
                //        }
                        
                //        break;
                //    case 3:
                //        switch (iteration)
                //        {
                //            case 1:
                //                output.Add(new Coordinate2D(t, (float)Math.Pow(1 - t, 2), 1));
                //                break;
                //            case 2:
                //                output.Add(new Coordinate2D(t, 2 * t * (1 - t), 1));
                //                break;
                //            case 3:
                //                output.Add(new Coordinate2D(t, (float)Math.Pow(t, 2), 1));
                //                break;
                //            default:
                //                return null;
                //        }
                //        break;
                //    case 4:
                //        switch (iteration)
                //        {
                //            case 1:
                //                output.Add(new Coordinate2D(t, (float)Math.Pow((1 - t), 3), 1));
                //                break;
                //            case 2:
                //                output.Add(new Coordinate2D(t, 3 * t * ((float)Math.Pow(1 - t, 2)), 1));
                //                break;
                //            case 3:
                //                output.Add(new Coordinate2D(t, 3 * ((float)Math.Pow(t, 2)) * (1 - t), 1));
                //                break;
                //            case 4:
                //                output.Add(new Coordinate2D(t, (float)Math.Pow(t, 3), 1));
                //                break;
                //            default:
                //                return null;
                //        }
                //        break;
                //}
            }
            return formatOutput(output);
        }

        private List<Coordinate2D> formatOutput(List<Coordinate2D> input)
        {
            float minX = 11223344;
            float maxX = -11223344;
            float minY = 11223344;
            float maxY = -11223344;

            List<Coordinate2D> output = new List<Coordinate2D>();

            foreach(Coordinate2D point in ControlPoints)
            {
                if(point.X < minX)
                    minX = point.X;
                if(point.Y < minY)
                    minY = point.Y;
                if(point.X > maxX)
                    maxX = point.X;
                if (point.Y > maxY)
                    maxY = point.Y;
            }

            foreach(Coordinate2D point in input)
            {
                float newX = minX + point.X * (maxX - minX);
                float newY = minY + point.Y * (maxY - minY);

                Coordinate2D formattedPoint = new Coordinate2D(newX, newY);
                output.Add(formattedPoint);
            }

            return output;
        }
    }
}
