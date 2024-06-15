using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment
{
    internal class RenderLayer
    {
        private int _globalZoom = 20;
        private float _vertexRadius = 0.5f;
        private float _penWidth = 2.0f;

        private PictureBox _renderTarget;
        private Coordinate2D _centerDisplacement;
        private Coordinate2D _globalDisplacement;
        private List<Coordinate2D> _vertices;
        private List<Coordinate2D> _selectedVertices;
        private List<Edge2D> _edges;

        internal float VertexRadius
        {
            get { return _vertexRadius; }
            set { _vertexRadius = value; }
        }

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
            get { return _selectedVertices.Count; }
        }

        internal RenderLayer(PictureBox renderTarget)
        {
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
            AddFromFile(path);
        }

        internal void AddVertex(Coordinate2D coord, bool isWorldSpace = true)
        {
            if(coord == null || !coord.Type.Equals("Point")) 
            {
                throw new ArgumentException("Vertex is not a valid point.");
            }

            if (!isWorldSpace)
            {
                _vertices.Add(coord);
            }
            else
            {
                _vertices.Add(new Coordinate2D(coord.X / _globalZoom - _globalDisplacement.X, coord.Y / _globalZoom - _globalDisplacement.Y, 1));
            }

            foreach (Coordinate2D vertex in _selectedVertices)
            {
                _edges.Add(new Edge2D(vertex, _vertices[^1]));
            }
        }

        internal void DeleteVertexAt(Coordinate2D vertex)
        {
            Coordinate2D? toBeDeleted = GetVertexAt(vertex, false);
            if(toBeDeleted == null)
            {
                return;
            }

            DeleteVertex(toBeDeleted);
            _selectedVertices.Remove(toBeDeleted);
        }

        internal void DeleteSelected()
        {
            foreach (Coordinate2D vertex in _selectedVertices)
            {
                DeleteVertex(vertex);
            }
            _selectedVertices.Clear();
        }

        private void DeleteVertex(Coordinate2D vertex)
        {
            List<Edge2D> connectedEdges = new List<Edge2D>();
            foreach (Edge2D edge in _edges)
            {
                if(edge.Start == vertex || edge.End == vertex)
                {
                    connectedEdges.Add(edge);
                }
            }
            foreach (Edge2D edge in connectedEdges)
            {
                _edges.Remove(edge);
            }
            _vertices.Remove(vertex);
        }

        internal void MoveSelected(Coordinate2D moveVector)
        {
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
            if(_selectedVertices.Count != 2)
            {
                throw new NotImplementedException("Operation not implemented for selections below or above 2 vertices.");
            }

            if (!RemoveEdge(_selectedVertices[0], _selectedVertices[1]))
            {
                return false;
            }

            Coordinate2D newVertex = new(0,0,1);
            newVertex.X = _selectedVertices[0].X * (1 - splitPct) + _selectedVertices[1].X * splitPct;
            newVertex.Y = _selectedVertices[0].Y * (1 - splitPct) + (_selectedVertices[1].Y * splitPct);
            AddVertex(newVertex, false);

            return true;
        }

        internal Coordinate2D? GetVertexAt(Coordinate2D coord, bool select = false) 
        {
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
            Coordinate2D upperLeftBound = new();
            Coordinate2D lowerRightBound = new();
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
            Coordinate2D globalCenter = new(localCenter.X + curDisplacement.X, localCenter.Y + curDisplacement.Y, 1);

            Coordinate2D displacement = new Coordinate2D(0,0,1) - globalCenter;
            _globalDisplacement += displacement;
        }

        internal void AddEdge(Edge2D edge)
        {
            _edges.Add(edge);
        }

        internal void AddEdge(Coordinate2D start, Coordinate2D end) 
        {
            AddEdge(new Edge2D(start, end));
        }

        internal bool RemoveEdge(Edge2D edge)
        {
            foreach(Edge2D curEdge in _edges)
            {
                if( curEdge.Start.Equals(edge.Start) && curEdge.End.Equals(edge.End) ||
                    curEdge.Start.Equals(edge.End) && curEdge.End.Equals(edge.Start))
                {
                    _edges.Remove(curEdge);
                    return true;
                }
            }

            return false;
        }

        internal bool RemoveEdge(Coordinate2D start, Coordinate2D end)
        {
            return RemoveEdge(new Edge2D(start, end));
        }

        internal void AddFromFile(string path)
        {
            ObjectReader reader = new();
            _vertices = reader.ReadFile(path);
            _edges.Clear();
            for(int i = 0; i < _vertices.Count - 1; i++) 
            {
                AddEdge(_vertices[i], _vertices[i+1]);
            }
        }

        internal void Render()
        {
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

            Graphics graphics = _renderTarget.CreateGraphics();
            graphics.Clear(Color.White);

            Coordinate2D boxCenterVector = new Coordinate2D(_renderTarget.Width / 2.0f, _renderTarget.Height / 2.0f, 0);
            Coordinate2D xAxisStart = boxCenterVector;
            Coordinate2D yAxisStart = boxCenterVector;
            Coordinate2D xAxisEnd = xAxisStart + new Coordinate2D(50, 0, 0);
            Coordinate2D yAxisEnd = yAxisStart + new Coordinate2D(0, 50, 0);
            graphics.DrawLine(xAxisPen, xAxisStart.X, xAxisStart.Y, xAxisEnd.X, xAxisEnd.Y);
            graphics.DrawLine(yAxisPen, yAxisStart.X, yAxisStart.Y, yAxisEnd.X, yAxisEnd.Y);

            if (_globalDisplacement == null)
            {
                _globalDisplacement = new Coordinate2D();
            }

            foreach(Coordinate2D vertex in _vertices)
            {
                Pen vertexPen = _selectedVertices.Contains(vertex) ? selectedPen : unselectedPen;
                Coordinate2D curPoint = (vertex + _globalDisplacement) * _globalZoom;
                float transformedRadius = _vertexRadius * _globalZoom;
                graphics.DrawEllipse(vertexPen, curPoint.X - transformedRadius, curPoint.Y - transformedRadius, transformedRadius * 2, transformedRadius * 2);
            }

            foreach(Edge2D edge in _edges)
            {
                Coordinate2D start = (edge.Start + _globalDisplacement) * _globalZoom;
                Coordinate2D end = (edge.End + _globalDisplacement) * _globalZoom;
                graphics.DrawLine(edgePen, start.X, start.Y, end.X, end.Y);
            }
        }
    }
}
