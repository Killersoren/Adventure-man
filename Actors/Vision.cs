using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Adventure_man
{
    class Vision : MoveableGameObject
    {
        private int width;
        private int height;

        private Enemy enemy;

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

        public Vision(Texture2D sprite, Vector2 position, int width, int height, Enemy enemy1)
        {
            Location = position;
            Size = new Vector2(width, height);
            Sprite = sprite;
            Width = width;
            Height = height;

            enemy = enemy1;

        }

        public override void Update()
        {
            MoveTo(enemy.Location);

            dir = enemy.dir;

            if (dir == GameWorld.Direction.Right)
            {
            }

            else if (dir == GameWorld.Direction.Left)
            {
                Location += new Vector2(-183, 0);
            }

            //    dir = UpdateSprite();

            base.Update();
        }


        public override void LoadContent(ContentManager contentManager)
        {
            
        }

        public override void OnCollision(GameObject collisionTarget)
        {
            if (collisionTarget is Player)
            {
                enemy.playerInSight = true;
            }

            base.OnCollision(collisionTarget);
        }

    }
}
