using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    public class RectangleF
    {
        public float X = 0;
        public float Y = 0;
        public float Width = 0;
        public float Height = 0;
        public Vector2 Location { get => new Vector2(X, Y); set { X = value.X; Y = value.Y; } }
        public Vector2 Size { get => new Vector2(Width, Height); set { Width = value.X; Height = value.Y; } }

        public Vector2 Center { get => new Vector2(X - Width / 2, Y - Height / 2); }

        public RectangleF()
        {
        }

        public RectangleF(Vector2 location, Vector2 size)
        {
            Location = location;
            Size = size;
        }

        public RectangleF(float x, float y, float with, float height)
        {
            X = x;
            Y = y;
            Width = with;
            Height = height;
        }

        public bool Contains(Vector2 point)
        {
            return ((point.X > X && point.X < X + Width) && (point.Y > Y && point.Y < Y + Height));
        }

        public bool Intersects(RectangleF rec)
        {
            return X < rec.X + rec.Width && X + Width > rec.X && Y < rec.Y + rec.Height && Y + Height > rec.Y;
        }

        public static implicit operator Rectangle(RectangleF rec)
        {
            return new Rectangle(rec.Location.ToPoint(), rec.Size.ToPoint());
        }

        public RectangleF Copy()
        {
            return new RectangleF(X, Y, Width, Height);
        }
    }
}