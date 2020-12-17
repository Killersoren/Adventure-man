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
        public static SpriteAnimation TransparentSprite;

        static Globals()
        {
            Texture2D texture = new Texture2D(Program.AdventureMan.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            Color[] c = new Color[] { Color.FromNonPremultiplied(255, 255, 255, 100) };
            texture.SetData(c);
            TransparentSprite = texture;
        }
    }
}