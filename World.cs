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
        /// <summary>
        /// GameObjects lists
        /// </summary>
        public List<GameObject> GameObjects;
        public List<GameObject> newGameObjects;
        public List<GameObject> GameObjectsToRemove;


        public static Player Player = new Player();
        public delegate bool CompleationParamitor();
        public CompleationParamitor forCompleation = () => { return false; };
        public Camera Camera;

        /// <summary>
        /// Parallax list and static textures
        /// </summary>
        public List<ParallaxLayer> parallaxList;
        public static Texture2D tree = Program.AdventureMan.content.Load<Texture2D>("tree");
        public static Texture2D cloud = Program.AdventureMan.content.Load<Texture2D>("clouds");
        public static Texture2D ground = Program.AdventureMan.content.Load<Texture2D>("ground");
        public static Texture2D sun = Program.AdventureMan.content.Load<Texture2D>("sun");
        

        /// <summary>
        /// The number of grids the world/level has
        /// </summary>
        private Vector2 worldGrid;

        /// <summary>
        /// How many pixels(^2) in each grid zone
        /// </summary>
        private static readonly int gridResulution = 64;

        public static int GridResulution { get => gridResulution; }

        public Vector2 worldSize;

        internal List<GameObject> Objects { get => GameObjects; private set => GameObjects = value; }

        /// <summary>
        /// All - Worlds Constructor, Sets parameters, adds player to GameObjects and sets player spawn to player location
        /// </summary>
        /// <param name="parallaxes"></param>
        /// <param name="playerLocation"></param>
        /// <param name="worldGrid"></param>
        /// <param name="gameObjects"></param>
        /// <param name="forCompleation"></param>
        public World(List<ParallaxLayer> parallaxes, Vector2 playerLocation, Vector2 worldGrid, List<GameObject> gameObjects, CompleationParamitor forCompleation)
        {
            Camera = new Camera();
            parallaxList = parallaxes;
            GameObjects = gameObjects;
            GameObjectsToRemove = new List<GameObject>();
            newGameObjects = new List<GameObject>();
            Player.SetSpawn(playerLocation);
            GameObjects.Add(Player);


            this.forCompleation = forCompleation;
            this.worldGrid = worldGrid;
            worldSize = new Vector2(worldGrid.X * GridResulution, worldGrid.Y * GridResulution);
            GameObjects.AddRange(Border());
        }

        /// <summary>
        /// All - Sets Camera position to players location, adds new gameobjects to Gameobjects and updates each parrallax and object from lists
        /// Removes objects in GameObjects from each Object added to list GamObjectsToRemove
        /// </summary>
        public void Update()
        {
            Camera.Position = Player.Location;
            GameObjects.AddRange(newGameObjects);
            newGameObjects.Clear();

            foreach (ParallaxLayer p in parallaxList)
            {
                p.Update();
            }

            foreach (GameObject o in Objects )
            {
                o.Update();
            }

            foreach (GameObject g in GameObjectsToRemove)
            {
                GameObjects.Remove(g);
            }
            GameObjectsToRemove.Clear();
        }

        /// <summary>
        /// All - Draws each parallax and objects from lists, 
        /// Debug: also draws CollisionBox
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ParallaxLayer p in parallaxList)
            {
                p.Draw();
            }
            foreach (GameObject o in Objects)
            {
                o.Draw(spriteBatch);
#if DEBUG
                o.DrawCollisionBox(spriteBatch);
#endif
            }
        }
        /// <summary>
        /// Creates a boarder around the playing area.
        /// </summary>
        /// <returns>The Boarder platforms</returns>
        private IEnumerable<GameObject> Border()
        {
            yield return new Platform(0, -2, (int)worldGrid.X, 1, true); //Top (y=-2 :Leaves a i grid gab at the op of the two side boarders, but allows for jumping at the top)
            yield return new Platform(0, worldGrid.Y - 1, (int)worldGrid.X, 1, true); //Bottom

            yield return new Platform(-1, -2, 1, (int)worldGrid.Y+2, true); //Left
            yield return new Platform(worldGrid.X, -2, 1, (int)worldGrid.Y+2, true); //Right
            //yield return new Platform(-1*gridResulution, -2*gridResulution, gridResulution, (worldGrid.Y+2)*gridResulution); //Left
            //yield return new Platform(worldGrid.X*gridResulution, -2*gridResulution, gridResulution, (worldGrid.Y+2)*gridResulution); //Right


        }
    }
}