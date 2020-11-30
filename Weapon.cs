using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static Adventure_man.GameWorld;

namespace Adventure_man
{
    abstract class Weapon
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
        public abstract void UseWeapon(Vector2 position,Direction direction);

        public void WeaponCooldown()
        {
            
            if (cooldown > 0)
            {
                cooldown -= (float)Program.AdventureMan.gameTime.ElapsedGameTime.TotalMilliseconds;
                //World.Player.color = Color.Red;

            }
            //else
            //{
            //    //World.Player.color = Color.White;

            //}

        }


    }
}
