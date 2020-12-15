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
        private Vector2 layerOffset;
        public Vector2 position;
      
        /// <summary>
        ///  Ras - ParralaxSprites constructor, sprite and offset is set from parameters
        /// </summary>
        /// <param name="sprite"></param>
        public ParallaxSprite(Texture2D sprite, Vector2 offset)
        {
            this.sprite = sprite;
            layerOffset = offset;
        }



        /// <summary>
        /// Ras - Draws a parallax sprite at a uniq position with a shared LayerOffset from parallaxlayer
        /// </summary>
        public void Draw()
        {
            Program.AdventureMan._spriteBatch.Draw(sprite, position, null, Color.White, 0, layerOffset, 1f, SpriteEffects.None, 0f);
        }
    }

}
