﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    public class SpriteAnimation
    {
        private Texture2D[] Frames;
        private ulong currIndex;
        public int Length { get => Frames.Length; }

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
            Frames = frames;
            Width = Frames[0].Width;
            Height = Frames[0].Height;
        }

        public SpriteAnimation(Texture2D[] frames, uint inverseSpeed)
        {
            Frames = frames;
            InverseSpeed = inverseSpeed;
        }

        public static implicit operator Texture2D(SpriteAnimation a)
        {
            return a.GetNextFrame();
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
        public Texture2D GetNextFrame()
        {
            return Frames[(++currIndex % ((ulong)Frames.Length * InverseSpeed)) / InverseSpeed];
        }
    }
}