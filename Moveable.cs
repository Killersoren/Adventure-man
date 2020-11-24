using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    abstract class Moveable : GameObject
    {
        public Vector2 velocity;
        public float speed;

        protected Texture2D[] sprites;
        protected float fps;
        private float timeElapsed;
        private int currentIndex;


        public void Move(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += ((velocity * speed) * deltaTime);

            if (velocity != Vector2.Zero)
                Animate(gameTime);

        }

        //public override void Draw(SpriteBatch spriteBatch)
        //{
        //    DrawMoveable(spriteBatch);
        //}
        //public abstract void DrawMoveable(SpriteBatch spriteBatch);

        protected void Animate(GameTime gameTime)
        {
            fps = 4;
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            currentIndex = (int)(timeElapsed * fps);


            if (currentIndex >= sprites.Length)
            {
                timeElapsed = 0;
                currentIndex = 0;
            }

            sprite = sprites[currentIndex];

        }



        public override void LoadContent(ContentManager content)
        {
            LoadContentMoveable(content);
        }
        public abstract void LoadContentMoveable(ContentManager content);


        public override void Update(GameTime gameTime)
        {
            UpdateMoveable(gameTime);
        }
        public abstract void UpdateMoveable(GameTime gameTime);

        //protected override void OnCollision(GameObject other)
        //{
        //    OnCollisionMoveable(other);
        //}
        //public abstract void OnCollisionMoveable(GameObject other);



    }
}
