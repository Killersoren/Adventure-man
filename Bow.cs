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
        private int offseta;
        private int offsetb;

        public Bow(string name, int damage, float force,float fireRate,MoveableGameObject user)
        {
            base.user = user;
            base.damage = damage;
            base.name = name;
            this.force = force;
            this.fireRate = fireRate;
            
            arrowSprite = Program.AdventureMan.Content.Load<Texture2D>("Arrow");
            
            bowOffset = new Vector2(0, 32);

            //arrow = new Arrow(damage,force);
            //arrow.LoadContent(Program.AdventureMan.content);
        }

        public override void UseWeapon(Vector2 position, Direction direction)
        {
            if (cooldown<=0)
            {
                offseta = (World.Player.Sprite.Width + arrowSprite.Width) / 2;
                offsetb = offseta - arrowSprite.Width;
                int dir;
                dir = (int)direction;
                //Program.AdventureMan.CurrentWorld.newGameObjects.Add(arrow.Shoot(position, new Vector2(dir, 0)));
                GameObject.Spawn(new Arrow(arrowSprite, position+(bowOffset+new Vector2(offsetb+offseta*(int)World.Player.dir,0)),damage,force,user));
                cooldown = 1000 / fireRate;
            }
            
        }
    }
}