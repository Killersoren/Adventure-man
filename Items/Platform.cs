using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    internal class Platform : IntermidiateTemporaryClassForStoppingMovement
    {
        private int width;
        private int height;
        private bool grid;

        private delegate void PDraw(SpriteBatch spriteBatch);

        private PDraw PlatformDraw = (SpriteBatch spriteBatch) => { };

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
        /// Sofie- Generates a platform object in grid
        /// </summary>
        /// <param name="pos">the position of the platform in the grid, Top Left</param>
        /// <param name="width">the nuber of Grid spaces its going to fill, so the number we are going to multiply the Sprite width with</param>
        /// <param name="height">Same as above just for height</param>
        /// <param name="grid">To set it apart from non grid platform</param>
        public Platform(Vector2 pos, int width, int height, bool grid)
        {
            Width = width;
            Height = height;
            int res = World.GridResulution;
            Location = new Vector2(pos.X * res, pos.Y * res);
            this.HitBox = (new RectangleF(Location.X, Location.Y * res, width * res, height * res));
            this.grid = true;
            CheckGrid();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="X">X position in a grid, det at den er float gør at man kan placere den lidt uden for grid</param>
        /// <param name="Y">Y position in a grid, det at den er float gør at man kan placere den lidt uden for grid</param>
        /// <param name="width">number of grid spaces it fils horizontally</param>
        /// <param name="height">number of grid spaces it fils vertically</param>
        /// <param name="grid">to set it apart from non grid (true or false it doesnt matter)</param>
        public Platform(float X, float Y, int width, int height, bool grid)
        {
            Width = width;
            Height = height;
            int res = World.GridResulution;
            Location = new Vector2(X * res, Y * res);
            this.grid = true;
            this.HitBox = (new RectangleF(X * res, Y * res, width * res, height * res));
            CheckGrid();
        }

        public Platform(float x, float y, float width, float height)
        {
            this.HitBox = new RectangleF(x, y, width, height);
            grid = false;
            CheckGrid();
        }

        public override void LoadContent(ContentManager contentManager)
        {
            Sprite = contentManager.Load<Texture2D>("PlatformTest");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            PlatformDraw(spriteBatch);
        }

        /// <summary>
        /// Sofie- This is so that you dont have to check every draw, This changes the Draw / Load Content Methods depending on if the platform is one of the old Grid platforms or the new non grid platform
        /// </summary>
        private void CheckGrid()
        {
            if (grid) //Sofie
            {
                PlatformDraw = (SpriteBatch spriteBatch) =>
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            spriteBatch.Draw(Sprite, Program.AdventureMan.CurrentWorld.Camera.WorldToScreen(new Vector2(Location.X + (Sprite.Width * x), Location.Y + (Sprite.Height * y))), Color.White);
                        }
                    }
                };
            }
            else
            {
                PlatformDraw = (SpriteBatch spriteBatch) =>
                {
                    base.Draw(spriteBatch);
                };
            }
        }

        //public override void OnCollision(GameObject collisionTarget)
        //{
        //    base.OnCollision(collisionTarget);
        //}
    }
}