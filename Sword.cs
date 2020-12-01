using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    class Sword : Weapon
    {
        public Vector2 origin;

        public Sword(string name, int damage)
        {
            base.damage = damage;
            base.name = name;


        }


        public override void UseWeapon(Vector2 position, GameWorld.Direction direction)
        {
            //if (cooldown <= 0)
            //{
            //    int dir;
            //    dir = (int)direction;
            //    GameObject.Spawn(new Sword(, position + (new Vector2((int)World.Player.dir, 1)), damage));
            //    cooldown = 1000 / fireRate;
            //}
        }

    }
}
