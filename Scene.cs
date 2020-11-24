using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    abstract class Scene
    {
        private Vector2 position;
        protected ContentManager Content = new ContentManager(GameWorld.Services);

        public Scene()
        {
            Content.RootDirectory = "Content";
        }


        public abstract void Draw(GameTime gameTime, SpriteBatch spritebatch);

        public abstract void Update(GameTime gameTime);

    }
}
