using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Adventure_man
{
    public abstract class GameObject
    {
        public SpriteAnimation Sprite;

        public RectangleF HitBox;

        public Color color = Color.White;

        public Vector2 Location
        {
            get => HitBox.Location;
            protected set => HitBox.Location = value;
        }

        public Vector2 Size
        {
            get => HitBox.Size;
            protected set => HitBox.Size = value;
        }

        public GameObject()
        {
            HitBox = new RectangleF();
        }

        public GameObject(RectangleF rec)
        {
            HitBox = rec;
        }

        public abstract void LoadContent(ContentManager contentManager);

        public virtual void Update()
        {
        }

        public virtual void OnCollision(GameObject collisionTarget)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, HitBox, color);
        }
        public void Destroy(GameObject o)
        {
            Program.AdventureMan.CurrentWorld.GameObjectsToRemove.Add(o);
        }

    }
}