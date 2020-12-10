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
        private Color hoverColor;
        private Color currentColor;
        private MouseState mouseCurrent;
        private MouseState mouseLast;
        private Rectangle mouseRectangle;
        private Rectangle buttonRectangle;
        private readonly string buttonDescription;
        private Texture2D sprite;
        public event EventHandler Click;

        /// <summary>
        /// Ras - Button constructor, takes parameter position x/y and buttondescription and creates a rectangle and sets its description text
        /// All buttons sprites are set to "button" texture and hover color is set
        /// </summary>
        /// <param name="positionX"></param>
        /// <param name="positionY"></param>
        /// <param name="buttonDescription"></param>
        public Button(int positionX, int positionY, string buttonDescription)
        {
            hoverColor = Color.Gray;
            this.buttonRectangle = new Rectangle(positionX,positionY, 120,50);
            this.buttonDescription = buttonDescription;
            Loadcontent();
        }


        /// <summary>
        /// Ras - Loads buttons sprite texture
        /// </summary>
        public void Loadcontent()
        {
            sprite = Program.AdventureMan.content.Load<Texture2D>("button");
        }
         

        /// <summary>
        /// Ras - Stores current and last mouse states and sets current position
        /// Changes button color if mouse rectangle intersects with button rectangle
        /// and invokes button content if last mouse state is clicked and current is realease, invokes when click is over, instead of on click
        /// Resets color after hovering
        /// </summary>
        public void Update()
        {
            mouseLast = mouseCurrent;
            mouseCurrent = Mouse.GetState();
            mouseRectangle = new Rectangle(mouseCurrent.X, mouseCurrent.Y, 1, 1);
            if (mouseRectangle.Intersects(buttonRectangle))
            {
                this.currentColor = hoverColor;
                if (mouseLast.LeftButton == ButtonState.Pressed && mouseCurrent.LeftButton == ButtonState.Released)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
            else
            {
                this.currentColor = Color.White;
            }
         
        }
       
        
        /// <summary>
        /// Ras - Draws button and its description text in middle of button
        /// </summary>
        public void Draw()
        {
            Program.AdventureMan._spriteBatch.Draw(sprite, buttonRectangle, currentColor);
            var x = (buttonRectangle.X + (buttonRectangle.Width / 2)) - (Program.AdventureMan.font.MeasureString(buttonDescription).X / 2);
            var y = (buttonRectangle.Y + (buttonRectangle.Height / 2)) - (Program.AdventureMan.font.MeasureString(buttonDescription).Y / 2);
            Program.AdventureMan._spriteBatch.DrawString(Program.AdventureMan.menuFont, buttonDescription, new Vector2(x, y), Color.Black);
        }
    }
}