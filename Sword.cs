using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
   internal class Sword : Weapon
    {
        private Texture2D swordSprite;
        private Vector2 swordOffset;
        private int offseta;
        private int offsetb;
        


        public Sword(string name, int damage, float force, float fireRate, MoveableGameObject user)
        {
            base.user = user;
            base.damage = damage;
            base.name = name;
            this.fireRate = fireRate;

            // Ras - flyttes til egen load metode eller til SwordAttack ?
            swordSprite = Program.AdventureMan.Content.Load<Texture2D>("Sword");

        }

        public Sword( int damage, float force, float fireRate, MoveableGameObject user)
        {
            base.user = user;
            base.damage = damage;
            this.fireRate = fireRate;

            // Ras - flyttes til egen load metode eller til SwordAttack ?
            swordSprite = Program.AdventureMan.Content.Load<Texture2D>("Sword");

        }



        public override void UseWeapon(Vector2 position, GameWorld.Direction direction)
        {
            //Ras - Edited version of arrow, no speed(force))
            // 
            if (cooldown <= 0)
            {
                swordOffset = new Vector2(0, World.Player.Size.Y / 4);
                //offseta = ((int)World.Player.Size.X + swordSprite.Width) / 2;
                //offsetb = offseta - swordSprite.Width;


                GameObject.Spawn(new SwordAttack(swordSprite, position + (swordOffset + new Vector2(offsetb + offseta * (int)World.Player.dir, 0)), damage, user, direction));
                
                cooldown = 1000 / fireRate;

            }
        }
    }


      

    
}
