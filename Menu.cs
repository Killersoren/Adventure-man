﻿using Microsoft.Xna.Framework;
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
        public Menu()
        {
            // Creates Start button
            Rectangle buttonRectangle = new Rectangle(10, 350, 100, 100);
            testButton = new Button(buttonRectangle, "Start");
            testButton.Click += TestButton_Click;

            Loadcontent();
            gameObject.Add(testButton);
        }
        //  this.sprite = GameWorld.content.Load<Texture2D>("button");

        public override void Loadcontent()
        {
            backgroundSprite = GameWorld.content.Load<Texture2D>("deepart");
        }

        private void TestButton_Click(object sender, System.EventArgs e)
        {
            //GameWorld.StartGame();

            //List<Scene> tempscene = new List<Scene>();
            //tempscene = GameWorld.loadedScenes;

            //    tempscene.RemoveAt(0);
            //GameWorld.loadedScenes = tempscene;

            GameWorld.isGameStarted = true;
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
            spritebatch.Draw(backgroundSprite, new Rectangle(0, 0, GameWorld.SceenSize.x, GameWorld.SceenSize.y), Color.White);
            //spritebatch.DrawString(spritefont, "Menu / pause");

            foreach (GameObject obj in gameObject)
            {
                obj.Draw(gameTime, spritebatch);
            }
        }
    }
}