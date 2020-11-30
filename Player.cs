using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using static Adventure_man.GameWorld;

namespace Adventure_man
{
    public class Player : MoveableGameObject
    {
        public int Points = 0;
        private float gravStrength = 0; // don't like the placement of this var :/
        private Weapon currentWeapon;
        private int availableJumps;
        public int JumpAmount;
        public Direction dir;
        
        public int health;
        private bool isAlive
        {
            get
            {
                if (health > 0)
                    return true;
                else
                    return false;
            }
        }



        protected bool isGrounded //bad maybe?, we check too often i think, maybe not only when we try to apply gravity (once per cycle) and ocasionally when we jummp
        {
            get
            {
                var isGrounded = false;

                var downRec = HitBox.Copy();
                downRec.Location -= new Vector2(0, -1);

                foreach (GameObject gameObject in Program.AdventureMan.CurrentWorld.GameObjects)
                {
                    if (downRec.Intersects(gameObject.HitBox) && !isGrounded)
                    {
                        if (gameObject is Platform)
                        {
                            isGrounded = true;
                        }
                    }
                }
                return isGrounded;
            }
        }

        internal Weapon CurrentWeapon { get => currentWeapon;private set => currentWeapon = value; }

        public Player()
        {
            health = 200;
            JumpAmount = 1;
            dragCoefficient = 0.9f;
            speed = 1f;
            CurrentWeapon = new Bow("Falcon Bow", 100, 1,5,this);
        }

        public override void Update()
        {
            if (isAlive == false)
            {
                //Die();
                Respawn();
            }

            CurrentWeapon.WeaponCooldown();
            ApplyGravity();
            HandleInput();
            base.Update();
        }

        private void Jump()
        {
            if (isGrounded)
            {
                availableJumps = JumpAmount;
            }
            if (availableJumps-- > 0)
            {
                velocity.Y = -30;// += new Vector2(0, -30f); //should probably also reset the gravity or something like that for better feel, pretty sure other games also do this
            }
        }

        private void ApplyGravity()
        {
            if (isGrounded)
            {
                if (velocity.Y > 0)
                    velocity.Y = 0;

                gravStrength = 0;
                return;
            }
            gravStrength += 0.1f;
            velocity += new Vector2(0, gravStrength);
        }

        private KeyboardState keyState; //don't like this in this scope

        /// <summary>
        /// Handles Player input
        /// </summary>
        private void HandleInput()
        {
            KeyboardState lastState = keyState;
            keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.D) || keyState.IsKeyDown(Keys.Right))
            {
                velocity += Vector2.UnitX;
                dir = Direction.Right;
            }
            if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Left))
            {
                velocity += -Vector2.UnitX;
                dir = Direction.Left;
            }
            if ((keyState.IsKeyDown(Keys.W) && lastState.IsKeyUp(Keys.W)) || (keyState.IsKeyDown(Keys.Up) && lastState.IsKeyUp(Keys.Up)))
            {
                Jump();
            }
            if (keyState.IsKeyDown(Keys.E))
            {
                Attack();
            }
        }

        public override void LoadContent(ContentManager contentManager)
        {
            var sprites = new Texture2D[2];
            for (int i = 0; i < sprites.Length; i++)
            {
                //sprites[i] = content.Load<Texture2D>("MoveTest" + (i + 1)+"_v2");
                sprites[i] = Program.AdventureMan.content.Load<Texture2D>("MoveTest" + (i + 1) + "_v2");
            }

            Sprite = new SpriteAnimation(sprites);
            //HitBox = new RectangleF((int)Location.X, (int)Location.Y, Sprite.Width, Sprite.Height);
            Size = new Vector2(Sprite.Width - 1, Sprite.Height - 1);
        }

        public void Attack()
        {
            CurrentWeapon.UseWeapon(Location, GameWorld.Direction.Right);// Need some kind of facing system
        }
        public void TakeDamage(int damage)
        {
            health -= damage;
        }
        public void Die()
        {
            Destroy(this);

            //Spawn(new Enemy(9, 3));
        }
        public void Respawn()
        {
            health = 200;
            Location =Vector2.Zero;
        }
    }
}