using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    internal class Enemy : MoveableGameObject
    {
        private int health;
        private bool isAlive;

        public Enemy()
        {
            speed = 100;
        }

        //public override void LoadContent(ContentManager content)
        public override void LoadContent(ContentManager contentManager)
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Location, Color.White);
        }

        public override void Update()
        {
            // Move(new Vector2(-0.5f, 0));
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