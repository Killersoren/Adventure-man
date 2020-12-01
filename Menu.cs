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
        public Button testButton;
        private List<Button> buttons;

        public Menu()
        {
            // Creates Start button
            Rectangle buttonRectangle = new Rectangle(10, 350, 100, 100);
            testButton = new Button(buttonRectangle, "Start");
            testButton.Click += TestButton_Click;

            buttons = new List<Button>();

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
            //spritebatch.DrawString(spritefont, "Menu / pause");

            foreach (Button b in buttons)
            {
                b.Draw(gameTime, spritebatch);
            }
        }
    }
}