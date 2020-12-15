using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    /// <summary>
    /// Magnus - A class representing the pickups in the game, these are gameobjects that the player can interact wiht by walking into
    /// </summary>
    public class PickUp : GameObject
    {
        /// <summary>
        /// The delegate signature for defining pickup behaviour
        /// </summary>
        /// <param name="player">The player that picked up the pickup</param>
        public delegate void OnPickupDelegate(Player player);

        /// <summary>
        /// Delegate describing what happens when the player picks up the pickup
        /// </summary>
        public OnPickupDelegate Use = (Player p) => { };

        private readonly string spritePath = "";
        private readonly int spriteAmount;

        /// <summary>
        /// Creates a new PickUp
        /// </summary>
        /// <param name="spritePath">The path to the texure(s) (ommiting the number)</param>
        /// <param name="spriteAmount">The amount of textures in the animation</param>
        /// <param name="location">The location of the pickup in the world</param>
        /// <param name="size">The size of the pickup</param>
        /// <param name="onPickup">The function to invoke in case a player picks this up</param>
        public PickUp(string spritePath, int spriteAmount, Vector2 location, Vector2 size, OnPickupDelegate onPickup)
        {
            Size = size;
            this.spritePath = spritePath;
            this.spriteAmount = spriteAmount;
            this.Location = location;
            Use = onPickup;
        }

        /// <summary>
        /// (not Magnus) - Pickup in grid formula
        /// </summary>
        /// <param name="spritePath">Path to sprite</param>
        /// <param name="numSprites">The number of sprites in the animation</param>
        /// <param name="gridX">X position in grid</param>
        /// <param name="gridY">Y position in grid</param>
        /// <param name="size">Size</param>
        /// <param name="onPickup">What will happen on pickup</param>
        public PickUp(string spritePath, int spriteAmount, float gridX, float gridY, Vector2 size, OnPickupDelegate onPickup)
        {
            Size = size;
            this.spritePath = spritePath;
            this.spriteAmount = spriteAmount;
            this.Location = new Vector2(gridX * World.GridResulution, gridY * World.GridResulution);
            Use = onPickup;
        }

        /// <summary>
        /// If the collision is with a Player, this invokes the OnPickupDelegate and removes the pickup from the current world.
        /// </summary>
        /// <param name="collisionTarget"></param>
        public override void OnCollision(GameObject collisionTarget)
        {
            if (collisionTarget is Player p)
            {
                Use.Invoke(p);
                Program.AdventureMan.CurrentWorld.GameObjectsToRemove.Add(this);
            }
        }

        /// <summary>
        /// Loader textures baseret på spritePath
        /// </summary>
        public override void LoadContent(ContentManager contentManager)
        {
            if (spritePath != "")
            {
                var sprites = new Texture2D[spriteAmount];
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