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
        private Vector2 oldLoc;
        private int damage;
        private SpriteEffects effect;
        private Vector2 origin;

        public Arrow(Texture2D sprite,Vector2 position, int damage, float speed)
        {
            dir = World.Player.dir;
            Location = position;
            this.velocity = new Vector2((int)dir,0);
            Sprite = sprite;
            Size = new Vector2(Sprite.Width, Sprite.Height);
            this.damage = damage;
            this.speed = speed;
            FlipSprite();
            
            origin = new Vector2(Sprite.Width * (int)dir, Sprite.Height / 2);


        }

        //public Arrow Shoot(Vector2 position, Vector2 velocity)
        //{
        //    Arrow arrow = this;
        //    arrow.Location = position;
        //    arrow.velocity = velocity;
            
        //    return arrow;
        //}

        public override void LoadContent(ContentManager contentManager)
        {
            //var sprites = new Texture2D[1];
            //sprites[0] = Program.AdventureMan.Content.Load<Texture2D>("Arrow");
            //sprites[1] = Program.AdventureMan.Content.Load<Texture2D>("PlatformTest");
            //Sprite = new SpriteAnimation(sprites);
            //Sprite= Program.AdventureMan.Content.Load<Texture2D>("Arrow");

            //Size = new Vector2(Sprite.Width, Sprite.Height);
        }

        public override void Update()
        {

            if (Location.X > Program.AdventureMan.CurrentWorld.screenSize.X || Location.X < 0)
                Destroy(this);


            foreach (var other in Program.AdventureMan.CurrentWorld.Objects)
                if (HitBox.Intersects(other.HitBox))
                {
                    if (other is Platform)
                    {
                        Destroy(this);
                    }
                    if (other is Enemy)
                    {
                        ((Enemy)other).TakeDamage(damage);
                        Destroy(this);
                    }

                }

            base.Update();

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Location, null, Color.White, 0, origin, 1, effect, 1);
        }
    }
}