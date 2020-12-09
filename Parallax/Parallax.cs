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

        /// <summary>
        /// - Ras Property that sets the offset for each parallax sprites in the list parallaxSprites.
        /// </summary>
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
        /// <summary>
        /// - Ras Parallax public constructor, the constructor that is called from other clases. 
        ///  Calls its private constructor with a list of "sprites" * "Amount"
        ///  Bool "alwaysMoving" sets the sprites to always move even if player is not (clouds and sun) is sat to false if not given
        ///  "ScrollSpeed" is the speed of the sprites scrolling.
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="ScrollSpeed"></param>
        /// <param name="Amount"></param>
        /// <param name="AlwaysMoving"></param>
        public Parallax(Texture2D sprite, float ScrollSpeed, int Amount, bool AlwaysMoving = false)
            : this(new List<Texture2D>(Enumerable.Repeat(sprite, Amount).ToList()), ScrollSpeed, AlwaysMoving)
        {
        }
        /// <summary>
        /// - Ras Parallax private constructor, sets scrollSpeed and alwaysMoving bool 
        ///  Creates a list of parallaxSprites and adds a new ParallaxSprite for each sprite in SpriteList
        ///  Position is sat for each sprite in the list acording to:
        ///  X = its Width multiplied with its number in the list to spread out the sprites in the parallax
        /// </summary>
        /// <param name="SpriteList"></param>
        /// <param name="ScrollSpeed"></param>
        /// <param name="AlwaysMoving"></param>
        private Parallax(List<Texture2D> SpriteList, float ScrollSpeed, bool AlwaysMoving = false)
        {
            parallaxSprites = new List<ParallaxSprite>();
            scrollSpeed = ScrollSpeed;
            alwaysMoving = AlwaysMoving;
            for (int i = 0; i < SpriteList.Count; i++)
            {
                var sprite = SpriteList[i];
                parallaxSprites.Add(new ParallaxSprite(sprite)
                { position = new Vector2((i * sprite.Width) , 0) }
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
        /// <summary>
        /// Ras - 
        /// </summary>
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
