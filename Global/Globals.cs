using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Adventure_man
{
    public static class Globals
    {
        public static SpriteAnimation DefaultSprite = Program.AdventureMan.Content.Load<Texture2D>("PlatformTest");
        //public static SpriteFont DefaultFont = Program.AdventureMan.Content.Load<SpriteFont>(""); //Add this
    }
}