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
        protected Vector2 SpriteOffset { get; set; }
        public Vector2 Offset { get { return SpriteOffset; } set { SpriteOffset = value; } }

        public Vector2 position;

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
            Program.AdventureMan._spriteBatch.Draw(sprite, position, null, Color.Gray, 0, SpriteOffset, 1f, SpriteEffects.None, 0f);
        }
    }

}
