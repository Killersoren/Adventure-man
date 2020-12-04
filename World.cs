using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public PickUp pickUp;
        Texture2D sprite1;
        /// <summary>
        /// The number of grids the world/level has
        /// </summary>
        private Vector2 worldGrid;

        /// <summary>
        /// How many pixels(^2) in each grid zone
        /// </summary>
        private static int gridResulution;

        public static int GridResulution { get => gridResulution; }

        public Vector2 worldSize;
        //public static Vector2 ScreenSize { get => screenSize; }

        internal List<GameObject> Objects { get => GameObjects; private set => GameObjects = value; }

        public World()
        {

            GameObjects = new List<GameObject>();
            GameObjectsToRemove = new List<GameObject>();
            newGameObjects = new List<GameObject>();

            worldGrid = new Vector2(20, 11);
            gridResulution = 64;
            //worldSize = new Vector2(Program.AdventureMan._graphics.PreferredBackBufferWidth, Program.AdventureMan._graphics.PreferredBackBufferHeight);
            worldSize = new Vector2(worldGrid.X * GridResulution, worldGrid.Y * GridResulution);

            Player = new Player(0,5);
            pickUp = new PickUp("doublejump", new Vector2(100, 200));
            pickUp.Use = (Player p) => { ++p.JumpAmount; };
            Objects.Add(pickUp);
            Objects.Add(Player);


            Objects.Add(new Enemy(9, 4));
            Objects.Add(new Platform(0, 7, 13, 1));
            Objects.Add(new Platform(13, 9, 1, 1));

            Objects.Add(new Platform(4, 4, 2, 1));
            Objects.Add(new Platform(7, 2, 2, 1));
            Boarder();
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
            sprite1 = Program.AdventureMan.content.Load<Texture2D>("tree");

            spriteBatch.Draw(sprite1,new Rectangle(2,200,128,256),Color.White);
            spriteBatch.Draw(sprite1, new Rectangle(2, 200, 128, 256), Color.White);
            spriteBatch.Draw(sprite1, new Rectangle(50, 200, 128, 256), Color.White);
            spriteBatch.Draw(sprite1, new Rectangle(250, 200, 128, 256), Color.White);
            spriteBatch.Draw(sprite1, new Rectangle(340, 200, 128, 256), Color.White);

            spriteBatch.Draw(sprite1, new Rectangle(500, 200, 128, 256), Color.White);



            foreach (GameObject o in Objects)
            {
                o.Draw(spriteBatch);
#if DEBUG
                o.DrawCollisionBox(spriteBatch);
#endif
            }
        }
        private void Boarder()
        {
            Objects.Add(new Platform(0, -2, (int)worldGrid.X, 1)); //Top (y=-2 :Leaves a i grid gab at the op of the two side boarders, but allows for jumping at the top)
            Objects.Add(new Platform(0, (int)worldGrid.Y, (int)worldGrid.X, 1)); //Bottom

            Objects.Add(new Platform(-1,0,1,(int)worldGrid.Y)); //Left
            Objects.Add(new Platform((int)worldGrid.X, 0, 1, (int)worldGrid.Y)); //Right
        }


    }
}