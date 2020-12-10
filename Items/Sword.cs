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


        /// <summary>
        /// Ras - Swords constructor, Sets parameters to this
        /// </summary>
        /// <param name="name"></param>
        /// <param name="damage"></param>
        /// <param name="force"></param>
        /// <param name="fireRate"></param>
        /// <param name="user"></param>
        public Sword(string name, int damage, float force, float fireRate, MoveableGameObject user)
        {
            base.user = user;
            base.damage = damage;
            base.name = name;
            this.fireRate = fireRate;
            Loadcontent();
        }

        /// <summary>
        /// Ras - Sets swords sprite
        /// </summary>
        public void Loadcontent()
        {
            swordSprite = Program.AdventureMan.Content.Load<Texture2D>("Sword");
        }

        /// <summary>
        /// Ras - Swords constructor, Sets parameters to this (without name)
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="force"></param>
        /// <param name="fireRate"></param>
        /// <param name="user"></param>
        public Sword( int damage, float force, float fireRate, MoveableGameObject user)
        {
            base.user = user;
            base.damage = damage;
            this.fireRate = fireRate;
            Loadcontent();

        }


        /// <summary>
        ///  Ras - If cooldown is 0 or less, spawn a swordattack infront of the player with offset sat to player position + (dir)x=1/-1, y= player.size / 4
        ///  Resets cooldown with firerate modifier after
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        public override void UseWeapon(Vector2 position, GameWorld.Direction direction)
        {
            if (cooldown <= 0)
            {
                swordOffset = new Vector2((int)World.Player.dir, World.Player.Size.Y / 4);

                GameObject.Spawn(new SwordAttack(swordSprite, position + swordOffset, damage, user, direction));
                
                cooldown = 1000 / fireRate;

            }
        }



        public override void UseWeaponEnemy(Vector2 position, GameWorld.Direction direction)
        {
            if (cooldown <= 0)
            {
                swordOffset = new Vector2(0, World.Player.Size.Y / 4);
                GameObject.Spawn(new SwordAttackEnemy(swordSprite, position + (swordOffset + new Vector2( (int)World.Player.dir, 0)), damage, user, direction));

                cooldown = 1000 / fireRate;

            }
        }
    }


      

    
}
