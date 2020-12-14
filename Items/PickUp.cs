using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    public class PickUp : GameObject
    {
        public delegate void OnPickupDelegate(Player player);

        public OnPickupDelegate Use = (Player p) => { };
        private readonly string spritePath = "";

        public PickUp(string spritePath, Vector2 location, Vector2 size, OnPickupDelegate onPickup)
        {
            Size = size;
            this.spritePath = spritePath;
            this.Location = location;
            Use = onPickup;
        }

        /// <summary>
        /// Pickup in grid formula
        /// </summary>
        /// <param name="spritePath">Path to sprite</param>
        /// <param name="numSprites">The number of sprites in the animation</param>
        /// <param name="gridX">X position in grid</param>
        /// <param name="gridY">Y position in grid</param>
        /// <param name="size">Size</param>
        /// <param name="onPickup">What will happen on pickup</param>
        public PickUp(string spritePath, float gridX, float gridY, Vector2 size, OnPickupDelegate onPickup)
        {
            Size = size;
            this.spritePath = spritePath;
            this.Location = new Vector2(gridX * World.GridResulution, gridY * World.GridResulution);
            Use = onPickup;
        }

        public override void OnCollision(GameObject collisionTarget)
        {
            if (collisionTarget is Player p)
            {
                Use.Invoke(p);
                Program.AdventureMan.CurrentWorld.GameObjectsToRemove.Add(this);
            }
        }

        public override void LoadContent(ContentManager contentManager)
        {
            if (spritePath != "") //temp
            {
                var sprites = new Texture2D[2]; // Havde nogle problemer med at lave Pickups som ikke animerede, prøvede på at implemere det men endte med at bruge 2 identiske PNG'er Lidt spild af plads
                for (int i = 0; i < sprites.Length; i++)
                {
                    sprites[i] = Program.AdventureMan.content.Load<Texture2D>(spritePath + (i + 1));
                }

                Sprite = sprites;
            }
            else
            {
                Sprite = Globals.DefaultSprite;
            }
        }
    }
}