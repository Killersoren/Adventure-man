using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    internal class Menu : Scene
    {
        private readonly Button startResumeGame;
        private readonly Button exit;
        private readonly Button fullScreen;
        private readonly List<Button> buttons;

        /// <summary>
        /// Ras - Menu Constructor, Sets and create the 3 buttons and adds them to a new list of buttons
        /// </summary>
        public Menu()
        {
            startResumeGame = new Button(Program.AdventureMan.SceenSize.x / 100 * 1, Program.AdventureMan.SceenSize.y / 100 * 40, "Start");
            fullScreen = new Button(Program.AdventureMan.SceenSize.x / 100 * 1, Program.AdventureMan.SceenSize.y / 100 * 50, "Fullscreen");
            exit = new Button(Program.AdventureMan.SceenSize.x / 100 * 1, Program.AdventureMan.SceenSize.y / 100 * 60, "Exit");
            startResumeGame.Click += StartResumeGame_Click;
            exit.Click += Exit_Click;
            fullScreen.Click += FullScreen_Click;

            buttons = new List<Button>
            {
                startResumeGame,
                exit,
                fullScreen
            };
            Loadcontent();
        }

        /// <summary>
        /// Ras - Toggles bool isGamestarted, (leave menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartResumeGame_Click(object sender, System.EventArgs e)
        {
            Program.AdventureMan.isGameStarted = true;
        }

        /// <summary>
        /// Ras - Exits the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, EventArgs e)
        {
            Program.AdventureMan.Exit();
        }

        /// <summary>
        /// Ras - Toggles fullscreen on and off
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FullScreen_Click(object sender, EventArgs e)
        {
            Program.AdventureMan._graphics.IsFullScreen = !Program.AdventureMan._graphics.IsFullScreen;
            Program.AdventureMan._graphics.ApplyChanges();
        }

        /// <summary>
        /// Ras - Menus backgroundssprite is set (black and drawn at 0.5f opacity to create a darker overlay when in menu)
        /// </summary>
        public override void Loadcontent()
        {
            backgroundSprite = Program.AdventureMan.content.Load<Texture2D>("blankbackground");
        }

        /// <summary>
        /// Ras - calls update on all buttons in list
        /// </summary>
        ///
        public override void Update()
        {
            foreach (Button b in buttons)
            {
                b.Update();
            }
        }

        /// <summary>
        /// Ras - Draws menus backgroundsprite, buttons in list and a string
        /// </summary>
        /// <param name="spritebatch"></param>
        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(backgroundSprite, new Rectangle(0, 0, Program.AdventureMan.SceenSize.x, Program.AdventureMan.SceenSize.y), Color.Black * 0.5f);
            spritebatch.DrawString(Program.AdventureMan.menuFont, $"Menu / Paused", new Vector2(Program.AdventureMan.SceenSize.x / 4, Program.AdventureMan.SceenSize.y / 20), Color.Black, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            foreach (Button b in buttons)
            {
                b.Draw();
            }
        }
    }
}