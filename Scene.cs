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
        private Vector2 position;

        public Scene()
        {
        }

        public abstract void Loadcontent();

        public abstract void Draw(GameTime gameTime, SpriteBatch spritebatch);

        public abstract void Update(GameTime gameTime);

    }
}
