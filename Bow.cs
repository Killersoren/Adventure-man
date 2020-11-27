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
        private float force;
        private Arrow arrow;
        private Texture2D arrowSprite;
        private Vector2 bowOffset;

        public Bow(string name, int damage, float force,float fireRate)
        {
            base.damage = damage;
            base.name = name;
            this.force = force;
            this.fireRate = fireRate;
            bowOffset = new Vector2(64, 32);
            arrowSprite = Program.AdventureMan.Content.Load<Texture2D>("Arrow");

            
            //arrow = new Arrow(damage,force);
            //arrow.LoadContent(Program.AdventureMan.content);
        }

        public override void UseWeapon(Vector2 position, Direction direction)
        {
            if (cooldown<=0)
            {
                int dir;
                dir = (int)direction;
                //Program.AdventureMan.CurrentWorld.newGameObjects.Add(arrow.Shoot(position, new Vector2(dir, 0)));
                GameObject.Spawn(new Arrow(arrowSprite, position + (bowOffset*new Vector2((int)World.Player.dir,1)),damage,force));
                cooldown = 1000 / fireRate;
            }
            
        }
    }
}