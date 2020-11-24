using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    abstract class GameObject
    {

        public Texture2D sprite;
        public Rectangle rectangle;


        public Vector2 position;
        public Vector2 offset;

        protected Vector2 origin;
        protected float scale;
        protected float rotation;
        protected Color color;
        protected int layer;
        protected SpriteEffects effect;


        public Vector2 Origin
        {
            get
            {
                return new Vector2(sprite.Width / 2 * scale, sprite.Height / 2 * scale);
            }
        }
        public Vector2 Offset
        {
            get
            {
                return new Vector2(-(Origin.X * scale), -(Origin.Y * scale));
            }
        }

        public virtual Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    (int)(position.X + Offset.X),
                    (int)(position.Y + Offset.Y),
                    sprite.Width,
                    sprite.Height
                );
            }
        }



        public abstract void LoadContent(ContentManager content);

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, color, rotation, Origin, scale, effect, layer);
        }

        public abstract void Update(GameTime gameTime);

    }
}

