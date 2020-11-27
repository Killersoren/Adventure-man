using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    class Arrow : MoveableGameObject
    {

        public Arrow(Vector2 position,Vector2 velocity, float speed)
        {
            Location = position;
            base.velocity = velocity;
            base.speed = speed;


        }

        public override void LoadContent(ContentManager contentManager)
        {
            var sprites = new Texture2D[2];
            sprites[0] = Program.AdventureMan.Content.Load<Texture2D>("PlatformTest");
            sprites[1] = Program.AdventureMan.Content.Load<Texture2D>("PlatformTest");
            Sprite = new SpriteAnimation(sprites);
            Size = new Vector2(Sprite.Width, Sprite.Height);
        }
        public override void Update()
        {
            base.Update();
        }
    }
}
