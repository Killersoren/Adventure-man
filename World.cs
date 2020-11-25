using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Adventure_man
{
    public class World
    {
        private List<GameObject> objects;
        private List<Moveable> moveables;
        private static Player p;

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

        internal List<GameObject> Objects { get => objects; private set => objects = value; }
        internal List<Moveable> Moveables { get => moveables; private set => moveables = value; }
        internal static Player P { get => p; private set => p = value; }
        

        public World()
        {
            Objects = new List<GameObject>();
            Moveables = new List<Moveable>();

            worldGrid = new Vector2(16, 16);
            gridResulution = 64;
            screenSize = new Vector2(worldGrid.X * GridResulution,worldGrid.Y * GridResulution);

            P = new Player();


            Objects.Add(p);
            Objects.Add(new Platform(new Vector2(0,7), 13, 1));

            Objects.Add(new Platform(new Vector2(4, 5), 1, 2));
            Objects.Add(new Enemy(new Vector2(2, 5)));


            TransferMoveables();
        }

        private void TransferMoveables()
        {



            foreach (GameObject o in Objects)
            {
                if (o is Moveable)
                    Moveables.Add((Moveable)o);
            }

        }
    }
}
