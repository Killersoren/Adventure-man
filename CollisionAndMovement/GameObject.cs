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

        //Only for objects with origin != (0,0)
        public Vector2 origin;
        public Vector2 offset;

        public Color color = Color.White;
        public SpriteEffects effect;


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
            //spriteBatch.Draw(Sprite, HitBox, color);
            spriteBatch.Draw(Sprite, HitBox,null, color,0f,Vector2.Zero,effect,0f);

        }
        public void Destroy(GameObject o)
        {
            Program.AdventureMan.CurrentWorld.GameObjectsToRemove.Add(o);
        }
        public static void Spawn(GameObject o)
        {
            Program.AdventureMan.CurrentWorld.newGameObjects.Add(o);
        }

    }
}