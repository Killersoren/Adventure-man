using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using static Adventure_man.GameWorld;

namespace Adventure_man
{
    public class Player : Character
    {
        public int points = 0;
        private Weapon currentWeapon;

        private int availableJumps;

        public int JumpAmount;
        public new Direction dir;
        private SoundEffect coinPickup;

        public bool crouched = false;

        public int health;
        private int maxHealth;
        private float invincibilityTime;
        private float invincibilityTimer;
        private bool isInvincible;

        public Vector2 spawn;

        private bool isAlive
        {
            get => health > 0;
        }

        private List<Weapon> weapons;

        internal Weapon CurrentWeapon { get => currentWeapon; private set => currentWeapon = value; }

        /// <summary>
        /// Sofie- Deafult player, so that we dont have to change the same thing in multiple constructors
        /// </summary>
        private void DefaultPlayer()
        {
            maxHealth = 200;
            health = maxHealth;
            JumpAmount = 1;
            dragCoefficient = 0.9f;
            speed = 1f;
            staticDir = Direction.Right;
            invincibilityTime = 1000;

            weapons = new List<Weapon>();
        }

        public Player()
        {
            DefaultPlayer();
        }

        public Player(int X, int Y)
        {
            DefaultPlayer();

            int res = World.GridResulution;
            Location = new Vector2(X * res, Y * res);
            spawn = Location;
        }

        /// <summary>
        /// Ras - If the player has a weapon change it to either 0/1(sword / bow), not a bool incase we had more weapons
        /// (Sofie)
        /// </summary>
        private void SwapWeapon()
        {
            int i = weapons.IndexOf(currentWeapon);
            if (i + 1 < weapons.Count && weapons.Count > 0)
            {
                currentWeapon = weapons[i + 1];
            }
            else if (weapons.Count > 0)
            {
                currentWeapon = weapons[0];
            }
        }

        /// <summary>
        /// Sofie- For picking up a new weapon
        /// </summary>
        /// <param name="weapon">The weapon to be picked up</param>
        public void PickupWeapon(Weapon weapon)
        {
            weapons.Add(weapon);
            if (weapons.Count == 1)
                currentWeapon = weapons[0];
        }

        public override void Update()
        {
            isGrounded = CheckIfGrounded();
            ApplyGravity(0.1f);

            if (isAlive == false)// Checks if the player is alive
            {
                Respawn();
            }

            if (isGrounded)// Refreshes the jumpcounter and removes gravity if the player is grounded
            {
                //resets jumps
                availableJumps = JumpAmount;

                //Reset gravity
                ResetGravity();
            }
            if (isInvincible)
                InvincibilityCountdown();

            if (currentWeapon != null) // Runs Weapon cooldown ,only if the player has a weapon.
                CurrentWeapon.WeaponCooldown();

            HandleInput();
            dir = UpdateSprite();
            base.Update();
        }

        /// <summary>
        /// Magnus - Resets the gravity
        /// </summary>
        private void ResetGravity()
        {
            if (velocity.Y > 0)
                velocity.Y = 0;

            gravStrength = 0;
        }

        /// <summary>
        /// Sofie- Makes the player crouch by halfing the height of the Hitbox and moving it down by the new height so that it wont be crouching in midair
        /// </summary>
        private void Crouch()
        {
            Size = new Vector2(Size.X, Size.Y / 2);
            Location += new Vector2(0, Size.Y); // else player will end up in the Air
            crouched = true;
        }

        /// <summary>
        /// Sofie- To make the Payer uncrouch,by "redoubelling" the hitbox height and moving the location up by the former height to prevent the player from being stuck in the ground, it also checks if there is anything "directly" above it and wont stand up (do as descriped before) until it wont hit its head.
        /// </summary>
        private void StandUp()
        {
            bool clear = true;
            var target = HitBox.Copy();

            target.Location = Location - new Vector2(0, Size.Y);

            foreach (GameObject o in Program.AdventureMan.CurrentWorld.Objects) // Makes it so that you dont get your head stuck in the cealing
            {
                if (target.Intersects(o.HitBox))
                {
                    if (o is Platform && o.Location.Y < target.Location.Y)
                    {
                        Debug.WriteLine("Cant Uncrouch");
                        clear = false;

                        //The Bellow code is for if you want the Jumper to uncrouch in the air and get pushed down by platforms above,
                        //if not in a grid this will have problems though

                        //if (isGrounded)
                        //{
                        //    Debug.WriteLine("Cant Uncrouch");
                        //    clear = false;
                        //}
                        //else
                        //{
                        //    while(target.Intersects(o.HitBox))
                        //    {
                        //        target.Location += new Vector2(0, 1);
                        //    }
                        //    clear = true;
                        //}
                    }
                }
            }
            if (clear)
            {
                Size = new Vector2(Size.X, Size.Y * 2);
                Location = target.Location; // else player will end up in the ground
                crouched = false;
            }
        }

