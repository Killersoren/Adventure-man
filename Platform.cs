using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    class Platform : GameObject
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

        public override Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    (int)(position.X),
                    (int)(position.Y),
                    sprite.Width*Width,
                    sprite.Height*Height
                );
            }
        }

        /// <summary>
        /// Generates a platform object
        /// </summary>
        /// <param name="pos">the position of the platform in the grid, Top Left</param>
        /// <param name="width">the nuber of Grid spaces its going to fill, so the number we are going to multiply the Sprite width with</param>
        /// <param name="height">Same as above just for height</param>
        public Platform(Vector2 pos,int width, int height)
        {
            Width = width;
            Height = height;
            int res = World.GridResulution;
            position = new Vector2(pos.X*res, pos.Y * res);



            color = Color.White;
            scale = 1;
        }

       // public override void LoadContent(ContentManager content)

        public override void LoadContent()
        {
            //sprite = content.Load<Texture2D>("PlatformTest");

            sprite = GameWorld.content.Load<Texture2D>("PlatformTest");

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (width == 1 && height == 1)
                spriteBatch.Draw(sprite, position, color);
            else
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        spriteBatch.Draw(sprite, new Vector2(position.X + (sprite.Width * x), position.Y + (sprite.Height * y)), color);
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
