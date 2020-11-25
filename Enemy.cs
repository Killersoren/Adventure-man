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

        private float gravStrength = 0; // don't like the placement of this var :/

        protected bool isGrounded //bad maybe?, we check too often i think, maybe not only when we try to apply gravity (once per cycle) and ocasionally when we jummp
        {
            get
            {
                var isGrounded = false;

                var downRec = HitBox.Copy();
                downRec.Location -= new Vector2(0, -5);

                foreach (GameObject gameObject in Program.AdventureMan.CurrentWorld.GameObjects)
                {
                    if (downRec.Intersects(gameObject.HitBox) && !isGrounded)
                    {
                        if (gameObject is Platform)
                        {
                            isGrounded = true;
                        }
                    }
                }
                return isGrounded;
            }
        }

        public Enemy()
        {
            speed = 100;
        }

        public Enemy(Vector2 pos)
        {
            speed = 100;

            Location = new Vector2(32 + (pos.X * World.GridResulution), 64 + (pos.Y * World.GridResulution));
        }

        //public override void LoadContent(ContentManager content)
        public override void LoadContent(ContentManager contentManager)
        {
            var sprites = new Texture2D[2];
            for (int i = 0; i < sprites.Length; i++)
            {
                //sprites[i] = content.Load<Texture2D>("MoveTest" + (i + 1)+"_v2");
                sprites[i] = Program.AdventureMan.content.Load<Texture2D>("MoveTest" + (i + 1) + "_v2");
            }

            Sprite = sprites;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Location, Color.White);
        }

        public override void Update()
        {
            ApplyGravity();
        }

        private void ApplyGravity()
        {
            if (isGrounded)
            {
                if (Velocity.Y > 0)
                    Velocity.Y = 0;

                gravStrength = 0;
                return;
            }
            gravStrength += 0.1f;
            Velocity += new Vector2(0, gravStrength);
        }

        public void Attack()
        {
        }

        public void TakeDamage()
        {
        }

        public override void OnCollision(GameObject other)
        {
            //    if (other is Platform)
            //    {
            //        if (other.HitBox.Center.Y > HitBox.Center.Y) // If Player is on top
            //        {
            //            //color = Color.Red;
            //            Velocity.Y = 0;
            //            //velocity.Normalize();
            //        }
            //        if (other.HitBox.Bottom <= CollisionBox.Bottom && other.CollisionBox.Center.X < CollisionBox.Center.X) // if the player is to the Right of the platform
            //        {
            //            velocity += new Vector2(1, 0);
            //        }
            //        if (other.CollisionBox.Bottom <= CollisionBox.Bottom && other.CollisionBox.Center.X > CollisionBox.Center.X)// if the player is to the Left of the platform
            //        {
            //            velocity += new Vector2(-1, 0);
            //        }
            //    }
            //}
        }
    }
}