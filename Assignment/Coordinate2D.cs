using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public struct Coordinate2D
    {
        public float x, y, epsilon;

        public Coordinate2D(float x, float y, int epsilon)
        {
            this.x = x;
            this.y = y;
            this.epsilon = epsilon;
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
            string type;
            switch (epsilon)
            {
                case 0:
                    type = "Vector";
                    break;
                case 1:
                    type = "Point";
                    break;
                default:
                    type = "Invalid";
                    break;
            }

            string output = type + " X: " + x + " Y: " + y;
            return output;
        }
    }
}
