using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    /// <summary>
    /// A struct representing rectangles in 2d space
    /// </summary>
    public struct RectangleF
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;
        public Vector2 Location { get => new Vector2(X, Y); set { X = value.X; Y = value.Y; } }
        public Vector2 Size { get => new Vector2(Width, Height); set { Width = value.X; Height = value.Y; } }
        public Vector2 Center { get => new Vector2(X - Width / 2, Y - Height / 2); }

        public RectangleF(float x, float y, float with, float height)
        {
            X = x;
            Y = y;
            Width = with;
            Height = height;
        }

        public RectangleF(Vector2 location, Vector2 size) : this(location.X, location.Y, size.X, size.Y)
        {
        }

        /// <summary>
        /// Method for checkig if a RectangleF contains a point
        /// </summary>
        /// <param name="point">The choordinates of the point</param>
        /// <returns>true if the rectangle contains the point and false if the point is outside, or on the edge of te rectangle</returns>
        public bool Contains(Vector2 point)
        {
            return (point.X > X && point.X < X + Width && point.Y > Y && point.Y < Y + Height);
        }

        /// <summary>
        /// Method for cheking of two RectangleF's overlap
        /// </summary>
        /// <param name="rec">The rectangle to check against</param>
        /// <returns>True if the rectangles overlap</returns>
        public bool Intersects(RectangleF rec)
        {
            return X < rec.X + rec.Width && X + Width > rec.X && Y < rec.Y + rec.Height && Y + Height > rec.Y;
        }

        /// <summary>
        /// Converts a RectangleF to a Microsoft.Xna.Framework.Rectangle
        /// </summary>
        /// <param name="rec">The RectangleF to convert</param>
        public static implicit operator Rectangle(RectangleF rec)
        {
            return new Rectangle(rec.Location.ToPoint(), rec.Size.ToPoint());
        }
    }
}