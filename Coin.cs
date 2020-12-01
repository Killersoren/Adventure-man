using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    class Coin : MoveableGameObject
    {
        public int coinValue;
        private float gravStrength = 0;
        private bool firstUpdate = true;
        protected bool isGrounded //bad maybe?, we check too often i think, maybe not only when we try to apply gravity (once per cycle) and ocasionally when we jummp
        {
            get
            {
                var isGrounded = false;

                var downRec = HitBox.Copy();
                downRec.Location -= new Vector2(0, -1);

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



        public Coin(Texture2D sprite, Vector2 position, Vector2 velocity)
        {
            
            Sprite = sprite;
            Location = position;
            base.velocity = velocity;
            velocity.Normalize();
            speed = 1;
            coinValue = 1;
            Size = new Vector2(Sprite.Width - 1, Sprite.Height - 1);
        }

        public override void Update()
        {
            if (Location.X > Program.AdventureMan.CurrentWorld.screenSize.X || Location.X < 0 || Location.Y > Program.AdventureMan.CurrentWorld.screenSize.Y || Location.Y < 0)
                Destroy(this);
            if (firstUpdate)
                firstUpdate = false;
            else
            {
                ApplyGravity();
                base.Update();
            }
            
        }
        private void ApplyGravity()
        {
            if (isGrounded)
            {
                if (velocity.Y > 0)
                    velocity.Y = 0;
                if (velocity.X != 0)
                    velocity.X = 0;

                gravStrength = 0;
                return;
            }
            gravStrength += 0.1f;
            velocity += new Vector2(0, gravStrength);
        }
        public override void LoadContent(ContentManager contentManager)
        {
            
        }


    }
}
