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
        /// <summary>
        /// Ras - UI constructor, calls Loadcontent
        /// </summary>
        public UI()
        {
            Loadcontent();
        }


        /// <summary>
        /// Ras - Loads and sets backgroundSprite
        /// </summary>
        /// 
        public override void Loadcontent()
        {
            backgroundSprite = Program.AdventureMan.content.Load<Texture2D>("bar");
        }


        /// <summary>
        /// Empty for now
        /// </summary>
        public override void Update()
        {
        }


        /// <summary>
        /// Ras - Draws background and shows the amount of healtch and coins the player have. 
        /// Also shows what weapons is equipped if 
        /// </summary>
        /// <param name="spritebatch"></param>
        /// 
        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(backgroundSprite, new Rectangle(-100, -160, Program.AdventureMan.SceenSize.x, Program.AdventureMan.SceenSize.y / 2), Color.White);
            spritebatch.DrawString(Program.AdventureMan.font, $"Coins: {World.Player.points}", new Vector2(Program.AdventureMan.CurrentWorld.worldSize.X / 2, 0), Color.Yellow);
            spritebatch.DrawString(Program.AdventureMan.font, $"Player Health= {World.Player.health}", new Vector2(0, Program.AdventureMan.font.LineSpacing * 2), Color.White);
            if (World.Player.CurrentWeapon != null)
                spritebatch.DrawString(Program.AdventureMan.font, $"Player current Weapon={World.Player.CurrentWeapon.name}", new Vector2(0, Program.AdventureMan.font.LineSpacing), Color.White);

            //For getting feedback
#if DEBUG
            spritebatch.DrawString(Program.AdventureMan.font, $"Player pos= {World.Player.Location.X},{World.Player.Location.Y}", Vector2.Zero, Color.White);
            spritebatch.DrawString(Program.AdventureMan.altFont, $"Test Altcodes:███", new Vector2(0, Program.AdventureMan.font.LineSpacing * 3), Color.White);
#endif
        }


     
    }
}