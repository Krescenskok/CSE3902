using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Link
{
    public class Stationary: Movement
    {
        
        public Stationary(LinkPlayer link)
        {
            this.link = link;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime,Vector2 location)
        {
            if (linkSprite == null)
            {
                linkSprite = SpriteFactory.Instance.CreateLinkSprite();
            }

            linkSprite.Draw(spriteBatch,location, 0, Color.White);
        }

        public override Vector2 HandleMagicalRod(GameTime gameTime,Vector2 location)
        {
            throw new NotImplementedException();
        }

        public override Vector2 HandleShield(GameTime gameTime,Vector2 location)
        {
            throw new NotImplementedException();
        }

        public override Vector2 HandleSword(GameTime gameTime,Vector2 location)
        {
            throw new NotImplementedException();
        }

        public override Vector2 HandleWoodenSword(GameTime gameTime,Vector2 location)
        {
            throw new NotImplementedException();
        }

        public override Vector2 Update(GameTime gameTime,Vector2 location)
        {
            return location;
        }

        
    }
}
