using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment
{
    internal class Bezier_Bernstein : Bezier
    {
        // List of control points
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
            // Initialize Bernstein object and set the control points
            ControlPoints = coordList;
            ControlPointsVec = new List<Coordinate2D>();
            foreach(Coordinate2D coord in ControlPoints)
            {
                ControlPointsVec.Add(new Coordinate2D(coord, true));
            }
        }

        public void AddControlPoint(Coordinate2D controlPoint, int index)
        {
            // Add a control point at a specific index
            ControlPoints.Insert(index, controlPoint);
            ControlPointsVec.Insert(index, new Coordinate2D(controlPoint, true));
        }

        public void AddControlPoint(Coordinate2D controlPoint)
        {
            // Add a control point at the end of the list
            ControlPoints.Add(controlPoint);
            ControlPointsVec.Add(new Coordinate2D(controlPoint, true));
        }

        public void ClearControlPoints()
        {
            // Clear the list of control points
            ControlPoints = new List<Coordinate2D>();
            ControlPointsVec = new List<Coordinate2D>();
        }



        public Coordinate2D GetCurvePoint(float t, bool isFirstIteration = true)
        {
            // Calculate the Bezier curve point at parameter t
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

        public static float BernsteinRecursive(int n, int i, float t)
        {
            // Rekursive Funktion zum Berechnen des Bernsteinpolynoms B_{i,n}(t)
            if (n == 0)
                return (i == 0) ? 1.0f : 0.0f;

            float a = (i > 0) ? t * BernsteinRecursive(n - 1, i - 1, t) : 0.0f;
            float b = (i < n) ? (1 - t) * BernsteinRecursive(n - 1, i, t) : 0.0f;

            return a + b;
        }

        public static List<Func<float, float>> CalculateBernsteinPolynomials(int n)
        {
            // Funktion zum Berechnen der Bernsteinpolynome für einen gegebenen Grad n
            List<Func<float, float>> polynomials = new List<Func<float, float>>();

            for (int i = 0; i <= n; i++)
            {
                int index = i; // Lokale Kopie von i für den Lambda-Ausdruck
                polynomials.Add(t => BernsteinRecursive(n, index, t));
            }

            return polynomials;
        }

        public static Func<float, (float x, float y)> CalculateBezierCurve(List<Coordinate2D> controlPoints)
        {
            // Funktion zum Berechnen der Bezierkurve
            int n = controlPoints.Count - 1;
            var bernsteinPolynomials = CalculateBernsteinPolynomials(n);

            return t =>
            {
                float x = 0, y = 0, w = 0;
                for (int i = 0; i <= n; i++)
                {
                    float b = bernsteinPolynomials[i](t);
                    x += b * controlPoints[i].X * controlPoints[i].W;
                    y += b * controlPoints[i].Y * controlPoints[i].W;
                    w += b * controlPoints[i].W;
                }
                return (x / w, y / w);
            };
        }

        public List<Coordinate2D> GetControlPoints(int iteration)
        {
            // Get the control points for the Bezier curve at iteration
            if (iteration > ControlPoints.Count)
                return null;

            var bernsteinPolynomials = CalculateBernsteinPolynomials(ControlPoints.Count - 1);

            var output = new List<Coordinate2D>();
            for (int i = 0; i <= 10; i++)
            {
                float t = 1f / 10 * i;


                output.Add(new Coordinate2D(t, bernsteinPolynomials[iteration](t)));

                Coordinate2D calcedCoord;
            }
            return formatOutput(output);
        }

        public List<Coordinate2D> GetDerivationControlPoints()
        {
            // throw exception if controlpoints is empty
            if (this._controlPoints.Count == 0)
            {
                throw new InvalidOperationException("No control points");
            }
            // for ever controlpoint, calculate the derivation point
            List<Coordinate2D> derivationPoints = new();
            for (int i = 0; i < _controlPoints.Count - 1; i++)
            {
                Coordinate2D derivationPoint = new Coordinate2D((_controlPoints[i + 1].X - _controlPoints[i].X), (_controlPoints[i + 1].Y - _controlPoints[i].Y), 1);
                derivationPoints.Add(derivationPoint);
            }
            return derivationPoints;
        }

        public Coordinate2D GetDerivationCurvePoint(float t)
        {
            // check if t is between -1 and 2
            if (t < -1 || t > 2)
            {
                throw new ArgumentOutOfRangeException(nameof(t));
            }
            // throw exception if controlpoints are less than 2
            if (this._controlPoints.Count < 2)
            {
                throw new InvalidOperationException("Not enough control points");
            }
            throw new NotImplementedException();
        }

        private List<Coordinate2D> formatOutput(List<Coordinate2D> input)
        {
            // Format the output of the control points to fit the screen
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

        public List<Coordinate2D> IncreaseControlPoints()
        {
            List<Coordinate2D> newControlPoints = new List<Coordinate2D>();
            int n = ControlPoints.Count - 1;

            // calculation of the new controlpoints
            newControlPoints.Add(ControlPointsVec[0]);
            for(float i = 1; i < ControlPointsVec.Count; i++)
            {
                float first = (i / (n + 1));
                float second = (1 - (i / (n + 1)));
                newControlPoints.Add(ControlPointsVec[(int) i - 1] * first + ControlPointsVec[(int) i] * second);
            }
            newControlPoints.Add(ControlPointsVec[ControlPointsVec.Count - 1]);

            // overwriting controlpoint buffers
            ControlPoints.Clear();
            ControlPointsVec.Clear();

            for(int i = 0; i < newControlPoints.Count; i++)
            {
                ControlPoints.Add(new Coordinate2D(newControlPoints[i], true));
                ControlPointsVec.Add(newControlPoints[i]);
            }

            return ControlPoints;
        }

        public List<List<Coordinate2D>> SplitCurve(List<float> ts)
        {
            List<List<Coordinate2D>> output = new();
            List<float> tmpTs;
            for (int i = 0; i < ts.Count; i++)
            {
                float lowerEnd = i == 0 ? 0f : ts[i - 1];
                float upperEnd = ts[i];

                output.Add(new List<Coordinate2D>());

                for(int e = 0; e <= ControlPointsVec.Count - 1; e++)
                {
                    tmpTs = new List<float>();
                    for (int j = 0; j < e; j++) tmpTs.Add(lowerEnd);
                    for(int j = ControlPointsVec.Count - 1; j > e; j--) tmpTs.Add(upperEnd);
                    output[i].Add(CalcPoint(tmpTs));
                }
            }

            output.Add(new List<Coordinate2D>());

            for (int e = 0; e <= ControlPointsVec.Count - 1; e++)
            {
                tmpTs = new List<float>();
                for (int j = 0; j < e; j++) tmpTs.Add(ts[ts.Count - 1]);
                for (int j = ControlPointsVec.Count - 1; j > e; j--) tmpTs.Add(1f);
                output[ts.Count].Add(CalcPoint(tmpTs));
            }

            //output.Add(new List<Coordinate2D>());

            //tmpTs = new List<float> { ts[ts.Count - 1], ts[ts.Count - 1], ts[ts.Count - 1] };
            //output[ts.Count].Add(CalcPoint(tmpTs));
            //tmpTs = new List<float> { ts[ts.Count - 1], ts[ts.Count - 1], 1f};
            //output[ts.Count].Add(CalcPoint(tmpTs));
            //tmpTs = new List<float> { ts[ts.Count - 1], 1f, 1f};
            //output[ts.Count].Add(CalcPoint(tmpTs));
            //tmpTs = new List<float> { 1f, 1f, 1f };
            //output[ts.Count].Add(CalcPoint(tmpTs));



            return output;

        }

        private Coordinate2D CalcPoint(List<float> ts)
        {
            int zeroes = ts.RemoveAll(t => t == 0f);
            int ones = ts.RemoveAll(t => t == 1f);

            if (ts.Count == 0)
                return ControlPointsVec[ones];

            float currT = ts[0];
            ts.RemoveAt(0);

            for (int i = 0; i < zeroes; i++) ts.Add(0);
            for(int i = 0; i < ones; i++) ts.Add(1);

            List<float> param1 = new();
            param1.AddRange(ts);
            param1.Add(0f);

            List<float> param2 = new();
            param2.AddRange(ts);
            param2.Add(1f);


            return (1 - currT) * CalcPoint(param1)  +  currT * CalcPoint(param2);

        }
    }
}
