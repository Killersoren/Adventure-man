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
            //Program.AdventureMan.SceenSize.x / 100 * 1, Program.AdventureMan.SceenSize.y / 100 * 40, 150, 100
            spritebatch.Draw(backgroundSprite, new Rectangle(-100, -160, Program.AdventureMan.SceenSize.x, Program.AdventureMan.SceenSize.y / 2), Color.White);
            spritebatch.DrawString(Program.AdventureMan.font, $"Coins: {World.Player.points}", new Vector2(Program.AdventureMan.CurrentWorld.worldSize.X / 2, 0), Color.Yellow);
            //(int)Program.AdventureMan.CurrentWorld.worldSize

            //For getting feedback
#if DEBUG
            spritebatch.DrawString(Program.AdventureMan.font, $"Player pos= {World.Player.Location.X},{World.Player.Location.Y}", Vector2.Zero, Color.White);
            if (World.Player.CurrentWeapon!=null)
                spritebatch.DrawString(Program.AdventureMan.font, $"Player current Weapon={World.Player.CurrentWeapon.name}", new Vector2(0, Program.AdventureMan.font.LineSpacing), Color.White);
            spritebatch.DrawString(Program.AdventureMan.font, $"Player Health= {World.Player.health}", new Vector2(0, Program.AdventureMan.font.LineSpacing * 2), Color.White);
            spritebatch.DrawString(Program.AdventureMan.altFont, $"Test Altcodes:███", new Vector2(0, Program.AdventureMan.font.LineSpacing * 3), Color.White);
#endif
            //spritebatch.Draw(backgroundSprite, new Rectangle(0, 0, Program.AdventureMan.SceenSize.x, Program.AdventureMan.SceenSize.y), Color.White);

            //  spritebatch.Draw(Program.AdventureMan.CurrentWorld.pickUp.Sprite, new Rectangle(1100, 0, 64, 64), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}