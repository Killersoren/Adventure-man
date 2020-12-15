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
        public bool IsBlocking;

        public RectangleF HitBox;

        //Only for objects with origin != (0,0)
        public Vector2 origin;

        public Vector2 offset;

        public Color color;
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
            color = Color.White;
            HitBox = new RectangleF();
            this.LoadContent(Program.AdventureMan.Content);
        }

        public GameObject(RectangleF rec)
        {
            HitBox = rec;
            this.LoadContent(Program.AdventureMan.Content);
        }

        public abstract void LoadContent(ContentManager contentManager);

        public virtual void Update()
        {
            Sprite.Update();
        }

        public virtual void OnCollision(GameObject collisionTarget)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Sprite, HitBox, color);

            var newBox = HitBox;
            newBox.Location = Program.AdventureMan.CurrentWorld.Camera.WorldToScreen(HitBox.Location);
            spriteBatch.Draw(Sprite, newBox, null, color, 0f, Vector2.Zero, effect, 0f);
        }

        /// <summary>
        /// Sofie- Removes a given Gameobject from the current worlds list of objects
        /// </summary>
        /// <param name="o">Gameobject you wish to destroy</param>
        public void Destroy(GameObject o)
        {
            Program.AdventureMan.CurrentWorld.GameObjectsToRemove.Add(o);
        }

        /// <summary>
        /// Sofie- Adds an object to the current worlds list of objects
        /// </summary>
        /// <param name="o">Gameobject you wish to add</param>
        public static void Spawn(GameObject o)
        {
            Program.AdventureMan.CurrentWorld.newGameObjects.Add(o);
        }
    }
}