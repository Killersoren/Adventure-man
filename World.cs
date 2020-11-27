using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Adventure_man
{
    public class World
    {
        public List<GameObject> GameObjects;
        public List<GameObject> newGameObjects;
        public List<GameObject> GameObjectsToRemove;
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
            GameObjects = new List<GameObject>();
            GameObjectsToRemove = new List<GameObject>();
            newGameObjects = new List<GameObject>();

            worldGrid = new Vector2(16, 16);
            gridResulution = 64;
            screenSize = new Vector2(worldGrid.X * GridResulution, worldGrid.Y * GridResulution);

            Player = new Player();
            //PowerUp1 testpu = new PowerUp1();
            //Objects.Add(testpu);
            PickUp pickUp = new PickUp("doublejump", new Vector2(100, 200));
            pickUp.Use = (Player p) => { ++p.JumpAmount; };
            Objects.Add(pickUp);
            Objects.Add(Player);
            Objects.Add(new Platform(0, 7, 13, 1));

            Objects.Add(new Platform(4, 4, 2, 1));
            Objects.Add(new Platform(7, 2, 2, 1));
        }

        public void Update()
        {
            GameObjects.AddRange(newGameObjects);
            newGameObjects.Clear();

            foreach (GameObject o in Objects)
            {
                o.Update();
            }

            foreach (GameObject g in GameObjectsToRemove)
            {
                GameObjects.Remove(g);
            }
            GameObjectsToRemove.Clear();
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