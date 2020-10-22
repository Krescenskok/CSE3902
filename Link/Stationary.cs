using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Link
{
    public class Stationary : Movement
    {
        
        public Stationary(LinkPlayer link) : base(link)
        {

        }

        Color[] colors = { Color.Yellow, Color.Pink, Color.Green, Color.Gold, Color.Blue, Color.IndianRed, Color.Indigo, Color.Ivory };
        int i = 0;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime,Vector2 location)
        {
            if (link.IsDamaged)
            {

                if (link.DamageStartTime == 0)
                    link.DamageStartTime = gameTime.TotalGameTime.TotalMilliseconds;
                else if (gameTime.TotalGameTime.TotalMilliseconds - link.DamageStartTime < 1000)
                {
                    Color col = colors[i];
                    i++;
                    linkSprite.Draw(spriteBatch, location, 96, col);

                    if (i == colors.Length - 1)
                    {
                        i = 0;
                    }
                }
                else
                {
                    link.IsDamaged = false;
                }

            }
            else
            {
                if (linkSprite == null)
                {
                    linkSprite = SpriteFactory.Instance.CreateLinkSprite();
                }
                linkSprite.Draw(spriteBatch, location, 96, Color.White);
            }

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
