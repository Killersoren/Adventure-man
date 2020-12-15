using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure_man
{
    public class ParallaxLayer
    {
        private readonly List<ParallaxSprite> parallaxSprites;
        private readonly bool alwaysMoving;
        private readonly float scrollSpeed;
        private float parallaxSpeed;


        /// <summary>
        /// - Ras Parallax public constructor, the constructor that is called from other clases. 
        ///  Calls its private constructor with a list of "sprites" * "Amount" added with Enumerable.Repeat
        ///  Bool "alwaysMoving" sets the sprites to always move even if player is not (clouds and sun) is sat to false if not given
        ///  "ScrollSpeed" is the speed of the sprites scrolling.
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="scrollSpeed"></param>
        /// <param name="amount"></param>
        /// <param name="alwaysMoving"></param>
        public ParallaxLayer(Texture2D sprite, float scrollSpeed, int amount, Vector2 offset, bool alwaysMoving = false)
            : this(new List<Texture2D>(Enumerable.Repeat(sprite, amount).ToList()), scrollSpeed, offset, alwaysMoving)
        { }


        /// <summary>
        /// - Ras Parallax private constructor, sets scrollSpeed and alwaysMoving bool 
        ///  Creates a list of parallaxSprites and adds a new ParallaxSprite for each sprite in SpriteList with a offset 
        ///  Position is sat with overloading acording to:
        ///  X = its Width multiplied with its number in the list to spread out the sprites in the parallax
        /// </summary>
        /// <param name="SpriteList"></param>
        /// <param name="scrollSpeed"></param>
        /// <param name="alwaysMoving"></param>
        private ParallaxLayer(List<Texture2D> SpriteList, float scrollSpeed, Vector2 offset, bool alwaysMoving = false)
        {
            parallaxSprites = new List<ParallaxSprite>();
            this.scrollSpeed = scrollSpeed;

            this.alwaysMoving = alwaysMoving;
            for (int i = 0; i < SpriteList.Count; i++)
            {
                var sprite = SpriteList[i];
                parallaxSprites.Add(new ParallaxSprite(sprite, offset) { position = new Vector2((i * sprite.Width), 0) }
                );
            }
        }

        /// <summary>
        /// Ras - Calls Applyspeed
        /// </summary>
        public void Update()
        {
            ApplySpeed();
        }


        /// <summary>
        /// Ras - Sets speed of all parallax sprites to their scrollspeed  modifier * time and thoose that are not always moving are multiplied with the players movement.
        /// X position is then subtracted with the speed. 
        /// </summary>
        private void ApplySpeed()
        {
            parallaxSpeed = (float)(scrollSpeed * Program.AdventureMan.gameTime.ElapsedGameTime.TotalSeconds);
            if (!alwaysMoving)
            {
                parallaxSpeed *= World.player.velocity.X;
            }
            foreach (var parallaxSprite in parallaxSprites)
            {
                parallaxSprite.position.X -= parallaxSpeed;
            }
        }

        /// <summary>
        /// Ras - Draws each sprite from list 
        /// </summary>
        public void Draw()
        {
            foreach (var sprite in parallaxSprites)
            {
                sprite.Draw();
            }
        }

    }
}
