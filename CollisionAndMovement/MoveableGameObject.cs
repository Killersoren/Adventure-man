using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using static Adventure_man.GameWorld;

namespace Adventure_man
{
    public abstract class MoveableGameObject : GameObject
    {
        public Vector2 velocity;
        protected float dragCoefficient = 1f;
        protected float groundDrag = 1f;
        protected float bounce = 0f; //0=no bouncyness   1=infinite bouncyness;
        protected float speed = 1f;
        public Direction dir;//=Direction.Right
        public Direction staticDir;

        private readonly HashSet<GameObject> collisions = new HashSet<GameObject>();

        public override void Update()
        {
            if (velocity.RoundTo(3) != Vector2.Zero)
            {
                Move((velocity *= dragCoefficient) * speed);
            }
            else
            {
                Sprite.Restart();
            }

            base.Update();
        }

        private void Move(Vector2 distance)
        {
            var targetPosition = (this.Location + distance);

            var stepSize = 0.5f;

            var step = distance;
            step.Normalize();
            step *= stepSize;

            var stepY = new Vector2(0, step.Y);
            var stepX = new Vector2(step.X, 0);

            bool movingX = stepX != Vector2.Zero;
            bool movingY = stepY != Vector2.Zero;

            var nextTarget = HitBox;

            while (movingX || movingY)
            {
                if (Vector2.Distance(nextTarget.Location, targetPosition) <= stepSize) //we're at the location (ca.)
                    break;

                if (movingX)
                {
                    if (!MoveTo(nextTarget.Location += stepX))
                    {
                        nextTarget.Location -= stepX;
                        targetPosition.X = nextTarget.Location.X;
                        velocity.X = 0;
                        movingX = false;
                    }
                }
                if (movingY)
                {
                    if (!MoveTo(nextTarget.Location += stepY))
                    {
                        nextTarget.Location -= stepY;
                        targetPosition.Y = nextTarget.Location.Y;
                        velocity.Y = 0;
                        movingY = false;
                    }
                }
            }

            ActivateCollisions(collisions);
            collisions.Clear();
        }

        public bool MoveTo(Vector2 position)
        {
            bool move = true;
            var target = HitBox;
            target.Location = position;
            foreach (GameObject gameObject in Program.AdventureMan.CurrentWorld.GameObjects)
            {
                if (target.Intersects(gameObject.HitBox))
                {
                    collisions.Add(gameObject);
                    if (gameObject.IsBlocking)
                    {
                        move = false;
                    }
                }
            }
            if (move)
            {
                this.Location = position;
            }
            return move;
        }

        private void ActivateCollisions(HashSet<GameObject> gameObjects)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                this.OnCollision(gameObject);

                gameObject.OnCollision(this);
            }
        }

        /// <summary>
        /// Sofie- Uses the Actors Current Velocity to decide the direction they should be facing.
        /// if the velicity is 0 it either makes the new direction the last direction, and if there is no last direction, like at the start of the game, it uses the sprites orientation to deside the direction.
        /// </summary>
        /// <returns>A direction -1: Left 1: Right</returns>
        private Direction CheckDirection()
        {
            Direction oldDir = dir;
            if (velocity.X > 0)
            {
                dir = Direction.Right;
            }
            else if (velocity.X < 0)
            {
                dir = Direction.Left;
            }
            else
            {
                if (oldDir != 0)
                    dir = oldDir;
                else
                {
                    switch (effect)
                    {
                        case SpriteEffects.None:
                            dir = Direction.Right;
                            break;

                        case SpriteEffects.FlipHorizontally:
                            dir = Direction.Left;
                            break;

                        default:
                            dir = Direction.Right;
                            break;
                    }
                }
            }

            return dir;
        }

        /// <summary>
        /// Sofie-Uses the current direction to flip a sprite
        /// </summary>
        public void FlipSprite()
        {
            switch (dir)
            {
                case Direction.Right:
                    effect = SpriteEffects.None;
                    break;

                case Direction.Left:
                    effect = SpriteEffects.FlipHorizontally;
                    break;
            }
        }

        /// <summary>
        /// Sofie-Both Check direction and flip sprite put together.
        ///
        /// </summary>
        /// <returns></returns>
        public Direction UpdateSprite()
        {
            Direction tempdir = CheckDirection();
            FlipSprite();
            return tempdir;
        }
    }
}