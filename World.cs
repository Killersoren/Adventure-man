﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Adventure_man
{
    public class World
    {
        public List<GameObject> GameObjects;
        public static Player Player;

        /// <summary>
        /// The number of grids the world/level has
        /// </summary>
        private Vector2 worldGrid;

        /// <summary>
        /// How many pixels(^2) in each grid zone
        /// </summary>
        private static int gridResulution;

        public static int GridResulution { get => gridResulution; }

        public Vector2 screenSize;
        //public static Vector2 ScreenSize { get => screenSize; }

        internal List<GameObject> Objects { get => GameObjects; private set => GameObjects = value; }

        public World()
        {
            Objects = new List<GameObject>();

            worldGrid = new Vector2(16, 16);
            gridResulution = 64;
            screenSize = new Vector2(worldGrid.X * GridResulution, worldGrid.Y * GridResulution);

            Player = new Player();

            Objects.Add(Player);
            Objects.Add(new Platform(0, 7, 13, 1));
            Objects.Add(new Platform(4, 5, 1, 2));
        }

        public void Update()
        {
            foreach (GameObject o in Objects)
            {
                o.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject o in Objects)
            {
                o.Draw(spriteBatch);
#if DEBUG
                o.DrawCollisionBox(spriteBatch);
#endif
            }
        }
    }
}