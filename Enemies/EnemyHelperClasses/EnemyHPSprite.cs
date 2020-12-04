using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class EnemyHPSprite : ISprite
    {
        private Texture2D background;
        private Texture2D fillBar;

        private int fillAmount;
        private int maxAmount;

        private int width;
        private int adjustedWidth;
        private int height;

        
        
        public EnemyHPSprite(Game game, int width, int height)
        {
            this.height = height;
            this.width = width;
            adjustedWidth = width;
            background = EnemySpriteFactory.Instance.GetHPBackground();
            fillBar = EnemySpriteFactory.Instance.GetHPFill();



            maxAmount = fillBar.Width;
            fillAmount = fillBar.Width;
        }

        public EnemyHPSprite()
        {

        }

        public void Draw(SpriteBatch batch, Vector2 location, int currentFrame, Color color)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, fillAmount, fillBar.Height);
            Rectangle backgroundRect = new Rectangle(0, 0, background.Width, background.Height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, adjustedWidth, height);
            Rectangle backgroundDest = new Rectangle((int)location.X, (int)location.Y, width, height);

            batch.Draw(background, backgroundDest, backgroundRect, Color.White);
            batch.Draw(fillBar, destinationRectangle, sourceRectangle, Color.Red);
            
        }

        public void Update(float fill)
        {
            fillAmount = (int)(fill * maxAmount);
            adjustedWidth = (int)(fill * width);
        }
    }
}
