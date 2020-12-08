using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure_man
{
    public class Parallax
    {
        private bool alwaysMoving;
        private Vector2 offset;

        private float scrollSpeed;
        private List<ParallaxSprite> parallaxSprites;
        private float parallaxSpeed;
      
        public Vector2 Offset
        {
            get { return offset; }
            set
            {
                offset = value;
                foreach (var sprite in parallaxSprites)
                {
                    sprite.Offset = offset;
                }
            }
        }

        public Parallax(Texture2D sprite, float ScrollSpeed, int Amount, bool AlwaysMoving = false)
            : this(new List<Texture2D>(Enumerable.Repeat(sprite, Amount).ToList()), ScrollSpeed, AlwaysMoving)
        {
        }

        private Parallax(List<Texture2D> Sprite, float ScrollSpeed, bool AlwaysMoving = false)
        {
            parallaxSprites = new List<ParallaxSprite>();
            scrollSpeed = ScrollSpeed;
            alwaysMoving = AlwaysMoving;
            for (int i = 0; i < Sprite.Count; i++)
            {
                var sprite = Sprite[i];
                parallaxSprites.Add(new ParallaxSprite(sprite)
                { position = new Vector2((i * sprite.Width) - 1, Program.AdventureMan.SceenSize.y - sprite.Width) }
                );
            }
        }


        public void Update()
        {
            ApplySpeed();
            // CheckPosition();
        }

        //private void CheckPosition()
        //{
        //    for (int i = 0; i < parallaxSprites.Count; i++)
        //    {
        //        var sprite = parallaxSprites[i];
        //        if (sprite.Rectangle.Right <- +200)
        //        {
        //            var index = i - 1;
        //            if (index < 0)
        //            {
        //                index = parallaxSprites.Count - 1;
        //            }
        //            sprite.position.X = parallaxSprites[index].Rectangle.Right - (parallaxSpeed * 2);
        //        }
        //    }
        //}

        private void ApplySpeed()
        {
            parallaxSpeed = (float)(scrollSpeed*Program.AdventureMan.gameTime.ElapsedGameTime.TotalSeconds);
            if (!alwaysMoving)
            {
                parallaxSpeed *= World.Player.velocity.X;
            }
            foreach (var sprite in parallaxSprites)
            {
                sprite.position.X -= parallaxSpeed;
            }
        }

        public void Draw()
        {
            foreach (var sprite in parallaxSprites)
            {
                sprite.Draw();
            }
        }

    }
}
