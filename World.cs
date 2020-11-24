using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    public class World
    {
        private List<GameObject> objects;
        private List<Moveable> moveables;
        private static Player p;


        internal List<GameObject> Objects { get => objects; private set => objects = value; }
        internal List<Moveable> Moveables { get => moveables; private set => moveables = value; }
        internal static Player P { get => p; private set => p = value; }

        public World()
        {
            Objects = new List<GameObject>();
            Moveables = new List<Moveable>();



            P = new Player();


            Objects.Add(p);
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
