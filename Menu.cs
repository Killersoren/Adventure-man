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
        public Button startResumeGame;
        public Button exit;
        public Button fullScreen;
        

        private List<Button> buttons;

        public Menu()
        {
            // Creates buttons
            Rectangle buttonRectangle = new Rectangle(Program.AdventureMan.SceenSize.x/100 *1, Program.AdventureMan.SceenSize.y /100 *30, 150, 100);
            Rectangle exitButtonRectangle = new Rectangle(Program.AdventureMan.SceenSize.x / 100 * 1, Program.AdventureMan.SceenSize.y / 100 * 50, 150, 100);
            Rectangle FullscreenButtonRectangle = new Rectangle(Program.AdventureMan.SceenSize.x / 100 * 1, Program.AdventureMan.SceenSize.y / 100 * 40, 150, 100);



            startResumeGame = new Button(buttonRectangle, "Start");
            exit = new Button(exitButtonRectangle, "Exit");
            fullScreen = new Button(FullscreenButtonRectangle, "Fullscreen");
            startResumeGame.Click += StartResumeGame_Click;
            exit.Click += Exit_Click;
            fullScreen.Click += FullScreen_Click;

            buttons = new List<Button>();

            Loadcontent();
            buttons.Add(startResumeGame);
            buttons.Add(exit);
            buttons.Add(fullScreen);


        }

        private void FullScreen_Click(object sender, EventArgs e)
        {
            Program.AdventureMan._graphics.IsFullScreen = !Program.AdventureMan._graphics.IsFullScreen;
            Program.AdventureMan._graphics.ApplyChanges();
        }

        private void Exit_Click(object sender, EventArgs e)
        {

            Program.AdventureMan.Exit();
        }


        public override void Loadcontent()
        {
            backgroundSprite = Program.AdventureMan.content.Load<Texture2D>("deepart");
        }

        private void StartResumeGame_Click(object sender, System.EventArgs e)
        {
            Program.AdventureMan.isGameStarted = true;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Button b in buttons)
            {
                b.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            spritebatch.Draw(backgroundSprite, new Rectangle(0, 0, Program.AdventureMan.SceenSize.x, Program.AdventureMan.SceenSize.y), Color.Black * 0.5f);
            spritebatch.DrawString(Program.AdventureMan.font, " Menu / Paused ", new Vector2(Program.AdventureMan.SceenSize.x/4, Program.AdventureMan.SceenSize.y/20), Color.Black , 0f, Vector2.Zero, 3f,SpriteEffects.None,0f);



            foreach (Button b in buttons)
            {
                b.Draw(gameTime, spritebatch);
            }
        }
    }
}