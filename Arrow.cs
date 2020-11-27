using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    internal class Arrow : MoveableGameObject
    {
        private int damage;
        public Arrow(int damage, float speed)
        {
            this.damage = damage;
            this.speed = speed;
        }

        public Arrow Shoot(Vector2 position, Vector2 velocity)
        {
            Arrow arrow = this;
            arrow.Location = position;
            arrow.Velocity = velocity;
            
            return arrow;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            //var sprites = new Texture2D[1];
            //sprites[0] = Program.AdventureMan.Content.Load<Texture2D>("Arrow");
            //sprites[1] = Program.AdventureMan.Content.Load<Texture2D>("PlatformTest");
            //Sprite = new SpriteAnimation(sprites);
            Sprite= Program.AdventureMan.Content.Load<Texture2D>("Arrow");

            Size = new Vector2(Sprite.Width, Sprite.Height);
        }

        public override void Update()
        {

            if (Location.X > Program.AdventureMan.CurrentWorld.screenSize.X || Location.X < 0)
                Destroy(this);

            foreach(var o in Program.AdventureMan.CurrentWorld.Objects)


            base.Update();
        }
    }
}