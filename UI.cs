using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Adventure_man
{
    internal class UI : Scene
    {
        public UI()
        {
            // content shouldent be loaded in constructor ?
            Loadcontent();
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
        }

        public override void Loadcontent()
        {
            // Loads and sets background texture
            backgroundSprite = Program.AdventureMan.content.Load<Texture2D>("bar");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            // Draws background
            spritebatch.Draw(backgroundSprite, new Rectangle(Program.AdventureMan.SceenSize.x / 2, Program.AdventureMan.SceenSize.y / 2, Program.AdventureMan.SceenSize.x / 2, Program.AdventureMan.SceenSize.y / 2), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}