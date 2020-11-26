using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    class Sword : Weapon
    {
        public Sword(string name, int damage)
        {
            base.damage = damage;
            base.name = name;
        }

        public override void UseWeapon(Vector2 position, GameWorld.Direction direction)
        {
            throw new NotImplementedException();
        }
    }
}
