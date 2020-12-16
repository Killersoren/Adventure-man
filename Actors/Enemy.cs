using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Timers;

namespace Adventure_man
{
    public class Enemy : Character
    {
        private int health;
        private int maxHealth;
        private Vector2 spawnLocation;
        private Weapon weapon;

        private readonly string startingWeapon;

        private bool IsAlive
        {
            get => health > 0;
        }

        internal Weapon EnemyWeapon { get => weapon; private set => weapon = value; }

        public Texture2D coinSprite;
        public Texture2D visionSprite;
        private Vision vision;
        public Vector2 coinSpawnOffset;
        private Random rnd;

        public int res = World.GridResulution;

        private Timer timerLeft;
        private Timer timerRight;
        private bool timerStart = false;

        public bool playerInSight = false;
        public bool EnemyVision = false;
        public Vector2 lastVelocity;

        private SpriteFont healthbarFont;
        private readonly int healthbarLength = 6;

        /// <summary>
        /// Sofie- Uses the current health, the max health and the number of segments in the healthbar to create a string to visualise the Enemys current health
        /// </summary>
        private string HealthBar
        {
            get
            {
                //  Debug.WriteLine($"{healthbarFont.MeasureString("█").X} {healthbarFont.MeasureString(" ").X} {healthbarFont.Spacing}");

                StringBuilder temp = new StringBuilder("", healthbarLength);
                double space = maxHealth / healthbarLength;
                int full = (int)Math.Floor(health / space);
                int empty = healthbarLength - full;

                for (int i = full; i > 0; i--)
                {
                    temp.Append("█");
                }
                for (int i = empty; i > 0; i--)
                {
                    temp.Append("   ");
                }
                return Convert.ToString(temp);
            }
        }

        /// <summary>
        /// Sofie- Uses the health and Maxhealth to determine the color of the enemy healthbar
        /// </summary>
        private Color HealthbarColor//Sofie
        {
            get
            {
                //if (playerInSight == true)
                //    return Color.Blue;

                // Debug.WriteLine($"{health}/{(maxHealth * ((float)2 / 3))}");
                if ((health <= maxHealth) && (health > (maxHealth * ((float)2 / 3))))
                    return Color.Green;
                else if ((health <= (maxHealth * ((float)2 / 3))) && (health > (maxHealth * ((float)1 / 3))))
                    return Color.Yellow;
                else if ((health <= (maxHealth * ((float)1 / 3))) && (health >= 0))
                    return Color.Red;
                else
                    return Color.White;
            }
        }

        private void DefaultEnemy()
        {
            dragCoefficient = 0.9f;
            speed = 0.2f;

            maxHealth = 200;
            health = maxHealth;
            rnd = new Random();
            staticDir = GameWorld.Direction.Right;
        }

        public Enemy()
        {
            DefaultEnemy();
        }

        public Enemy(float X, float Y)
        {
            DefaultEnemy();
            int res = World.GridResulution;
            spawnLocation = new Vector2(X * res, Y * res);
            Location = spawnLocation;
        }


        /// <summary>
        /// Søren - Spawns a enemy with a position using x and y coordinates and a starting weapon
        /// </summary>
        /// <param name="X">The x coordinate of the enemy on the map</param>
        /// <param name="Y">The y coordinate of the enemy on the map</param>
        /// <param name="startingWeapon">The weapon the enemy spawns with</param>
        public Enemy(float X, float Y, string startingWeapon)
        {
            DefaultEnemy();
            int res = World.GridResulution;
            spawnLocation = new Vector2(X * res, Y * res);
            Location = spawnLocation;

            this.startingWeapon = startingWeapon;

            if (this.startingWeapon == "Bow")
            {
                SpawnBow();
            }
            else if (this.startingWeapon == "Sword")
            {
                SpawnSword();
            }
        }

        //public override void LoadContent(ContentManager content)
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

            coinSpawnOffset = new Vector2(Size.X / 2, Size.Y / 4);

            coinSprite = Program.AdventureMan.content.Load<Texture2D>("Coin");
            visionSprite = Program.AdventureMan.content.Load<Texture2D>("VisionTexture");
            healthbarFont = Program.AdventureMan.altFont;
        }


        /// <summary>
        /// Søren - Sets the timer for when the enemy will move left.
        /// </summary>
        private void SetTimerLeft()
        {
            timerLeft = new Timer(2500);
            timerLeft.Elapsed += OnTimedEventLeft;
            timerLeft.AutoReset = false;
            timerLeft.Enabled = true;
        }
        /// <summary>
        /// Søren - Sets the timer for when the enemy will move Right.
        /// </summary>
        private void SetTimerRight()
        {
            timerRight = new Timer(2500);
            timerRight.Elapsed += OnTimedEventRight;
            timerRight.AutoReset = false;
            timerRight.Enabled = true;
        }

        /// <summary>
        /// Søren - The enemy moves to the left and starts the timer to move right.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnTimedEventLeft(Object source, ElapsedEventArgs e)
        {
            velocity += -Vector2.UnitX;
            SetTimerRight();
        }
        /// <summary>
        /// Søren - The enemy moves to the Right and starts the timer to move left.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnTimedEventRight(Object source, ElapsedEventArgs e)
        {
            velocity += Vector2.UnitX;
            SetTimerLeft();
            timerStart = true;
        }


