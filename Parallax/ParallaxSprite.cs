using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    public class ParallaxSprite
    {
        protected Texture2D sprite;
        private Vector2 SpriteOffset;
        public Vector2 position;
        //private Texture2D tree;
        //private Texture2D cloud;
        //private Texture2D ground;
        //private Texture2D sun;



        /// <summary>
        ///  Ras - ParralaxSprites constructor, sprite and offset is set from parameters
        /// </summary>
        /// <param name="Sprite"></param>
        public ParallaxSprite(Texture2D Sprite, Vector2 offset)
        {
            sprite = Sprite;
            SpriteOffset = offset;
           // Loadcontent();
        }

        //public void Loadcontent()
        //{
            
        //    tree = Program.AdventureMan.content.Load<Texture2D>("tree");
        //    cloud = Program.AdventureMan.content.Load<Texture2D>("clouds");
        //    ground = Program.AdventureMan.content.Load<Texture2D>("ground");
        //    sun = Program.AdventureMan.content.Load<Texture2D>("sun");

        //}


        /// <summary>
        /// Ras - Draws sprite 
        /// </summary>
        public void Draw()
        {
            Program.AdventureMan._spriteBatch.Draw(sprite, position, null, Color.White, 0, SpriteOffset, 1f, SpriteEffects.None, 0f);
        }
    }

}
