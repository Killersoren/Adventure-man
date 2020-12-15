using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    internal class SwordAttack : MoveableGameObject
    {
        private readonly int damage;
        private readonly MoveableGameObject friendly;
        private Vector2 stab;

        /// <summary>
        /// Ras - SwordAttacks constructor, sets parameters to this and calls flipsprite because direction only matters upon attacking
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="position"></param>
        /// <param name="damage"></param>
        /// <param name="friendly"></param>
        /// <param name="direction"></param>
        public SwordAttack(Texture2D sprite, Vector2 position, int damage, MoveableGameObject friendly, GameWorld.Direction direction)
        {
            this.dir = direction;
            this.friendly = friendly;
            this.Location = position;
            this.Sprite = sprite;
            this.Size = new Vector2(Sprite.Width, Sprite.Height);
            this.damage = damage;
            FlipSprite();
        }

        /// <summary>
        /// Ras - Empty for now
        /// </summary>
        /// <param name="contentManager"></param>
        public override void LoadContent(ContentManager contentManager)
        {
        }


        /// <summary>
        /// Ras - Changes swords velocity increasingly, and removes it when its too far away from the player,
        /// </summary>
        public override void Update()
        {
            if (stab.X > origin.X + 10 || stab.X < origin.X - 10)
            {
                Destroy(this);
            }
            else
            {
                stab.X += (int)dir;
                velocity = stab + World.player.velocity;
            }
            base.Update();
        }

        /// <summary>
        /// Ras - Collision for swordattack, if collisiontarget is enemy or player and not friendly (itself), actor takes dmg. 
        /// </summary>
        /// <param name="collisionTarget"></param>
        public override void OnCollision(GameObject collisionTarget)
        {
            if (collisionTarget is Enemy enemy && collisionTarget != friendly && friendly is Player)
            {
                enemy.TakeDamage(damage);
            }
            if (collisionTarget is Player player && collisionTarget != friendly)
            {
                player.TakeDamage(damage);
            }
            base.OnCollision(collisionTarget);
        }
    }
}