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
    internal class Enemy : MoveableGameObject
    {
        private int health;
        private int maxHealth;

        private Vector2 test;

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

        public Texture2D coinSprite;
        public Texture2D visionSprite;
        public Vision vision;
        public Vector2 coinSpawnOffset;
        private Random rnd;

        public int res = World.GridResulution;

        private static Timer timerA;
        private static Timer timerB;
        private bool timerStart = false;

        public static bool playerInSight = false;
        public bool EnemyVision = false;
        public Vector2 lastVelocity;

        private float gravStrength = 0; // don't like the placement of this var :/

        private SpriteFont healthbarFont;
        private int healthbarLength = 6;

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

        private Color HealthbarColor
        {
            get
            {
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

        protected bool isGrounded //bad maybe?, we check too often i think, maybe not only when we try to apply gravity (once per cycle) and ocasionally when we jummp
        {
            get
            {
                var isGrounded = false;

                var downRec = HitBox.Copy();
                downRec.Location -= new Vector2(0, -5);

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

        public Enemy(int X, int Y)
        {
            DefaultEnemy();
            int res = World.GridResulution;
            Location = new Vector2(X * res, Y * res);
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

        private void SetTimerA()
        {
            // Create a timer with a two second interval.
            timerA = new System.Timers.Timer(1500);
            // Hook up the Elapsed event for the timer.
            timerA.Elapsed += OnTimedEventA;
            timerA.AutoReset = false;
            timerA.Enabled = true;
        }

        private void SetTimerB()
        {
            // Create a timer with a two second interval.
            timerB = new System.Timers.Timer(1500);
            // Hook up the Elapsed event for the timer.
            timerB.Elapsed += OnTimedEventB;
            timerB.AutoReset = false;
            timerB.Enabled = true;
        }

        private void OnTimedEventA(Object source, ElapsedEventArgs e)
        {
            timerStart = true;
            velocity += -Vector2.UnitX;

            SetTimerB();
        }

        private void OnTimedEventB(Object source, ElapsedEventArgs e)
        {
            velocity += Vector2.UnitX;
            SetTimerA();
        }

        public void EnemyLogic()
        {
            if (playerInSight == false)

            {
                if (!timerStart)
                {
                    SetTimerA();
                }
            }
            else if (playerInSight)
            {
                timerA.Enabled = false;
                timerA.AutoReset = false;
                timerB.Enabled = false;
                timerB.AutoReset = false;

                Attack();
                // MoveTo(World.Player.Location);

                //TODO Update position af vision til at følge enemy
            }
        }

        //public void Draw(SpriteBatch spriteBatch)
        //{
        //    spriteBatch.Draw(Sprite, Location, Color.White);

        //}
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(healthbarFont, HealthBar, new Vector2(Location.X + (Size.X / 2), Location.Y - healthbarFont.LineSpacing), HealthbarColor, 0, new Vector2((healthbarFont.MeasureString(HealthBar).X / 2), 0), 1, SpriteEffects.None, 0);
        }

        public override void Update()
        {
            if (isAlive == false)
            {
                //Die();
                Respawn();
            }

            //Debug.WriteLine("last Velocity is" + lastVelocity);

            //Debug.WriteLine("Velocity is" + velocity);

            // Debug.WriteLine(playerInSight);

            CreateVision();
            ApplyGravity();
            EnemyLogic();

            dir = UpdateSprite();
            base.Update();
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

        public void Attack()
        {
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
        }

        public void Die()
        {
            Coins();
            Destroy(this);

            //Spawn(new Enemy(9, 3));
        }

        public void Respawn()
        {
            Coins();
            health = maxHealth;
            playerInSight = false;
            Location = new Vector2(9 * res, 3 * res);
        }

        /// <summary>
        /// Spawns x-y coins
        /// </summary>
        public void Coins()
        {
            for (int i = rnd.Next(3, 7); i > 0; i--)
            {
                Spawn(new Coin(coinSprite, Location + coinSpawnOffset, new Vector2(rnd.Next(-5, 5), rnd.Next(-5, 5))));
            }
        }

        public void CreateVision()
        {
            if (!EnemyVision)
            {
                vision = new Vision(visionSprite, Location, 25 * 10, 50);

                Program.AdventureMan.CurrentWorld.newGameObjects.Add(vision);

                EnemyVision = true;
            }
        }

        public override void OnCollision(GameObject other)
        {
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