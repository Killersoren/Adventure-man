using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;


namespace Adventure_man
{
    class UI : Scene
    {
        public UI()
        {

            //Texture2D buttonSprite = Content.Load<Texture2D>("button");
            //Rectangle buttonRectangle = new Rectangle(10, 350, 100, 100);
            //Button testButton = new Button(buttonSprite, buttonRectangle);
            //testButton.Click += TestButton_Click;

            //gameObject.Add(testButton);
        }

        private void TestButton_Click(object sender, EventArgs e)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
        }


        public override void Update(GameTime gameTime)
        {
        }
    }
}
