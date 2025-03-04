﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.Enemies.Zol
{
    /// <summary>
    /// Author: JT Thrash
    /// </summary>
    public class ZolMoveSprite : EnemySprite
    {
        private Texture2D texture;
        private static int[] spriteSheetSize = EnemySpriteFactory.SheetSize();
        private int rows = spriteSheetSize[0];
        private int columns = spriteSheetSize[1];

        private int row = EnemySpriteFactory.GetRow("Zol");
        private int startColumn = EnemySpriteFactory.GetColumn("Zol");
        private int currentAnimatedFrame;
        private const int totalAnimatedFrames = 2;

        private int currentFrame;
        private int trueFrameCount;



        private const int frameRate = 7;
        private const int maxFrameRate = 60;

        private Vector2 spriteSize;



        public ZolMoveSprite(Texture2D texture)
        {
            currentAnimatedFrame = 0;
            currentFrame = 0;
            trueFrameCount = totalAnimatedFrames * (maxFrameRate / frameRate);

            this.texture = texture;

            spriteSize.X = texture.Width / columns;
            spriteSize.Y = texture.Height / rows;

        }

        public void Update()
        {
            currentFrame++;
            if (currentFrame == trueFrameCount)
            {
                currentFrame = 0;
            }
            currentAnimatedFrame = currentFrame / (maxFrameRate / frameRate);
            currentAnimatedFrame += startColumn;
        }

        public void Draw(SpriteBatch batch, Vector2 location, int curFrame, Color color)
        {
            int width = (int)spriteSize.X;
            int height = (int)spriteSize.Y;


            Rectangle sourceRectangle = new Rectangle(width * currentAnimatedFrame, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width * 2, height * 2);

            batch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);

        }

        public void Load(Game game)
        {
            texture = game.Content.Load<Texture2D>("EnemySpriteSheet");
        }
    }
}
