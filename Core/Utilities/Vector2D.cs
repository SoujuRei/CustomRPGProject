using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProjectRPG.Core.Utilities
{

    public struct Vector2D
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2D Add(Vector2D other) => new Vector2D(X + other.X, Y + other.Y);
        public Vector2D Scale(float factor) => new Vector2D(X * factor, Y * factor);
        public float DistanceTo(Vector2D other) =>
            (float)Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
    }
}