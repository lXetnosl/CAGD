﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    internal interface Bezier
    {
        Coordinate2D GetCurvePoint(float t);

        List<Coordinate2D> GetControlPoints(int iteration);
    }
}