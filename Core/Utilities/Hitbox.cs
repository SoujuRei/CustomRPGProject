using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProjectRPG.Core.Utilities
{
    public struct Hitbox

    {
        public Vector2D Position { get; set; }
        public Vector2D Size { get; }

        public Hitbox(Vector2D position, Vector2D size)
        {
            Position = position;
            Size = size;
        }

        public bool Intersects(Hitbox other)
        {
            return !(Position.X + Size.X < other.Position.X ||
                     Position.X > other.Position.X + other.Size.X ||
                     Position.Y + Size.Y < other.Position.Y ||
                     Position.Y > other.Position.Y + other.Size.Y);
        }
    }

}

