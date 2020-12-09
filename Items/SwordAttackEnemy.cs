using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    internal class SwordAttackEnemy : MoveableGameObject
    {
        private int damage;
        private MoveableGameObject friendly;
        private Vector2 stab;

        public SwordAttackEnemy(Texture2D sprite, Vector2 position, int damage, MoveableGameObject friendly, GameWorld.Direction direction)
        {
            dir = direction;
            this.friendly = friendly;
            Location = position;
            Sprite = sprite;
            Size = new Vector2(Sprite.Width, Sprite.Height);
            this.damage = damage;
            FlipSprite();
        }

        public override void LoadContent(ContentManager contentManager)
        {
        }

        public override void Update()
        {
            // Ras - Changes swords velocity increasingly, and removes it if too far away from the player,
            // missing: rotate, size, effect ?
            if (stab.X > origin.X + 10 || stab.X < origin.X - 10)
            {
                Destroy(this);
            }
            else
            {
                //
                stab.X = stab.X + (int)dir;

                velocity = stab + World.Player.velocity;
            }

            // Ras - Edited version of arrow, sword is not removed upon collision but dmg is stil taken
            //foreach (var other in Program.AdventureMan.CurrentWorld.Objects)
            //    if (HitBox.Intersects(other.HitBox))
            //    {
            //        if (other is Player && other != friendly)
            //        {
            //            ((Player)other).TakeDamage(damage);
            //        }
            //    }

            base.Update();
        }

        public override void OnCollision(GameObject collisionTarget)
        {
            if (collisionTarget is Player && collisionTarget != friendly)
            {
                ((Player)collisionTarget).TakeDamage(damage);
            }

            base.OnCollision(collisionTarget);
        }
    }
}