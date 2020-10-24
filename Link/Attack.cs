using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Link
{
    public class Attack : ILinkState
    {
        LinkPlayer link;

        public Attack(LinkPlayer link)
        {
            this.link = link;
            if(link.state is Movement)
            {
                
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 location)
        {
           
        }

        public void Movement()
        {
            link.state = new Movement(link);
        }

        public Vector2 Update(GameTime gameTime, Vector2 location)
        {
           
        }

   
    }
}
