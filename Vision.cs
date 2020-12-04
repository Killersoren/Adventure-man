using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    class Vision : MoveableGameObject
    {
        private int width;
        private int height;

        public int Width
        {
            get { return width; }
            private set
            {
                if (value > 0)
                    width = value;
                else
                    width = 1;
            }
        }

        public int Height
        {
            get { return height; }
            private set
            {
                if (value > 0)
                    height = value;
                else
                    height = 1;
            }
        }


        public Vision(Texture2D sprite, Vector2 position, int width, int height)
        {
            Location = position;
            Size = new Vector2(width - 1, height - 1);
            Sprite = sprite;
            Width = width;
            Height = height;

            
       
        }

        public override void Update()
        {
            MoveTo(Location);

            base.Update();
        }


        public override void LoadContent(ContentManager contentManager)
        {
            
        }

        public override void OnCollision(GameObject collisionTarget)
        {
            if (collisionTarget is Player)
            {
                Enemy.playerInSight = true;
            }

            base.OnCollision(collisionTarget);
        }

    }
}
