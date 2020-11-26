using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    public abstract class MoveableGameObject : GameObject
    {
        public Vector2 Velocity;
        protected float dragCoefficient = 1f;
        protected float speed = 1f;

        private HashSet<GameObject> collisions = new HashSet<GameObject>();

        public override void Update()
        {
            if (Velocity.RoundTo(3) != Vector2.Zero)
            {
                Move((Velocity *= dragCoefficient) * speed);
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

            var nextTarget = HitBox.Copy();

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
                        Velocity.X = 0;
                        movingX = false;
                    }
                }

                if (movingY)
                {
                    if (!MoveTo(nextTarget.Location += stepY))
                    {
                        nextTarget.Location -= stepY;
                        targetPosition.Y = nextTarget.Location.Y;
                        Velocity.Y = 0;
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
            var target = HitBox.Copy();
            target.Location = position;
            foreach (GameObject gameObject in Program.AdventureMan.CurrentWorld.GameObjects)
            {
                if (target.Intersects(gameObject.HitBox))
                {
                    collisions.Add(gameObject);
                    if (gameObject is Platform)
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
    }
}