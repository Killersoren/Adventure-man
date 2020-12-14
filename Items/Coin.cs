using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    class Coin : MoveableGameObject //Sofie
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


        /// <summary>
        /// Sofie- Creates a Coin
        /// </summary>
        /// <param name="sprite">The coins sprite</param>
        /// <param name="position">Start position</param>
        /// <param name="velocity">the 2D direction it will go in when it moves</param>
        public Coin(Texture2D sprite, Vector2 position, Vector2 velocity)
        {
            dragCoefficient = 1f;
            groundDrag = 0.9f;
            bounce = 0.5f;
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
            if (Location.X > Program.AdventureMan.CurrentWorld.worldSize.X || Location.X < 0 || Location.Y > Program.AdventureMan.CurrentWorld.worldSize.Y || Location.Y < 0) // despawns COin when out of bounds
                Destroy(this);
            if (firstUpdate) // so that the coin doesnt move before it is drawn for the first time
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
                    velocity.Y *=-bounce;
                if (velocity.X != 0)
                    velocity.X*=groundDrag;

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