        /// <summary>
        /// Magnus - Makes the character jump
        /// </summary>
        private void Jump()
        {
            if (availableJumps-- > 0)
            {
                velocity.Y = -30;

                ResetGravity();
            }
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
                //dir = Direction.Right;
            }
            else if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Left))
            {
                velocity += -Vector2.UnitX;
                //dir = Direction.Left;
            }
            else
            {
                Sprite.Restart();
            }
            if ((keyState.IsKeyDown(Keys.W) && lastState.IsKeyUp(Keys.W)) || (keyState.IsKeyDown(Keys.Up) && lastState.IsKeyUp(Keys.Up)))
            {
                Jump();
            }
            if (((keyState.IsKeyDown(Keys.S)) || (keyState.IsKeyDown(Keys.Down))) && crouched == false)
            {
                Crouch();
            }
            if ((keyState.IsKeyUp(Keys.S) && keyState.IsKeyUp(Keys.Down)) && crouched == true)
            {
                StandUp();
            }
            if (keyState.IsKeyDown(Keys.E))
            {
                Attack();
            }
            if (keyState.IsKeyDown(Keys.Q) && lastState.IsKeyUp(Keys.Q))
            {
                SwapWeapon();
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

            //currentWeaponSprite = Program.AdventureMan.content.Load<Texture2D>("Sword");

            Sprite = new SpriteAnimation(sprites);
            //HitBox = new RectangleF((int)Location.X, (int)Location.Y, Sprite.Width, Sprite.Height);
            Size = new Vector2(Sprite.Width - 1, Sprite.Height - 1);

            coinPickup = Program.AdventureMan.content.Load<SoundEffect>("CoinSound");
        }

        /// <summary>
        /// Sofie- Attack using Currrent Weapon, if the Player has any.
        /// </summary>
        public void Attack()
        {
            if (CurrentWeapon != null)//Jeg troede jeg allerede havde lagt dette ind men det m� jeg alligevel have glemt
                CurrentWeapon.UseWeapon(Location, dir, this);// Need some kind of facing system
        }

        /// <summary>
        /// Sofie- Take damage
        /// </summary>
        /// <param name="damage">The amount of damage to be taken</param>
        public void TakeDamage(int damage)
        {
            if (!isInvincible)
            {
                health -= damage;
                StartInvincibilityTimer();
            }
        }

        /// <summary>
        /// Sofie- Removes the Player object
        /// </summary>
        public void Die()
        {
            Destroy(this);

            //Spawn(new Enemy(9, 3));
        }

        /// <summary>
        /// Sofie- Puts Player back to starting location with max health, and makes it no longer invinviblt
        /// </summary>
        public void Respawn()
        {
            health = maxHealth;
            Location = spawn;
            isInvincible = false;
        }

        public override void OnCollision(GameObject collisionTarget)
        {
            if (collisionTarget is Coin coin)//For coin Pickup
            {
                coinPickup.Play(0.6f, 0, 0);
                points += coin.coinValue;
                Destroy(collisionTarget);
            }

            base.OnCollision(collisionTarget);
        }

        /// <summary>
        /// FOr setting spawnpoint
        /// </summary>
        /// <param name="location"></param>
        public void SetSpawn(Vector2 location)
        {
            spawn = location;
            Location = spawn;
        }

        /// <summary>
        /// Sofie- Starts invincibility frames
        /// </summary>
        private void StartInvincibilityTimer()
        {
            isInvincible = true;
            invincibilityTimer = invincibilityTime;
        }

        /// <summary>
        /// Sofie- Counts down to when invinvibility ends, and flickers the player sprite in and out of existance
        /// </summary>
        private void InvincibilityCountdown()
        {
            if (invincibilityTimer > 0)
            {
                invincibilityTimer -= (float)Program.AdventureMan.gameTime.ElapsedGameTime.TotalMilliseconds;
                if (color == Color.White)
                    color = Color.Transparent;
                else if (color == Color.Transparent)
                    color = Color.White;
            }
            else
            {
                isInvincible = false;
                color = Color.White;
            }
        }
    }
}