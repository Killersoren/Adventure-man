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
        private Rectangle rectangle;
        private SpriteAnimation sprite;

        public event EventHandler Click;

        public Button(Rectangle rectangle)
        {
            this.rectangle = rectangle;
            this.currentColor = defaultColor;
            this.sprite = Program.AdventureMan.content.Load<Texture2D>("button");
        }

        public void Update()
        {
            mouseLast = mouseCurrent;
            mouseCurrent = Mouse.GetState();
            mouseRectangle = new Rectangle(mouseCurrent.X, mouseCurrent.Y, 1, 1);

            hover = false;
            if (mouseRectangle.Intersects(rectangle))
            {
                hover = true;
                if (mouseLast.LeftButton == ButtonState.Pressed && mouseCurrent.LeftButton == ButtonState.Released)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            if (hover == true)
            {
                this.currentColor = hoverColor;
            }
            else
            {
                this.currentColor = defaultColor;
            }
            spritebatch.Draw(sprite, rectangle, currentColor);
        }
    }
}