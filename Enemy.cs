using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
     class Enemy : Moveable
    {
        private int health;
        private bool isAlive;


        public Enemy()
        {

            speed = 100;
            effect = SpriteEffects.None;
            color = Color.Red;
            scale = 5f;
        }
        public Enemy(Vector2 pos)
        {

            speed = 100;
            effect = SpriteEffects.None;
            color = Color.Red;
            scale = 1;


            position = new Vector2(32 + (pos.X * World.GridResulution), 64 + (pos.Y * World.GridResulution));

        }

        //public override void LoadContent(ContentManager content)
        public override void LoadContent()

        {
            sprites = new Texture2D[2];
            for (int i = 0; i < sprites.Length; i++)
            {
                //sprites[i] = content.Load<Texture2D>("MoveTest" + (i + 1)+"_v2");
                sprites[i] = GameWorld.content.Load<Texture2D>("MoveTest" + (i + 1) + "_v2");

            }

            sprite = sprites[0];


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, color);
        }

        public override void Update(GameTime gameTime)
        {
            Gravity();
            Move(gameTime);
        }

        public void Attack()
        {

        }

        public void TakeDamage()
        {

        }

        public override void OnCollision(GameObject other)
        {
            if (other is Platform)
            {

                if (other.CollisionBox.Center.Y > CollisionBox.Center.Y) // If Player is on top
                {
                    //color = Color.Red;
                    velocity.Y = 0;
                    //velocity.Normalize();
                }
                if (other.CollisionBox.Bottom <= CollisionBox.Bottom && other.CollisionBox.Center.X < CollisionBox.Center.X) // if the player is to the Right of the platform
                {
                    velocity += new Vector2(1, 0);
                }
                if (other.CollisionBox.Bottom <= CollisionBox.Bottom && other.CollisionBox.Center.X > CollisionBox.Center.X)// if the player is to the Left of the platform
                {
                    velocity += new Vector2(-1, 0);
                }
            }
        }
    }
}
