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

        public Button testButton;
        public Menu()
        {
            // Creates Start button
            Rectangle buttonRectangle = new Rectangle(10, 350, 100, 100);
            testButton = new Button(buttonRectangle, "Start");
            testButton.Click += TestButton_Click;

           Loadcontent();

        }

        private void TestButton_Click(object sender, System.EventArgs e)
        {
            // Sets game to started (changes scene to UI) 
            GameWorld.isGameStarted = true;
        }


        public override void Loadcontent()
        {
            backgroundSprite = GameWorld.content.Load<Texture2D>("deepart");
            //placeholder texture
        }
        public override void Update(GameTime gameTime)
        {
                testButton.Update(gameTime);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            // Draws background
            spritebatch.Draw(backgroundSprite, new Rectangle(0, 0, GameWorld.SceenSize.x, GameWorld.SceenSize.y), Color.Black * 0.5f);
            //spritebatch.DrawString(spritefont, "Menu / pause");
            //later

            //Draws button
            testButton.Draw(gameTime, spritebatch);
            //Draws Pause text
            spritebatch.DrawString(GameWorld.font, "Paused (Press Enter)", new Vector2(0, 0), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            //spritebatch.DrawString(GameWorld.font, "Paused", new Vector2(GameWorld.SceenSize.x / 2 , GameWorld.SceenSize.y / 100 *2), Color.White, 0f, Vector2.Zero,1f, SpriteEffects.None, 1f);
            //later
        }


    }
}
