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
        /// <summary>
        /// Søren - Generates the vision
        /// </summary>
        /// <param name="sprite">The sprite for the vision</param>
        /// <param name="position">The position of the vision</param>
        /// <param name="width">How wide the sprite is (How far the enemy can see</param>
        /// <param name="height">How tall the sprite is</param>
        /// <param name="enemy">The enemy that generated the vision</param>
        public Vision(Texture2D sprite, Vector2 position, int width, int height, Enemy enemy)
        {
            Location = position;
            Size = new Vector2(width, height);
            Sprite = sprite;
            Width = width;
            Height = height;

            this.enemy = enemy;

        }

        public override void Update()
        {
            dir = enemy.dir;
            if (dir == GameWorld.Direction.Right)
            {
                Location = enemy.Location;
            }

            else if (dir == GameWorld.Direction.Left)
            {
                Location = enemy.Location - new Vector2(width - enemy.Size.X, 0);
            }
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
