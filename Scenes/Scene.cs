using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    abstract public class Scene
    {
        /// <summary>
        /// Ras - A scenes backgroundsprite
        /// </summary>
        protected Texture2D backgroundSprite;
      
        public abstract void Loadcontent();

        public abstract void Update();

        public abstract void Draw(SpriteBatch spritebatch);


    }
}
