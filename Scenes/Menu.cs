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
        

        private readonly List<Button> buttons;

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

        public override void Update()
        {
            foreach (Button b in buttons)
            {
                b.Update();
            }
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(backgroundSprite, new Rectangle(0, 0, Program.AdventureMan.SceenSize.x, Program.AdventureMan.SceenSize.y), Color.Black * 0.5f);
            spritebatch.DrawString(Program.AdventureMan.menuFont, $" Menu / Paused (Enter)", new Vector2(Program.AdventureMan.SceenSize.x/4, Program.AdventureMan.SceenSize.y/20), Color.Black , 0f, Vector2.Zero, 2f,SpriteEffects.None,0f);



            foreach (Button b in buttons)
            {
                b.Draw();
            }
        }
    }
}