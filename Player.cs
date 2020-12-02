using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        public int points = 0;
        private float gravStrength = 0; // don't like the placement of this var :/
        private Weapon currentWeapon;
        private int availableJumps;
        public int JumpAmount;
        public Direction dir;
        private Sword sword;
        private Bow bow;
        private SoundEffect coinPickup;
        private Texture2D currentWeaponSprite;

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

        private Weapon[] weapons = new Weapon[2];

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

        internal Weapon CurrentWeapon { get => currentWeapon; private set => currentWeapon = value; }

        public Player()
        {
            health = 200;
            JumpAmount = 1;
            dragCoefficient = 0.9f;
            speed = 1f;
            CurrentWeapon = new Bow("Falcon Bow", 100, 10, 5, this);
            staticDir = Direction.Right;
            bow = new Bow("Falcon Bow", 100, 10, 5, this);
            sword = new Sword("Sword", 100, 10, 5, this);

            weapons = new Weapon[2] { sword, bow };

            CurrentWeapon = weapons[1];
        }

        private void SwapWeapon()
        {
            if (currentWeapon == weapons[0])
            {
                currentWeapon = weapons[1];
            }
            else if (currentWeapon == weapons[1])
            {
                currentWeapon = weapons[0];
            }
        }

        public override void Update()
        {
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
            if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Left))
            {
                velocity += -Vector2.UnitX;
                //dir = Direction.Left;
            }
            if ((keyState.IsKeyDown(Keys.W) && lastState.IsKeyUp(Keys.W)) || (keyState.IsKeyDown(Keys.Up) && lastState.IsKeyUp(Keys.Up)))
            {
                Jump();
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

            currentWeaponSprite = Program.AdventureMan.content.Load<Texture2D>("Sword");

            Sprite = new SpriteAnimation(sprites);
            //HitBox = new RectangleF((int)Location.X, (int)Location.Y, Sprite.Width, Sprite.Height);
            Size = new Vector2(Sprite.Width - 1, Sprite.Height - 1);

            coinPickup = Program.AdventureMan.content.Load<SoundEffect>("CoinSound");
        }

        public void Attack()
        {
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
            if (collisionTarget is Coin)
            {
                coinPickup.Play(0.6f, 0, 0);
                points += ((Coin)collisionTarget).coinValue;
                Destroy(collisionTarget);
            }

            base.OnCollision(collisionTarget);
        }

        //public override void Draw(SpriteBatch spriteBatch)
        //{
        //    spriteBatch.Draw(Sprite, HitBox, null, color, 0, Vector2.Zero, effect, 0);
        //}
    }
}