using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    public class Camera
    {
        public Vector2 Position;
        public Vector2 Offset;

        public Camera()
        {
            Offset = new Vector2(Program.AdventureMan.Window.ClientBounds.Width, Program.AdventureMan.Window.ClientBounds.Height);
        }

        /// <summary>
        /// Gives World position to screen
        /// </summary>
        /// <param name="worldCoords"></param>
        /// <returns></returns>
        public Vector2 WorldToScreen(Vector2 worldCoords)
        {
            return worldCoords - Position + Offset / 2;
        }

        /// <summary>
        /// Gives screen position to world
        /// </summary>
        /// <param name="screenCoords"></param>
        /// <returns></returns>
        public Vector2 ScreenToWorld(Vector2 screenCoords)
        {
            return screenCoords + Position - Offset / 2;
        }
    }
}