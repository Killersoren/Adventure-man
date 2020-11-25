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

        public Texture2D backgroundSprite;

        public UI()
        {

            //Texture2D buttonSprite = Content.Load<Texture2D>("button");
            //Rectangle buttonRectangle = new Rectangle(10, 350, 100, 100);
            //Button testButton = new Button(buttonSprite, buttonRectangle);
            //testButton.Click += TestButton_Click;

            //gameObject.Add(testButton);
            Loadcontent();
        }

        private void TestButton_Click(object sender, EventArgs e)
        {

        }

        public override void Loadcontent()
        {
         backgroundSprite = GameWorld.content.Load<Texture2D>("bar");


        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {

            spritebatch.Draw(backgroundSprite, new Rectangle(0, 0, GameWorld.SceenSize.x, GameWorld.SceenSize.y), Color.White * 0.5f);

        }


        public override void Update(GameTime gameTime)
        {
        }

       
    }
}
