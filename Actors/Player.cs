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
    public class Player : MoveableGameObject
    {
        public int points = 0;
        private float gravStrength = 0; // don't like the placement of this var :/
        private Weapon currentWeapon;

        //private List<Weapon> weapons;
        private int availableJumps;

        public int JumpAmount;
        public Direction dir;
        private SoundEffect coinPickup;

        public bool crouched = false;

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

        //private Weapon[] weapons = new Weapon[2];

        private List<Weapon> weapons;

        protected bool isGrounded; //bad maybe?, we check too often i think, maybe not only when we try to apply gravity (once per cycle) and ocasionally when we jummp

        internal Weapon CurrentWeapon { get => currentWeapon; private set => currentWeapon = value; }

        private void DefaultPlayer()
        {
            health = 200;
            JumpAmount = 1;
            dragCoefficient = 0.9f;
            speed = 1f;
            staticDir = Direction.Right;

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
        }

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
        public void PickupWeapon(Weapon weapon)
        {
            weapons.Add(weapon);
            if (weapons.Count == 1)
                currentWeapon = weapons[0];
            else
            {
                currentWeapon = weapons[1];
            }
        }

        private bool CheckIfGrounded()
        {
            var isGrounded = false;

            var downRec = HitBox.Copy();
            downRec.Location -= new Vector2(0, -1);

            foreach (GameObject gameObject in Program.AdventureMan.CurrentWorld.GameObjects)
            {
                if (downRec.Intersects(gameObject.HitBox) && !isGrounded)
                {
                    if (gameObject is IntermidiateTemporaryClassForStoppingMovement)
                    {
                        isGrounded = true;
                    }
                }
            }
            return isGrounded;
        }

        public override void Update()
        {
            isGrounded = CheckIfGrounded();

            if (isAlive == false)
            {
                //Die();
                Respawn();
            }

            if (isGrounded)
            {
                //resets jumps
                availableJumps = JumpAmount;

                //Reset gravity
                if (velocity.Y > 0)
                    velocity.Y = 0;

                gravStrength = 0;
            }

            //Debug.WriteLine("The player location is "+Location);
            if (currentWeapon!=null)
                CurrentWeapon.WeaponCooldown();
            
            ApplyGravity();
            HandleInput();
            dir = UpdateSprite();
            base.Update();
        }

        private void Jump()
        {
            if (availableJumps-- > 0)
            {
                velocity.Y = -30;// += new Vector2(0, -30f); //should probably also reset the gravity or something like that for better feel, pretty sure other games also do this
            }
        }

        private void Crouch()
        {
            Size = new Vector2(Size.X, Size.Y / 2);
            Location += new Vector2(0, Size.Y); // else player will end up in the Air
            crouched = true;
        }

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

        private void ApplyGravity()
        {
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

        public void Attack()
        {
            if (CurrentWeapon!=null)//Jeg troede jeg allerede havde lagt dette ind men det m� jeg alligevel have glemt
                CurrentWeapon.UseWeapon(Location, dir);// Need some kind of facing system
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
            Location = Vector2.Zero;
        }

        public override void OnCollision(GameObject collisionTarget)
        {
            if (collisionTarget is Coin coin)
            {
                coinPickup.Play(0.6f, 0, 0);
                points += coin.coinValue;
                Destroy(collisionTarget);
            }

            base.OnCollision(collisionTarget);
        }

        //public override void Draw(SpriteBatch spriteBatch)
        //{
        //    spriteBatch.Draw(Sprite, HitBox, null, color, 0, Vector2.Zero, effect, 0);
        //}
        public void SetSpawn(Vector2 location)
        {
            Location = location;
        }
    }
    
}