        /// <summary>
        /// Søren - The enemy logic, as to how the enemy moves and attacks.
        /// </summary>
        public void EnemyLogic()
        {
            if (playerInSight == false)

            {

                if (!timerStart)
                {
                    velocity += -Vector2.UnitX;
                    SetTimerRight();

                }
            }
            else if (playerInSight)
            {
                if (timerStart)
                {
                    timerLeft.Stop();
                    timerRight.Stop();
                }

                Attack();
            }
        }

        //public void Draw(SpriteBatch spriteBatch)
        //{
        //    spriteBatch.Draw(Sprite, Location, Color.White);

        //}
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            // Sofie- Draws the enemies healthbar directly above its head.
            spriteBatch.DrawString(healthbarFont, HealthBar, Program.AdventureMan.CurrentWorld.Camera.WorldToScreen(new Vector2(Location.X + (Size.X / 2), Location.Y - healthbarFont.LineSpacing)), HealthbarColor, 0, new Vector2((healthbarFont.MeasureString(HealthBar).X / 2), 0), 1, SpriteEffects.None, 0);
        }

        public override void Update()
        {
            Debug.WriteLine("the velocity is " + velocity);

            if (IsAlive == false)
            {
                Die();
                //Respawn();
            }

            CreateVision();
            ApplyGravity(0.1f);
            EnemyLogic();

            if (weapon != null)
                EnemyWeapon.WeaponCooldown();

            dir = UpdateSprite();
            base.Update();
        }

        /// <summary>
        /// Søren - When set to attack the player the enemy will check if it has a sword or a bow, and use that to determine how close to move to the player's location.
        /// </summary>
        public void Attack()
        {
          
            MoveTowardsPlayer();
            void MoveTowardsPlayer()
            {
                if (weapon is Bow)
                {
                    if (World.player.Location.X < Location.X && Location.X >= World.player.Location.X + 150)
                    {
                        velocity += -Vector2.UnitX;
                    }
                    else if (World.player.Location.X > Location.X && Location.X <= World.player.Location.X - 150)
                    {
                        velocity += Vector2.UnitX;
                    }

                    if (EnemyWeapon != null)
                    {
                        EnemyWeapon.UseWeapon(Location, dir, this);
                    }
                }
                else if (weapon is Sword)
                {
                    if (World.player.Location.X < Location.X && Location.X >= World.player.Location.X + 50)
                    {
                        velocity += -Vector2.UnitX;
                    }
                    else if (World.player.Location.X > Location.X && Location.X <= World.player.Location.X - 50)
                    {
                        velocity += Vector2.UnitX;
                    }
                }

                if (EnemyWeapon != null)
                {
                    EnemyWeapon.UseWeapon(Location, dir, this);
                }
            }

            if (World.player.Location.X < Location.X)
            {
                dir = GameWorld.Direction.Left;
            }
            else if (World.player.Location.X > Location.X)
            {
                dir = GameWorld.Direction.Right;
            }
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
        }

        /// <summary>
        /// Sofie + Søren- Compleatly destroys the Enemy
        /// </summary>
        public void Die()
        {
            Coins();
            Destroy(this);
            Destroy(vision);
        }

        /// <summary>
        /// Sofie- Resets the enemy
        /// </summary>
        public void Respawn()
        {
            Coins();
            health = maxHealth;
            playerInSight = false;
            timerStart = false;
            Location = spawnLocation;
        }

        /// <summary>
        /// Sofie- Spawns 3-6 coins
        /// </summary>
        public void Coins()
        {
            for (int i = rnd.Next(3, 7); i > 0; i--)
            {
                Spawn(new Coin(coinSprite, Location + coinSpawnOffset, new Vector2(rnd.Next(-5, 5), rnd.Next(-5, 5))));
            }
        }


        /// <summary>
        /// Søren - Creates the vision for the enemy.
        /// </summary>
        public void CreateVision()
        {
            if (!EnemyVision)
            {
                vision = new Vision(visionSprite, Location, 250, 50, this);
                

                Program.AdventureMan.CurrentWorld.newGameObjects.Add(vision);

                EnemyVision = true;
            }
        }
        /// <summary>
        /// Søren - sets the enemy's weapon to a bow
        /// </summary>
        public void SpawnBow()
        {
            weapon = new Bow(30, 10, 2);
        }
        /// <summary>
        /// Søren - sets the enemy's weapon to a sword
        /// </summary>
        public void SpawnSword()
        {
            weapon = new Sword(40, 2);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                playerInSight = true;
            }

            base.OnCollision(other);
            //    if (other is Platform)
            //    {
            //        if (other.HitBox.Center.Y > HitBox.Center.Y) // If Player is on top
            //        {
            //            //color = Color.Red;
            //            Velocity.Y = 0;
            //            //velocity.Normalize();
            //        }
            //        if (other.HitBox.Bottom <= CollisionBox.Bottom && other.CollisionBox.Center.X < CollisionBox.Center.X) // if the player is to the Right of the platform
            //        {
            //            velocity += new Vector2(1, 0);
            //        }
            //        if (other.CollisionBox.Bottom <= CollisionBox.Bottom && other.CollisionBox.Center.X > CollisionBox.Center.X)// if the player is to the Left of the platform
            //        {
            //            velocity += new Vector2(-1, 0);
            //        }
            //    }
            //}
        }
    }
}