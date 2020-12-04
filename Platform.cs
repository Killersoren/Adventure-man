using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure_man
{
    public class Platform : IntermidiateTemporaryClassForStoppingMovement
    {
        public Platform(float x, float y, float width, float height)
        {
            this.HitBox = new RectangleF(x, y, width, height);
        }
        public override void LoadContent(ContentManager contentManager)
        {
            Sprite = contentManager.Load<Texture2D>("PlatformTest");
        }
        public override void OnCollision(GameObject collisionTarget)
        {
            base.OnCollision(collisionTarget);
        }
    }
}