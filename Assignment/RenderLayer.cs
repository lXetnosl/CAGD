using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Assignment
{
    internal class RenderLayer
    {
        // Zoom and rendering variables.
        private int _globalZoom = 20;
        private float _vertexRadius = 0.5f;
        private float _penWidth = 2.0f;

        // Render target and displacement variables.
        private PictureBox _renderTarget;
        private Coordinate2D _centerDisplacement;
        private Coordinate2D _globalDisplacement;

        // Vertex and edge lists.
        private List<Coordinate2D> _vertices;
        private List<Coordinate2D> _selectedVertices;
        private List<Edge2D> _edges;
        private List<List<Coordinate2D>> _splittedPoints;

        // Bezier curve variables and flags.
        internal bool useDeCasteljau = true;
        internal bool ShowBezierCheck = false;
        internal bool ShowControlPointsCheck = false;
        internal bool ShowBernsteinPolynoms = false;
        internal bool ShowDerivation = false;
        internal int DerivationDegree = 1;

        private Bezier bezier;
        internal float T = 0.5f;
        internal float VertexRadius
        {
            get { return _vertexRadius; }
            set { _vertexRadius = value; }
        }

        // Zoom property for rendering.
        internal int Zoom
        {
            get { return _globalZoom; }
            set 
            { 
                _globalZoom = value;
                Coordinate2D boxCenterVector = new Coordinate2D(_renderTarget.Width / 2.0f, _renderTarget.Height / 2.0f, 0);
                _globalDisplacement -= _centerDisplacement;
                _centerDisplacement = boxCenterVector / _globalZoom;
                _globalDisplacement += _centerDisplacement;
            }
        }

        internal Coordinate2D Displacement
        {
            get { return _globalDisplacement * _globalZoom; }
        }

        internal int SelectedCount
        {
            // Get the number of selected vertices.
            get { return _selectedVertices.Count; }
        }

        internal List<Coordinate2D> SelectedVertices
        {
            get { return _selectedVertices; }
        }

        internal RenderLayer(PictureBox renderTarget)
        {
            // Initialize the render layer.
            _renderTarget = renderTarget;
            _vertices = new List<Coordinate2D>();
            _selectedVertices = new List<Coordinate2D>();
            _edges = new List<Edge2D>();

            Coordinate2D boxCenterVector = new Coordinate2D(_renderTarget.Width / 2.0f, _renderTarget.Height / 2.0f, 0);
            _centerDisplacement = boxCenterVector / _globalZoom;
            _globalDisplacement = _centerDisplacement;
        }

        internal RenderLayer(PictureBox renderTarget, string path) : this(renderTarget)
        {
            // Initialize the render layer with vertices from a file.
            AddFromFile(path);
        }

        internal void DrawLine(Pen pen, Coordinate2D start, Coordinate2D end, Graphics graphics)
        {
            if (Math.Abs(start.X) < 1000000 && Math.Abs(start.Y) < 1000000 && Math.Abs(end.X) < 1000000 && Math.Abs(end.Y) < 1000000)
            {
                graphics.DrawLine(pen, start.X, start.Y, end.X, end.Y);
            }
        }

        internal void DrawEllipse(Pen pen, Coordinate2D center, float radius, Graphics graphics)
        {
            // make radius smaller if zoom is too big
            radius = radius / _globalZoom * 20;
            if (Math.Abs(center.X) < 1000000 && Math.Abs(center.Y) < 1000000)
            {
                graphics.DrawEllipse(pen, center.X - radius, center.Y - radius, radius * 2, radius * 2);
            }
        }

        internal void AddVertex(Coordinate2D coord, bool isWorldSpace = true)
        {
            // Add a vertex to the list of vertices.
            if (coord == null || !coord.Type.Equals("Point")) 
            {
                throw new ArgumentException("Vertex is not a valid point.");
            }

            // isWorldSpace is used to determine if the vertex was added by mouse click or by split method.
            // If the vertex was added by mouse click, it is added to the end of the list.
            // If the vertex was added by split method, it is added between the two selected vertices.
            if (!isWorldSpace)
            {
                int idx = _vertices.FindIndex(x => x.Equals(_selectedVertices[0]));
                int idx2 = _vertices.FindIndex(x => x.Equals(_selectedVertices[1]));
                int index = idx > idx2 ? idx : idx2;
                _vertices.Insert(index, coord);
            }
            else
            {
                _vertices.Add(new Coordinate2D(coord.X / _globalZoom - _globalDisplacement.X, coord.Y / _globalZoom - _globalDisplacement.Y, 1));
            }
        }

        internal void DeleteVertexAt(Coordinate2D vertex)
        {
            // Delete a vertex at the specified location.
            Coordinate2D? toBeDeleted = GetVertexAt(vertex, false);
            if(toBeDeleted == null)
            {
                return;
            }

            _vertices.Remove(toBeDeleted);
            _selectedVertices.Remove(toBeDeleted);
        }

        internal void DeleteSelected()
        {
            // Delete all selected vertices.
            foreach (Coordinate2D vertex in _selectedVertices)
            {
                _vertices.Remove(vertex);
            }
            _selectedVertices.Clear();
        }

        internal void ClearVertices()
        {
            // Delete all vertices.
            _vertices.Clear();
            _selectedVertices.Clear();
            // if _splittedPoints is not null, clear it.
            if (_splittedPoints != null)
            {
                _splittedPoints.Clear();
            }
        }

        internal void MoveSelected(Coordinate2D moveVector)
        {
            // Move all selected vertices by the specified vector.
            for (int i = 0; i < _selectedVertices.Count; i++)
            {
                int vertIdx = _vertices.FindIndex(x => x.Equals(_selectedVertices[i]));
                if(vertIdx < 0)
                {
                    continue;
                }
                _vertices[vertIdx] += moveVector;

                for (int j = 0; j < _edges.Count; j++)
                {
                    if (_selectedVertices[i].Equals(_edges[j].Start))
                    {
                        _edges[j].Start = _vertices[vertIdx];
                    }
                    else if (_selectedVertices[i].Equals(_edges[j].End))
                    {
                        _edges[j].End = _vertices[vertIdx];
                    }
                }

                _selectedVertices[i] = _vertices[vertIdx];
            }
        }

        internal bool SplitSelected(float splitPct = 0.5f)
        {
            // Split the edge between the two selected vertices at the specified percentage and create a new vertex.
            if (_selectedVertices.Count != 2)
            {
                throw new NotImplementedException("Operation not implemented for selections below or above 2 vertices.");
            }

            Coordinate2D newVertex = new(0,0,1);
            newVertex.X = _selectedVertices[0].X * (1 - splitPct) + _selectedVertices[1].X * splitPct;
            newVertex.Y = _selectedVertices[0].Y * (1 - splitPct) + (_selectedVertices[1].Y * splitPct);
            AddVertex(newVertex, false);

            return true;
        }

        internal Coordinate2D? GetVertexAt(Coordinate2D coord, bool select = false) 
        {
            // Find the vertex at the specified mouse click location.
            Coordinate2D transformedCoord = new Coordinate2D(coord.X / _globalZoom - _globalDisplacement.X, coord.Y / _globalZoom - _globalDisplacement.Y, 1);
            foreach (Coordinate2D vertex in _vertices) 
            {
                if( transformedCoord.X - _vertexRadius <= vertex.X &&
                    transformedCoord.X + _vertexRadius >= vertex.X &&
                    transformedCoord.Y - _vertexRadius <= vertex.Y &&
                    transformedCoord.Y + _vertexRadius >= vertex.Y )
                {
                    if (select)
                    {
                        if (!_selectedVertices.Contains(vertex))
                        {
                            _selectedVertices.Add(vertex);
                        }
                        else
                        {
                            _selectedVertices.Remove(vertex);
                        }
                    }
                    return vertex;
                }
            }
            if (select)
            {
                _selectedVertices.Clear();
            }
            return null;
        }

        internal void CenterObject()
        {
            // Center the object in the render target.
            if (_vertices.Count <= 0)
            {
                return;
            }

            Coordinate2D upperLeftBound = new(float.PositiveInfinity, float.PositiveInfinity, 0);
            Coordinate2D lowerRightBound = new(float.NegativeInfinity, float.NegativeInfinity, 0);
            foreach (Coordinate2D vertex in _vertices)
            {
                if( vertex.X < upperLeftBound.X )
                {
                    upperLeftBound.X = vertex.X;
                }
                else if( vertex.X > lowerRightBound.X )
                {
                    lowerRightBound.X = vertex.X;
                }
                if(vertex.Y < upperLeftBound.Y )
                {  
                    upperLeftBound.Y = vertex.Y; 
                }
                else if(vertex.Y > lowerRightBound.Y )
                {
                    lowerRightBound.Y = vertex.Y;
                }
            }
            Coordinate2D localCenter = (upperLeftBound + lowerRightBound) / 2.0f;
            Coordinate2D curDisplacement = (_globalDisplacement - _centerDisplacement);
            Coordinate2D globalCenter = new(localCenter.X + curDisplacement.X, localCenter.Y + curDisplacement.Y, 0);

            Coordinate2D displacement = new Coordinate2D() - globalCenter;
            _globalDisplacement += displacement;
        }

        internal void AddFromFile(string path)
        {
            // Overwrite current vertices with vertices from a file.
            ObjectReader reader = new();
            _vertices = reader.ReadFile(path);
            _selectedVertices.Clear();
            _edges.Clear();
            for(int i = 0; i < _vertices.Count - 1; i++) 
            {
                _edges.Add(new Edge2D(_vertices[i], _vertices[i + 1]));
            }
        }

        internal void Render()
        {
            // Render the vertices and edges.
            // Initialize pens for rendering.
            Pen unselectedPen = new Pen(Color.Magenta)
            {
                Width = _penWidth
            };
            Pen selectedPen = new Pen(Color.FromArgb(255,191,0))
            {
                Width = _penWidth
            };
            Pen edgePen = new Pen(Color.Black)
            {
                Width = _penWidth
            };
            Pen xAxisPen = new Pen(Color.Red)
            {
                Width = 5
            };
            xAxisPen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            Pen yAxisPen = new Pen(Color.Blue)
            {
                Width = 5
            };
            yAxisPen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            // Clear the render target.
            Graphics graphics = _renderTarget.CreateGraphics();
            graphics.Clear(Color.White);
            // Draw the axes.
            Coordinate2D boxCenterVector = new Coordinate2D(_renderTarget.Width / 2.0f, _renderTarget.Height / 2.0f, 0);
            Coordinate2D xAxisStart = boxCenterVector;
            Coordinate2D yAxisStart = boxCenterVector;
            Coordinate2D xAxisEnd = xAxisStart + new Coordinate2D(50, 0, 0);
            Coordinate2D yAxisEnd = yAxisStart + new Coordinate2D(0, 50, 0);
            DrawLine(xAxisPen, xAxisStart, xAxisEnd, graphics);
            DrawLine(yAxisPen, yAxisStart, yAxisEnd, graphics);

            if (_globalDisplacement == null)
            {
                _globalDisplacement = new Coordinate2D();
            }

            // if there are no vertices, return and skip rendering vertices edges and bezier curve.
            if (_vertices.Count == 0)
            {
                return;
            }

            // Draw the vertices.
            foreach (Coordinate2D vertex in _vertices)
            {
                Pen vertexPen = _selectedVertices.Contains(vertex) ? selectedPen : unselectedPen;
                Coordinate2D curPoint = new Coordinate2D(vertex.X * _globalZoom, vertex.Y * _globalZoom, 1) + (_globalDisplacement * _globalZoom);
                float transformedRadius = _vertexRadius * _globalZoom;
                DrawEllipse(vertexPen, curPoint, transformedRadius, graphics);
            }

            // Draw the edges.
            _edges.Clear();
            for (int i = 0; i < _vertices.Count - 1; i++)
            {
                _edges.Add(new Edge2D(_vertices[i], _vertices[i + 1]));
            }
            foreach (Edge2D edge in _edges)
            {
                Coordinate2D start = new Coordinate2D(edge.Start.X * _globalZoom, edge.Start.Y * _globalZoom, 1) + (_globalDisplacement * _globalZoom);
                Coordinate2D end = new Coordinate2D(edge.End.X * _globalZoom, edge.End.Y * _globalZoom, 1) + (_globalDisplacement * _globalZoom);
                DrawLine(edgePen, start, end, graphics);
            }

            // Draw the Bezier curve with the selected method.
            bezier = useDeCasteljau ? new Bezier_DeCasteljau(_vertices) : new Bezier_Bernstein(_vertices);
            if (ShowBezierCheck)
            {
                List<Coordinate2D> curvePoints = new();
                for (float t = 0; t <= 1; t += 0.01f)
                {
                    Coordinate2D curvePoint = bezier.GetCurvePoint(t);
                    curvePoints.Add(curvePoint + _globalDisplacement);
                }
                for (int i = 0; i < curvePoints.Count - 1; i++)
                {
                    Coordinate2D start = new Coordinate2D(curvePoints[i].X * _globalZoom, curvePoints[i].Y * _globalZoom, 1);
                    Coordinate2D end = new Coordinate2D(curvePoints[i + 1].X * _globalZoom, curvePoints[i + 1].Y * _globalZoom, 1);
                    DrawLine(edgePen, start, end, graphics);
                }
            }

            // Initialize pens for control points and edges.
            List<Pen> ctrlEdgePens = new List<Pen>();
            ctrlEdgePens.Add(new Pen(Color.FromArgb(0, 255, 0), _penWidth));
            ctrlEdgePens.Add(new Pen(Color.FromArgb(0, 0, 255), _penWidth));
            ctrlEdgePens.Add(new Pen(Color.FromArgb(0, 255, 255), _penWidth));
            ctrlEdgePens.Add(new Pen(Color.FromArgb(255, 128, 0), _penWidth));

            // Draw the control points and edges.
            if (ShowControlPointsCheck)
            {
                bezier.GetCurvePoint(T);

                List<Coordinate2D> controlPoints = bezier.GetControlPoints(1);
                List<Edge2D> controlEdges = new();
                int iteration = 1;
                while (controlPoints.Count > 1)
                {
                    controlPoints = bezier.GetControlPoints(iteration);
                    foreach (Coordinate2D controlPoint in controlPoints)
                    {
                        Coordinate2D curPoint = new Coordinate2D(controlPoint.X * _globalZoom, controlPoint.Y * _globalZoom, 1) + (_globalDisplacement * _globalZoom);
                        float transformedRadius = _vertexRadius * _globalZoom;
                        if (controlPoints.Count == 1)
                        {
                            DrawEllipse(new Pen(Color.FromArgb(255, 0, 0), _penWidth+2), curPoint, transformedRadius, graphics);
                        }
                        else
                        {
                            DrawEllipse(ctrlEdgePens[(iteration + 1) % 4], curPoint , transformedRadius, graphics);
                        }
                    }
                    iteration++;

                    controlEdges.Clear();
                    for (int j = 0; j < controlPoints.Count - 1; j++)
                    {
                        controlEdges.Add(new Edge2D(controlPoints[j], controlPoints[j + 1]));
                    }
                    foreach (Edge2D edge in controlEdges)
                    {
                        Coordinate2D start = new Coordinate2D(edge.Start.X * _globalZoom, edge.Start.Y * _globalZoom, 1) + (_globalDisplacement * _globalZoom);
                        Coordinate2D end = new Coordinate2D(edge.End.X * _globalZoom, edge.End.Y * _globalZoom, 1) + (_globalDisplacement * _globalZoom);
                        DrawLine(ctrlEdgePens[iteration%4], start, end, graphics);
                    }
                }
            }

            // Draw the bernstein polynoms.
            if (ShowBernsteinPolynoms)
            {
                for (int i = 0; i < _vertices.Count; i++)
                {
                    List<Coordinate2D> controlPoints = bezier.GetControlPoints(i);
                    List<Edge2D> controlEdges = new();

                    if (controlPoints == null)
                        break;

                    for (int j = 0; j < controlPoints.Count - 1; j++)
                    {
                        controlEdges.Add(new Edge2D(controlPoints[j], controlPoints[j + 1]));
                    }
                    foreach (Edge2D edge in controlEdges)
                    {
                        Coordinate2D start = new Coordinate2D(edge.Start.X * _globalZoom, edge.Start.Y * _globalZoom, 1) + (_globalDisplacement * _globalZoom);
                        Coordinate2D end = new Coordinate2D(edge.End.X * _globalZoom, edge.End.Y * _globalZoom, 1) + (_globalDisplacement * _globalZoom);
                        DrawLine(ctrlEdgePens[i % 4], start, end, graphics);
                    }
                }
            }

            // Draw the derivation of the bezier curve.
            if (ShowDerivation)
            {
                // If there is no bezier curve, throw an exception.
                if (bezier == null)
                {
                    throw new InvalidOperationException("No bezier curve");
                }
                // return if there are no control points
                if (bezier.GetControlPoints(0).Count <= 1)
                {
                    return;
                }

                



                // Choose Solution 1 or 2 for the derivation of the bezier curve.
                bool chooseFirstSolution = false;

                if (chooseFirstSolution)    // Solution 1 (complex)
                {
                    // for 0 to 1, step size 0.01, draw the derivation of the bezier curve
                    List<Coordinate2D> derivationCurvePoints = new();
                    for (float t = 0; t <= 1; t += 0.01f)
                    {
                        bezier.GetCurvePoint(t);
                        Coordinate2D derivationPoint = bezier.GetDerivationCurvePoint(t);
                        derivationCurvePoints.Add(derivationPoint + _globalDisplacement);
                    }
                    for (int i = 0; i < derivationCurvePoints.Count - 1; i++)
                    {
                        Coordinate2D start = new Coordinate2D(derivationCurvePoints[i].X * _globalZoom, derivationCurvePoints[i].Y * _globalZoom, 1);
                        Coordinate2D end = new Coordinate2D(derivationCurvePoints[i + 1].X * _globalZoom, derivationCurvePoints[i + 1].Y * _globalZoom, 1);
                        // catch overflow error when drawing bezier curve
                        if (Math.Abs(start.X) < 1000000 && Math.Abs(start.Y) < 1000000 && Math.Abs(end.X) < 1000000 && Math.Abs(end.Y) < 1000000)
                        {
                            DrawLine(edgePen, start, end, graphics);
                        }
                    }
                }
                else    // Solution 2 (simple)
                {
                    // if derivation degree is 0, return
                    if (DerivationDegree == 0)
                    {
                        return;
                    }
                    // if derivation degree is greater than the degree of the bezier curve, return
                    if (DerivationDegree > _vertices.Count - 1)
                    {
                        return;
                    }


                    List<Coordinate2D> derivationControlPoints = bezier.GetDerivationControlPoints();
                    List<Edge2D> controlEdges = new();

                    Bezier DerivedBezier = useDeCasteljau ? new Bezier_DeCasteljau(derivationControlPoints) : new Bezier_Bernstein(derivationControlPoints);
                    for (int i= 0; i < DerivationDegree-1; i++)
                    {
                        derivationControlPoints = DerivedBezier.GetDerivationControlPoints();
                        DerivedBezier = useDeCasteljau ? new Bezier_DeCasteljau(derivationControlPoints) : new Bezier_Bernstein(derivationControlPoints);
                    }

                    // draw control points of the derivation
                    foreach (Coordinate2D derivationControlPoint in derivationControlPoints)
                    {
                        Coordinate2D curPoint = new Coordinate2D(derivationControlPoint.X * _globalZoom, derivationControlPoint.Y * _globalZoom, 1) + (_globalDisplacement * _globalZoom);
                        float transformedRadius = _vertexRadius * _globalZoom;
                        DrawEllipse(new Pen(Color.FromArgb(255, 0, 0), _penWidth), curPoint, transformedRadius, graphics);
                    }
                    // draw edges between control points
                    for (int j = 0; j < derivationControlPoints.Count - 1; j++)
                    {
                        controlEdges.Add(new Edge2D(derivationControlPoints[j], derivationControlPoints[j + 1]));
                    }
                    foreach (Edge2D edge in controlEdges)
                    {
                        Coordinate2D start = new Coordinate2D(edge.Start.X * _globalZoom, edge.Start.Y * _globalZoom, 1) + (_globalDisplacement * _globalZoom);
                        Coordinate2D end = new Coordinate2D(edge.End.X * _globalZoom, edge.End.Y * _globalZoom, 1) + (_globalDisplacement * _globalZoom);
                        DrawLine(edgePen, start, end, graphics);
                    }

                    // draw the derivation of the bezier curve
                    List<Coordinate2D> curvePoints = new();
                    for (float t = 0; t <= 1; t += 0.01f)
                    {
                        Coordinate2D curvePoint = DerivedBezier.GetCurvePoint(t);
                        curvePoints.Add(curvePoint + _globalDisplacement);
                    }
                    for (int i = 0; i < curvePoints.Count - 1; i++)
                    {
                        Coordinate2D start = new Coordinate2D(curvePoints[i].X * _globalZoom, curvePoints[i].Y * _globalZoom, 1);
                        Coordinate2D end = new Coordinate2D(curvePoints[i + 1].X * _globalZoom, curvePoints[i + 1].Y * _globalZoom, 1);
                        // catch overflow error when drawing bezier curve
                        if (Math.Abs(start.X) < 1000000 && Math.Abs(start.Y) < 1000000 && Math.Abs(end.X) < 1000000 && Math.Abs(end.Y) < 1000000)
                        {
                            DrawLine(edgePen, start, end, graphics);
                        }
                    }
                }
            }

            // Draw the bernstein polynoms.
            if (_splittedPoints != null)
            {
                for(int i = 0; i < _splittedPoints.Count; i++)
                {
                    foreach (Coordinate2D controlPoint in _splittedPoints[i])
                    {
                        Coordinate2D curPoint = new Coordinate2D(controlPoint.X * _globalZoom, controlPoint.Y * _globalZoom, 1) + (_globalDisplacement * _globalZoom);
                        float transformedRadius = _vertexRadius * _globalZoom;
                        DrawEllipse(ctrlEdgePens[i], curPoint, transformedRadius, graphics);
                    }
                }
            }
        }

        internal void increaseControlPoints()
        {
            // Increase the number of control points by 1.
            // If there are less than 2 control points, return.
            if (_vertices.Count <= 1)
            {
                return;
            }
            // If there is no next iteration, throw an exception.
            if (bezier == null)
            {
                throw new InvalidOperationException("No next iteration");
            }
            _vertices = bezier.IncreaseControlPoints();
        }

        internal void SplitCurve(List<float> ts)
        {
            // Split the bezier curve at the specified t values.
            // If there is no bezier curve, throw an exception.
            if (bezier == null)
            {
                throw new InvalidOperationException("No bezier curve");
            }
            _splittedPoints = bezier.SplitCurve(ts);
        }
    }
}
