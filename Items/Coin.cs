using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    internal class Coin : Character //Sofie
    {
        public int coinValue;
        private bool firstUpdate = true;

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
                ApplyGravity(0.1f);
                base.Update();
            }
        }

        public override void LoadContent(ContentManager contentManager)
        {
        }
    }
}