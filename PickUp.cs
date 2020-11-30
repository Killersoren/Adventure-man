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

        public OnPickupDelegate Use;
        private string spritePath;

        public PickUp(string spritePath)
        {
            Size = new Vector2(64, 64);
            Use = (Player p) => { };
            this.spritePath = spritePath;
        }

        public PickUp(string spritePath, Vector2 location) : this(spritePath)
        {
            this.Location = location;
        }

        public override void OnCollision(GameObject collisionTarget)
        {
            var p = collisionTarget as Player;
            if (p != null)
            {
                Use.Invoke(p);
            }

            Program.AdventureMan.CurrentWorld.GameObjectsToRemove.Add(this);
        }

        public override void LoadContent(ContentManager contentManager)
        {
            var sprites = new Texture2D[2];
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = Program.AdventureMan.content.Load<Texture2D>(spritePath + (i + 1));
            }

            Sprite = sprites;
        }
    }
}