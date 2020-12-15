using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    /// <summary>
    /// Magnus - Class for extending the MoveableGameObject with commonly used features like Gravity and Grounding
    /// (Additional comment: name kinda bad, but guess who sucks at names...)
    /// </summary>
    public abstract class Character : MoveableGameObject
    {
        #region Gravity and grounding

        protected bool isGrounded;

        /// <summary>
        /// Magnus - Used for checking if the character is standing on a blocking gameobject like the ground or a platform
        /// </summary>
        /// <returns>true if a blocking gameobject is immediately below the character, otherwise returns false</returns>
        protected bool CheckIfGrounded()
        {
            var isGrounded = false;

            var downRec = HitBox;
            downRec.Location -= new Vector2(0, -0.5f);

            foreach (GameObject gameObject in Program.AdventureMan.CurrentWorld.GameObjects)
            {
                if (downRec.Intersects(gameObject.HitBox) && !isGrounded)
                {
                    if (gameObject.IsBlocking)
                    {
                        isGrounded = true;
                    }
                }
            }
            return isGrounded;
        }

        protected float gravStrength = 0;

        /// <summary>
        /// Magnus - Applies gravity to the character
        /// </summary>
        /// <param name="growth">The amount the gravity's strength increases with</param>
        protected void ApplyGravity(float growth)
        {
            gravStrength += growth;
            velocity += new Vector2(0, gravStrength);
        }

        #endregion Gravity and grounding
    }
}