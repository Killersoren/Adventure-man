using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    class Menu : Scene
    {

        List<GameObject> gameObject = new List<GameObject>();
        public Menu()
        {
            Texture2D buttonSprite = Content.Load<Texture2D>("button");
            Rectangle buttonRectangle = new Rectangle(10, 350, 100, 100);
            Button testButton = new Button(buttonSprite, buttonRectangle);
            testButton.Click += TestButton_Click;

            gameObject.Add(testButton);
        }



        private void TestButton_Click(object sender, System.EventArgs e)
        {
            //StartGame();

        }

        public override void Update(GameTime gameTime)
        {

            foreach (GameObject obj in gameObject)
            {
                obj.Update(gameTime);
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {

            foreach (GameObject obj in gameObject)
            {
                obj.Draw(gameTime, spritebatch);
            }
        }


    }
}
