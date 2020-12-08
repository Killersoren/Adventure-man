using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Adventure_man
{
    internal class Button
    {
        private bool hover;
        private Color hoverColor = Color.Gray;
        private Color defaultColor = Color.White;
        private Color currentColor = Color.White;
        private MouseState mouseCurrent;
        private MouseState mouseLast;
        private Rectangle mouseRectangle;
        private string buttonDescription;
        private Rectangle rectangle;
        private Texture2D sprite;

        public event EventHandler Click;

        /// <summary>
        /// Button constructor
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="buttonDescription"></param>
        public Button(int positionX, int positionY, string buttonDescription)
        {
            this.rectangle = new Rectangle(positionX,positionY, 120,50);
            this.sprite = Program.AdventureMan.content.Load<Texture2D>("button");
            this.buttonDescription = buttonDescription;
        }

        public void Update(GameTime gameTime)
        {
            // Stores current and last mouse states and sets current position
            mouseLast = mouseCurrent;
            mouseCurrent = Mouse.GetState();
            mouseRectangle = new Rectangle(mouseCurrent.X, mouseCurrent.Y, 1, 1);
            // Resets hover to false after hovering
            hover = false;
            // Sets hover to true if mouse cursor intersects with button rectangle
            if (mouseRectangle.Intersects(rectangle))
            {
                hover = true;
                // Invokes button content if last mouse state is clicked and current is realease, invokes when click is over, instead of on click
                if (mouseLast.LeftButton == ButtonState.Pressed && mouseCurrent.LeftButton == ButtonState.Released)
                {
                    Click?.Invoke(this, new EventArgs());
                }

            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            // Toggles color of button depending on hover
            if (hover == true)
            {
                this.currentColor = hoverColor;
            }
            else
            {
                this.currentColor = defaultColor;
            }
            // Draws button
            spritebatch.Draw(sprite, rectangle, currentColor);
            // Draws buttons description text in middle of button
            var x = (rectangle.X + (rectangle.Width / 2)) - (Program.AdventureMan.font.MeasureString(buttonDescription).X / 2);
            var y = (rectangle.Y + (rectangle.Height / 2)) - (Program.AdventureMan.font.MeasureString(buttonDescription).Y / 2); //my font was broken so comment this out to test
            spritebatch.DrawString(Program.AdventureMan.menuFont, buttonDescription, new Vector2(x, y), Color.Black);
        }
    }
}