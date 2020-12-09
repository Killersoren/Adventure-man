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

        /// <summary>
        ///  Ras - ParralaxSprites constructor, sprite is set from parameter Sprite
        /// </summary>
        /// <param name="Sprite"></param>
        public ParallaxSprite(Texture2D Sprite)
        {
            sprite = Sprite;

        }

        /// <summary>
        /// Ras - Draws sprite at offset 
        /// </summary>
        public void Draw()
        {
            Program.AdventureMan._spriteBatch.Draw(sprite, position, null, Color.Gray, 0, offset, 1f, SpriteEffects.None, 0f);
        }
    }

}
