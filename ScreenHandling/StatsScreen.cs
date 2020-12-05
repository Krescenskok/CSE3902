using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Sprint5.DifficultyHandling;
using Sprint5.ScreenHandling.ScreenSprites;
using Sprint5.ScreenHandling;

namespace Sprint5.Menus
{
    public class StatsScreen : IScreen
    {

        private static readonly StatsScreen instance = new StatsScreen();

        private static int statCount = 7;

        private  Vector2[] statsPosition = new Vector2[7];
        private int [] statsCounts = new int[7];
        private String[] statsMessages = new String[7];
        private StatsSprite sprite;

        private int drawBounds = 0;
         
        public int Time { get => statsCounts[0]; set => statsCounts[0] = value; }
        public int RoomsEntered { get => statsCounts[1]; set => statsCounts[1] = value; }
        public int DamageGiven { get => statsCounts[2]; set => statsCounts[2] = value; }
        public int DamageTaken { get => statsCounts[3]; set => statsCounts[3] = value; }
        public int KillCount { get => statsCounts[4]; set => statsCounts[4] = value; }
        public int Rupees { get => statsCounts[5]; set => statsCounts[5] = value; }
        public int ItemsConsumed { get => statsCounts[6]; set => statsCounts[6] = value; }



        public static StatsScreen Instance
        {
            get
            {
                return instance;
            }
        }

        public ScreenSprite Sprite { get; set; }
        private StatsScreen()
        {
            sprite = SpriteFactory.Instance.CreateStatsSprite();
        }

        public void Draw(Game1 game, GameTime gameTime)
        {

            if (game.ActiveCommand != null)
                game.ActiveCommand.ExecuteCommand(game, gameTime, game.Spritebatch);


            game.Spritebatch.Begin();


            game.GraphicsDevice.Viewport = game.Camera.EntireView;
            GenerateStats(gameTime);

            game.Spritebatch.Draw(sprite.Texture, new Rectangle(drawBounds, drawBounds, game.Camera.EntireArea.Width, game.Camera.EntireArea.Height), Color.White);
           

            for (int i = 1; i < statsPosition.Length; i++)
            {
                game.Spritebatch.DrawString(game.Font, statsMessages[i], statsPosition[i], Color.White);
            }
            game.Spritebatch.End();
        }

        private void GenerateStats(GameTime gameTime)
        {
            TimeSpan time = gameTime.TotalGameTime;


            statsPosition[0] = new Vector2(85, 10);
            for (int i = 1; i < 7; i++)
            {
                statsPosition[i] = new Vector2(10, statsPosition[i-1].Y + 25);
            }
            statsMessages[0] = " " + time.Hours + ":" + time.Minutes + ":" + time.Seconds + ":" + time.Milliseconds;
            statsMessages[1] = "" + statsCounts[1] + "/18";
            statsMessages[2] = "" + statsCounts[2] / 20 + " Hearts";
            statsMessages[3] = "" + statsCounts[3] / 20 + " Hearts";
            statsMessages[4] = "" + statsCounts[4]; 
            statsMessages[5] = "" + statsCounts[5];
            statsMessages[6] = "" + statsCounts[6];



        }
    }
}
