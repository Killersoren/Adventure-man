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
        
        protected Texture2D backgroundSprite;
        public abstract void Loadcontent();

        public abstract void Draw(SpriteBatch spritebatch);

        public abstract void Update();

    }
}
