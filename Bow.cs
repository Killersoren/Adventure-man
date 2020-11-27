using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static Adventure_man.GameWorld;

namespace Adventure_man
{
    internal class Bow : Weapon
    {
        private float force;
        private Arrow arrow;

        public Bow(string name, int damage, float force)
        {
            base.damage = damage;
            base.name = name;
            this.force = force;

            arrow = new Arrow();
            arrow.LoadContent(Program.AdventureMan.content);
        }

        public override void UseWeapon(Vector2 position, Direction direction)
        {
            int dir;
            switch (direction)
            {
                case Direction.Right:
                    dir = 1;
                    break;

                case Direction.Left:
                    dir = -1;
                    break;

                default:
                    dir = 1;
                    break;
            }
            Program.AdventureMan.CurrentWorld.newGameObjects.Add(arrow.Shoot(position, new Vector2(dir, 0), force));
        }
    }
}