using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Adventure_man
{
    internal class Arrow : MoveableGameObject
    {

        private readonly int damage;
        private readonly MoveableGameObject friendly;//the object that shot the arrow
        /// <summary>
        /// Makes and shoots an arrow
        /// </summary>
        /// <param name="sprite">loaded in the weapon</param>
        /// <param name="position">the position of the arrow (Calculated so that it is reletive to the Shooter)</param>
        /// <param name="damage">Damage that the arrow deals</param>
        /// <param name="speed">Speed if the arrow</param>
        /// <param name="friendly">Who shot the arrow</param>
        /// <param name="direction">What direction where the person that shot the arrow facing when they shot it</param>
        public Arrow(Texture2D sprite, Vector2 position, int damage, float speed, MoveableGameObject friendly, GameWorld.Direction direction)
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


        /// <summary>
        /// Relic that has to be here
        /// </summary>
        /// <param name="contentManager"></param>
        public override void LoadContent(ContentManager contentManager)
        {

        }

        public override void Update()
        {
            if (Location.X > Program.AdventureMan.CurrentWorld.worldSize.X || Location.X < 0) // Makes it so that an arrow dissapears when it is outside of world limits, since you cant shoot upward it only checks X.
                Destroy(this);

            base.Update();
        }

        public override void OnCollision(GameObject collisionTarget)
        {
            if (collisionTarget is Platform) // Makes the arrow dissapar when it hits a platform
            {
                Destroy(this);
            }
            if (collisionTarget is Enemy enemy && collisionTarget != friendly && friendly is Player) // Makes it so that an enemy cant hurt itself, and so that only players can harm enemies (now that i say it the first one is now kind of obsolete)
            {
                enemy.TakeDamage(damage);
                Destroy(this);
            }
            if (collisionTarget is Player player && collisionTarget != friendly)// Makes it so that a player cant harm themselves
            {
                player.TakeDamage(damage);
                Destroy(this);
            }
            base.OnCollision(collisionTarget);
        }
    }
}