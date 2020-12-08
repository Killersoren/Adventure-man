using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    public class ParallaxSprite
    {
        protected Texture2D sprite;
        protected Vector2 offset { get; set; }
        public Vector2 Offset { get { return offset; } set { offset = value; } }

        public Vector2 position;



        //public RectangleF Rectangle { get { return new RectangleF((int)position.X, (int)position.Y, sprite.Width, sprite.Height); } }

        public ParallaxSprite(Texture2D texture)
        {
            sprite = texture;

        }

        public void Draw()
        {
            Program.AdventureMan._spriteBatch.Draw(sprite, position, null, Color.Gray, 0, offset, 1f, SpriteEffects.None, 0f);
        }
    }

}
