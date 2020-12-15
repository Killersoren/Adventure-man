using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using static Adventure_man.GameWorld;

namespace Adventure_man
{
    internal class Bow : Weapon
    {
        private readonly float force;
        private readonly Texture2D arrowSprite;
        private Vector2 bowOffset;
        private int offseta;
        private int offsetb;
        /// <summary>
        /// Bow constructor
        /// </summary>
        /// <param name="name">The name of the Bow</param>
        /// <param name="damage">The damage of the arrows the bow shoots</param>
        /// <param name="force">the speed of the arrows the bow shoots</param>
        /// <param name="fireRate">how many arrows the bow shoots every second</param>
        public Bow(string name, int damage, float force, float fireRate)
        {
            //base.user = user;
            base.damage = damage;
            base.name = name;
            this.force = force;
            this.fireRate = fireRate;

            arrowSprite = Program.AdventureMan.Content.Load<Texture2D>("Arrow");
        }

        /// <summary>
        /// Bow Constructor, no name (for enemy bows)
        /// </summary>
        /// <param name="damage">The damage of the arrows the bow shoots</param>
        /// <param name="force">the speed of the arrows the bow shoots</param>
        /// <param name="fireRate">how many arrows the bow shoots every second</param>
        public Bow(int damage, float force, float fireRate)
        {
            //base.user = user;
            base.damage = damage;
            this.force = force;
            this.fireRate = fireRate;

            arrowSprite = Program.AdventureMan.Content.Load<Texture2D>("Arrow");
        }

        public override void UseWeapon(Vector2 position, Direction direction,MoveableGameObject user)
        {
            if (cooldown <= 0)
            {

                CalculateOffsets(user);
                GameObject.Spawn(new Arrow(arrowSprite, position + (bowOffset + new Vector2(offsetb + offseta * (int)World.player.dir, 0)), damage, force, user, direction));
                cooldown = 1000 / fireRate;
            }
        }
        /// <summary>
        ///  uses Actor and Arrow sprite dimentions to make an offset that determines where the arrow is going to spawn.
        /// </summary>
        /// <param name="user">The user of the weapon</param>
        private void CalculateOffsets(MoveableGameObject user)
        {
            this.bowOffset = new Vector2(0, user.Size.Y / 4);
            this.offseta = ((int)user.Size.X + arrowSprite.Width) / 2;
            this.offsetb = offseta - arrowSprite.Width;
        }
    }
}