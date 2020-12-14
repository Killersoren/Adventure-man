using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    /// <summary>
    /// Magnus - A class for containing the multiple textures that make up and animation
    /// </summary>
    public class SpriteAnimation
    {
        private Texture2D[] frames;
        private ulong currIndex;

        /// <summary>
        /// The amount of textures in the animation
        /// </summary>
        public int Length { get => frames.Length; }

        /// <summary>
        /// The width of the first texture in pixels
        /// </summary>
        public int Width;

        /// <summary>
        /// The height of the first texture in pixels
        /// </summary>
        public int Height;

        /// <summary>
        /// Resets the animation to the first texture
        /// </summary>
        public void Restart()
        {
            currIndex = 0;
        }

        /// <summary>
        /// How many frames each texture is repeated, lover numbers means faster animantions
        /// </summary>
        public uint InverseSpeed = 10;

        public SpriteAnimation(Texture2D[] frames)
        {
            this.frames = frames;
            Width = this.frames[0].Width;
            Height = this.frames[0].Height;
        }

        /// <summary>
        /// Creates a new SpriteAnimation
        /// </summary>
        /// <param name="frames">The textures making up the animation, in order.</param>
        /// <param name="inverseSpeed">How many frames each texture should appear on screen i a row</param>
        public SpriteAnimation(Texture2D[] frames, uint inverseSpeed)
        {
            this.frames = frames;
            InverseSpeed = inverseSpeed;
        }

        /// <summary>
        /// Returns the current texture from an animation
        /// </summary>
        /// <param name="a">The animation to return a texture from</param>
        public static implicit operator Texture2D(SpriteAnimation a)
        {
            return a.frames[(a.currIndex % ((ulong)a.frames.Length * a.InverseSpeed)) / a.InverseSpeed];
        }

        /// <summary>
        /// Updates the texture, this should be caled every frame you want the animation to play
        /// </summary>
        public void Update()
        {
            ++currIndex;
        }

        /// <summary>
        /// Converts an array of Texture2D's to a new SpriteAnimation
        /// </summary>
        public static implicit operator SpriteAnimation(Texture2D[] t)
        {
            return new SpriteAnimation(t);
        }

        /// <summary>
        /// Converts a single Texture2D's to a new SpriteAnimation
        /// </summary>
        public static implicit operator SpriteAnimation(Texture2D t)
        {
            return new SpriteAnimation(new Texture2D[] { t });
        }

        /// <summary>
        /// Gets next frame
        /// </summary>
        /// <returns></returns>
        //public Texture2D GetNextFrame()
        //{
        //    return frames[(++currIndex % ((ulong)frames.Length * InverseSpeed)) / InverseSpeed];
        //}
    }
}