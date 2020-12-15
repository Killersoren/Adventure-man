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
        /// Ras - Draws a black bar at the top of the scene that shows the amount of healtch and what weapons is equipped if any 
        /// Also shows the amount of coins the player have in the middle and player controls to the right
        /// Shows aditional info in debug mode
        /// </summary>
        /// <param name="spritebatch"></param>
        /// 
        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(backgroundSprite, new Rectangle(-100, -160, Program.AdventureMan.SceenSize.x, Program.AdventureMan.SceenSize.y / 2), Color.White);
            spritebatch.DrawString(Program.AdventureMan.font, $"Coins: {World.player.points}", new Vector2(Program.AdventureMan.CurrentWorld.worldSize.X / 2, 0), Color.Yellow);
            spritebatch.DrawString(Program.AdventureMan.font, $"Player Health= {World.player.health}", new Vector2(0, Program.AdventureMan.font.LineSpacing * 2), Color.White);
            if (World.player.CurrentWeapon != null)
                spritebatch.DrawString(Program.AdventureMan.font, $"Player current Weapon={World.player.CurrentWeapon.name}", new Vector2(0, Program.AdventureMan.font.LineSpacing), Color.White);
           
            spritebatch.DrawString(Program.AdventureMan.font, $"Press Up / W to jump", new Vector2(900, Program.AdventureMan.font.LineSpacing), Color.White);
            spritebatch.DrawString(Program.AdventureMan.font, $"Press Down/S to crouch", new Vector2(900, Program.AdventureMan.font.LineSpacing*2), Color.White);
            spritebatch.DrawString(Program.AdventureMan.font, $"Press E to attack", new Vector2(1100, Program.AdventureMan.font.LineSpacing), Color.White);
            spritebatch.DrawString(Program.AdventureMan.font, $"Press Q to Swap Weapon", new Vector2(1100, Program.AdventureMan.font.LineSpacing*2), Color.White);

            //For getting feedback
#if DEBUG
            spritebatch.DrawString(Program.AdventureMan.font, $"Player pos= {World.player.Location.X},{World.player.Location.Y}", Vector2.Zero, Color.White);
            spritebatch.DrawString(Program.AdventureMan.altFont, $"Test Altcodes:███", new Vector2(0, Program.AdventureMan.font.LineSpacing * 3), Color.White);
#endif
        }


     
    }
}