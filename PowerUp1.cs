using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    internal class PowerUp1 : Upgrade
    {
        public PowerUp1()
        {
           // this.Location = new Vector2(0, 50);
            //this.Size = new Vector2(5, 5);
        }
       // spriteBatch.Draw(Sprite, Location, null, Color.White, 0, origin, 1, effect, 1);

        public override void LoadContent(ContentManager contentManager)
        {
            var sprites = new Texture2D[1];
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = Program.AdventureMan.content.Load<Texture2D>("doubleJump" + (i + 1));
            }

            Sprite = sprites;
        }
    }
}