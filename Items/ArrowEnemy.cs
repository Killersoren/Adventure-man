using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Adventure_man
{
    internal class ArrowEnemy : MoveableGameObject
    {
        private Vector2 oldLoc;
        private int damage;
        private MoveableGameObject friendly;//the object that shot the arrow

        public ArrowEnemy(Texture2D sprite, Vector2 position, int damage, float speed, MoveableGameObject friendly, GameWorld.Direction direction)
        {
            dir = direction;
            this.friendly = friendly;
            Location = position;
            base.velocity = new Vector2((int)dir, 0);
            Sprite = sprite;
            Size = new Vector2(Sprite.Width, Sprite.Height);
            this.damage = damage;
            base.speed = speed;
            FlipSprite();
        }

        public override void LoadContent(ContentManager contentManager)
        {
        }

        public override void Update()
        {
            if (Location.X > Program.AdventureMan.CurrentWorld.worldSize.X || Location.X < 0)
                Destroy(this);

            base.Update();
        }

        public override void OnCollision(GameObject collisionTarget)
        {
            if (collisionTarget is Platform)
            {
                Destroy(this);
            }
            if (collisionTarget is Player && collisionTarget != friendly)
            {
                ((Player)collisionTarget).TakeDamage(damage);
                Destroy(this);
            }
            base.OnCollision(collisionTarget);
        }
    }
}