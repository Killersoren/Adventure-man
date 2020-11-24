using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    class Player : Moveable
    {


        /// <summary>
        /// Initialisere en Player Object
        /// </summary>
        public Player()
        {

            speed = 100;
            effect = SpriteEffects.None;
            color = Color.White;
            scale = 1;
            position = new Vector2(32,64);

            //forceGravity = new Vector2(0, 9.81f);
            //forceGravity = new Vector2(0, 1f);


        }
        /// <summary>
        /// Handles Player input
        /// </summary>
        private void HandleInput(GameTime gameTime)
        {
            velocity = Vector2.Zero;

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.D) || keyState.IsKeyDown(Keys.Right))
            {
                velocity += new Vector2(1, 0);
                effect = SpriteEffects.None;
                Animate(gameTime);
            }
            if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Left))
            {
                velocity += new Vector2(-1, 0);
                effect = SpriteEffects.FlipHorizontally;
                Animate(gameTime);
            }




            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }




        }


        //public override void OnCollisionMoveable(GameObject other)
        //{
        //    throw new NotImplementedException();
        //}

        public override void UpdateMoveable(GameTime gameTime)
        {
            HandleInput(gameTime);
            //Animate(gameTime);
            Gravity();
            Move(gameTime);
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[2];
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>("MoveTest" + (i + 1)+"_v2");
            }

            sprite = sprites[0];
        }

        public override void LoadContentMoveable(ContentManager content)
        {
            throw new NotImplementedException();
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Platform)
            {
                
                if (other.CollisionBox.Center.Y>CollisionBox.Center.Y) // If Player is on top
                {
                    //color = Color.Red;
                    velocity.Y = 0;
                    //velocity.Normalize();
                }
                if(other.CollisionBox.Bottom<=CollisionBox.Bottom && other.CollisionBox.Center.X<CollisionBox.Center.X) // if the player is to the Right of the platform
                {
                    velocity += new Vector2(1, 0);
                }
                if (other.CollisionBox.Bottom <= CollisionBox.Bottom && other.CollisionBox.Center.X > CollisionBox.Center.X)// if the player is to the Left of the platform
                {
                    velocity += new Vector2(-1, 0);
                }
            }
        }
        //public override void Draw(SpriteBatch spriteBatch)
        //{
        //    spriteBatch.Draw(sprite, position, Color.White);
        //}


    }
}
