using System;

namespace Adventure_man
{
    public static class Program
    {
        public static GameWorld AdventureMan = new GameWorld();

        [STAThread]
        public static void Main()
        {
            AdventureMan.Run();
        }
    }
}