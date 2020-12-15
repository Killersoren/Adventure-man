using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static Adventure_man.GameWorld;

namespace Adventure_man
{
    public abstract class Weapon // Sofie
    {
        public string name;
        public int damage;
        public float fireRate;
        public float cooldown;
        public MoveableGameObject user;



        /// <summary>
        /// Uses the current Weapon
        /// </summary>
        /// <param name="position">The position of the user</param>
        /// <param name="direction">The Direction the user is facing</param>
        /// <param name="user">the Actor currently attacking, so that the Actor doesn't harm themselves or others of their class, and to make a proper offset</param>
        public abstract void UseWeapon(Vector2 position,Direction direction,MoveableGameObject user);


       /// <summary>
       /// its what it says on the tin, it manages cooldown
       /// </summary>
        public void WeaponCooldown()
        {
            if (cooldown > 0)
            {
                cooldown -= (float)Program.AdventureMan.gameTime.ElapsedGameTime.TotalMilliseconds;

            }
        }
    }
}
