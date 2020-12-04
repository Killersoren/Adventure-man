using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;

namespace Adventure_man
{
    public static class Utils
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        public static IEnumerable<GameObject> CollidesInCurrentWorld(this Rectangle rectangle)
        {
            foreach (GameObject gameObject in Program.AdventureMan.CurrentWorld.GameObjects)
            {
                if (rectangle.Intersects(gameObject.HitBox))
                {
                    yield return gameObject;
                }
            }
        }

        public static void DrawCollisionBox(this GameObject go, SpriteBatch spriteBatch)
        {
            Rectangle topLine = go.HitBox;
            topLine.Height = 1;

            Rectangle bottomLine = go.HitBox;
            bottomLine.Y += bottomLine.Height;
            bottomLine.Height = 1;

            Rectangle rightLine = go.HitBox;
            rightLine.X += rightLine.Width;
            rightLine.Width = 1;

            Rectangle leftLine = go.HitBox;
            leftLine.Width = 1;

            spriteBatch.Draw(Globals.DefaultSprite, topLine, Color.Red);
            spriteBatch.Draw(Globals.DefaultSprite, bottomLine, Color.Red);
            spriteBatch.Draw(Globals.DefaultSprite, rightLine, Color.Red);
            spriteBatch.Draw(Globals.DefaultSprite, leftLine, Color.Red);
            spriteBatch.Draw(Globals.TransparentSprite, go.HitBox, Color.Red);
        }

        public static Vector2 RoundTo(this Vector2 vec, int digits)
        {
            vec.X = MathF.Round(vec.X, digits);
            vec.Y = MathF.Round(vec.Y, digits);
            return vec;
        }
    }
}