using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    public class SpriteAnimation
    {
        private Texture2D[] frames;
        private ulong currIndex;
        public int Length { get => frames.Length; }

        public int Width;
        public int Height;

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

        public SpriteAnimation(Texture2D[] frames, uint inverseSpeed)
        {
            this.frames = frames;
            InverseSpeed = inverseSpeed;
        }

        public static implicit operator Texture2D(SpriteAnimation a)
        {
            return a.frames[(a.currIndex % ((ulong)a.frames.Length * a.InverseSpeed)) / a.InverseSpeed];
        }

        public void Update()
        {
            ++currIndex;
        }

        public static implicit operator SpriteAnimation(Texture2D[] t)
        {
            return new SpriteAnimation(t);
        }

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