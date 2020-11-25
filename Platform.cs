using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    internal class Platform : GameObject
    {
        private int width;
        private int height;

        public int Width
        {
            get { return width; }
            private set
            {
                if (value > 0)
                    width = value;
                else
                    width = 1;
            }
        }

        public int Height
        {
            get { return height; }
            private set
            {
                if (value > 0)
                    height = value;
                else
                    height = 1;
            }
        }

        /// <summary>
        /// Generates a platform object
        /// </summary>
        /// <param name="pos">the position of the platform in the grid, Top Left</param>
        /// <param name="width">the nuber of Grid spaces its going to fill, so the number we are going to multiply the Sprite width with</param>
        /// <param name="height">Same as above just for height</param>
        public Platform(Vector2 pos, int width, int height)
        {
            Width = width;
            Height = height;
            int res = World.GridResulution;
            Location = new Vector2(pos.X * res, pos.Y * res);
        }

        public Platform(int X, int Y, int width, int height)
        {
            Width = width;
            Height = height;
            int res = World.GridResulution;
            Location = new Vector2(X * res, Y * res);
        }

        // public override void LoadContent(ContentManager content)

        public override void LoadContent(ContentManager contentManager)
        {
            Sprite = contentManager.Load<Texture2D>("PlatformTest");
            //HitBox = new RectangleF(Location.X, Location.Y, Sprite.Width * Width, Sprite.Height * Height);
            Size = new Vector2(Sprite.Width * Width, Sprite.Height * Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    spriteBatch.Draw(Sprite, new Vector2(Location.X + (Sprite.Width * x), Location.Y + (Sprite.Height * y)), Color.White);
                }
            }
        }
    }
}