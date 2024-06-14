﻿using System;
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

        internal void AddVertex(Coordinate2D coord)
        {
            if(coord == null || !coord.Type.Equals("Point")) 
            {
                throw new ArgumentException("Vertex is not a valid point.");
            }

            _vertices.Add(new Coordinate2D(coord.X / _globalZoom - _globalDisplacement.X, coord.Y / _globalZoom - _globalDisplacement.Y, 1));

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
            float centerX = 0;
            float centerY = 0;
            foreach (Coordinate2D vertex in _vertices)
            {
                centerX += vertex.X;
                centerY += vertex.Y;
            }
            centerX /= _vertices.Count;
            centerY /= _vertices.Count;
            Coordinate2D curDisplacement = (_globalDisplacement - _centerDisplacement);
            Coordinate2D objectCenter = new(centerX + curDisplacement.X, centerY + curDisplacement.Y, 1);

            Coordinate2D displacement = new Coordinate2D(0,0,1) - objectCenter;
            _globalDisplacement += displacement;
        }

        internal void AddEdge(Coordinate2D start, Coordinate2D end) 
        {
            _edges.Add(new Edge2D(start, end));
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

            Graphics g = _renderTarget.CreateGraphics();
            g.Clear(Color.White);

            Coordinate2D boxCenterVector = new Coordinate2D(_renderTarget.Width / 2.0f, _renderTarget.Height / 2.0f, 0);
            Coordinate2D xAxisStart = boxCenterVector;
            Coordinate2D yAxisStart = boxCenterVector;
            Coordinate2D xAxisEnd = xAxisStart + new Coordinate2D(50, 0, 0);
            Coordinate2D yAxisEnd = yAxisStart + new Coordinate2D(0, 50, 0);
            g.DrawLine(xAxisPen, xAxisStart.X, xAxisStart.Y, xAxisEnd.X, xAxisEnd.Y);
            g.DrawLine(yAxisPen, yAxisStart.X, yAxisStart.Y, yAxisEnd.X, yAxisEnd.Y);

            if (_globalDisplacement == null)
            {
                _globalDisplacement = new Coordinate2D();
            }

            foreach(Coordinate2D vertex in _vertices)
            {
                Pen vertexPen = _selectedVertices.Contains(vertex) ? selectedPen : unselectedPen;
                Coordinate2D curPoint = (vertex + _globalDisplacement) * _globalZoom;
                float transformedRadius = _vertexRadius * _globalZoom;
                g.DrawEllipse(vertexPen, curPoint.X - transformedRadius, curPoint.Y - transformedRadius, transformedRadius * 2, transformedRadius * 2);
            }

            foreach(Edge2D edge in _edges)
            {
                Coordinate2D start = (edge.Start + _globalDisplacement) * _globalZoom;
                Coordinate2D end = (edge.End + _globalDisplacement) * _globalZoom;
                g.DrawLine(edgePen, start.X, start.Y, end.X, end.Y);
            }
        }
    }
}
