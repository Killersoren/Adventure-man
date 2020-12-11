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
        private Vector2 LayerOffset;
        public Vector2 position;
      
        /// <summary>
        ///  Ras - ParralaxSprites constructor, sprite and offset is set from parameters
        /// </summary>
        /// <param name="Sprite"></param>
        public ParallaxSprite(Texture2D Sprite, Vector2 offset)
        {
            sprite = Sprite;
            LayerOffset = offset;
        }



        /// <summary>
        /// Ras - Draws a parallax sprite at a uniq position with a shared LayerOffset from parallaxlayer
        /// </summary>
        public void Draw()
        {
            Program.AdventureMan._spriteBatch.Draw(sprite, position, null, Color.White, 0, LayerOffset, 1f, SpriteEffects.None, 0f);
        }
    }

}
