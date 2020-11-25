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
        private List<Button> buttons = new List<Button>();
        public Texture2D backgroundSprite;

        public Menu()
        {
            Rectangle buttonRectangle = new Rectangle(10, 350, 100, 100);
            Button testButton = new Button(buttonRectangle);
            testButton.Click += TestButton_Click;

            Loadcontent();
            buttons.Add(testButton);
        }

        //  this.sprite = GameWorld.content.Load<Texture2D>("button");

        public override void Loadcontent()
        {
            backgroundSprite = Program.AdventureMan.content.Load<Texture2D>("deepart");
        }

        private void TestButton_Click(object sender, System.EventArgs e)
        {
            //GameWorld.StartGame();

            //List<Scene> tempscene = new List<Scene>();
            //tempscene = GameWorld.loadedScenes;

            //    tempscene.RemoveAt(0);
            //GameWorld.loadedScenes = tempscene;

            Program.AdventureMan.isGameStarted = true;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Button b in buttons)
            {
                b.Update();
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            spritebatch.Draw(backgroundSprite, new Rectangle(0, 0, Program.AdventureMan.SceenSize.x, Program.AdventureMan.SceenSize.y), Color.White);
            //spritebatch.DrawString(spritefont, "Menu / pause");

            foreach (Button b in buttons)
            {
                b.Draw(gameTime, spritebatch);
            }
        }
    }
}