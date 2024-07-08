using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class Coordinate2D
    {
        private float x, y, epsilon;

        public float X
        {
            get { return x; }
            set { x = value; }
        }

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public string Type
        {
            get 
            {
                switch (epsilon) 
                {
                    case 0:
                        return "Vector";
                    case 1:
                        return "Point";
                    default:
                        return "Invalid";
                }
            }
        }

        public Coordinate2D(float x = 0, float y = 0, int epsilon = 0)
        {
            this.x = x;
            this.y = y;
            this.epsilon = epsilon;
        }

        public Coordinate2D(Coordinate2D oldPoint, bool switchType)
        {
            x = oldPoint.X;
            y = oldPoint.Y;
            epsilon = switchType ? (oldPoint.epsilon == 1 ? 0 : 1) : oldPoint.epsilon;
        }

        public static Coordinate2D operator +(Coordinate2D left, Coordinate2D right)
        {
            Coordinate2D result = new();
            result.x = left.x + right.x;
            result.y = left.y + right.y;
            result.epsilon = left.epsilon + right.epsilon;
            if (result.epsilon != 0 && result.epsilon != 1)
            {
                throw new ArgumentException("Subtraction result is not a valid point or vector.");
            }
            return result;
        }

        public static Coordinate2D operator -(Coordinate2D left, Coordinate2D right)
        {
            Coordinate2D result = new();
            result.x = left.x - right.x;
            result.y = left.y - right.y;
            result.epsilon = left.epsilon - right.epsilon;
            if (result.epsilon != 0 && result.epsilon != 1)
            {
                throw new ArgumentException("Subtraction result is not a valid point or vector.");
            }
            return result;
        }

        public static Coordinate2D operator *(float scalar, Coordinate2D coord)
        {
            Coordinate2D result = new();
            result.x = coord.x * scalar;
            result.y = coord.y * scalar;
            result.epsilon *= scalar;
            if (result.epsilon != 0 && result.epsilon != 1)
            {
                throw new ArgumentException("Scalar multiplication result is not a valid point or vector.");
            }
            return result;
        }

        public static Coordinate2D operator *(Coordinate2D coord, float scalar)
        {
            Coordinate2D result = scalar * coord;
            return result;
        }

        public static Coordinate2D operator /(Coordinate2D coord, float scalar)
        {
            if(scalar == 0)
            {
                throw new DivideByZeroException();
            }

            return coord * (1 / scalar);
        }

        public static float operator *(Coordinate2D left, Coordinate2D right)
        {
            if (left.epsilon != 0 || right.epsilon != 0)
            {
                throw new ArgumentException("Scalar product can only be calculated for vectors.");
            }
            float result = left.x * right.x + left.y * right.y;
            return result;
        }

        public override string ToString()
        {
            string output = Type + " X: " + x + " Y: " + y;
            return output;
        }
    }
}
