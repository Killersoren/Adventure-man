using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Adventure_man
{
    class Button : GameObject
    {
        private bool hover;
        private Color hoverColor = Color.Gray;
        private Color defaultColor = Color.White;
        private MouseState mouseCurrent;
        private MouseState mouseLast;
        private Rectangle mouseRectangle;
        public event EventHandler Click;
        public Button(Rectangle rectangle)
        {
            this.rectangle = rectangle;
            this.color = defaultColor;
            this.sprite = GameWorld.content.Load<Texture2D>("button");

        }
        public override void LoadContent()
        {
        }
        public override void Update(GameTime gameTime)
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
        public override void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            if (hover == true)
            {
                this.color = hoverColor;
            }
            else
            {
                this.color = defaultColor;

            }
            spritebatch.Draw(sprite, rectangle, color);
        }


    }
}

