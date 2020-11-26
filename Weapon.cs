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

       /// <summary>
       /// Uses the current Weapon
       /// </summary>
       /// <param name="position">The position of the user</param>
       /// <param name="direction">The Direction the user is facing</param>
        public abstract void UseWeapon(Vector2 position,Direction direction);



    }
}
