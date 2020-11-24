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


        //public override void LoadContent(ContentManager content)
        public override void LoadContent()

        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
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
            throw new NotImplementedException();
        }
    }
}
