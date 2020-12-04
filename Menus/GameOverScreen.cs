using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Sprint5.DifficultyHandling;

namespace Sprint5.Menus
{
    public class GameOverScreen
    {

        private static readonly GameOverScreen instance = new GameOverScreen();

        private static Vector2 gameOver = new Vector2 (260, 95);
        private static Vector2 playAgain = new Vector2(237, 145);
        private static Vector2 quit = new Vector2(250, 175);
        private String gameOverMessage;

        private static int statCount = 7;

        private  Vector2[] statsPosition = new Vector2[9];
        private int [] statsCounts = new int[9];
        private String[] statsMessages = new String[9];
         

        public int KillCount { get => statsCounts[0]; set => statsCounts[0] = value; }
        public int PickupCount { get => statsCounts[1]; set => statsCounts[1] = value; }
        public int ItemsConsumed { get => statsCounts[2]; set => statsCounts[2] = value; }
        public int ProjectileCount { get => statsCounts[3]; set => statsCounts[3] = value; }
        public int DistanceTravelled { get => statsCounts[4]; set => statsCounts[4] = value; }
        public int RoomsEntered { get => statsCounts[5]; set => statsCounts[5] = value; }
        public int DamageGiven { get => statsCounts[6]; set => statsCounts[6] = value; }
        public int DamageTaken { get => statsCounts[7]; set => statsCounts[7] = value; }
        public int AmountHealed { get => statsCounts[8]; set => statsCounts[8] = value; }



        public static GameOverScreen Instance
        {
            get
            {
                return instance;
            }
        }
        private GameOverScreen()
        {
        }

        public void Draw(SpriteBatch batch, Game1 game, SpriteFont font)
        {
            GenerateStats();
            gameOverMessage = DifficultyMultiplier.Instance.DetermineGameOverMessage();
            game.GraphicsDevice.Viewport = game.Camera.gameView;
            game.GraphicsDevice.Clear(Color.Black);
            batch.DrawString(font, gameOverMessage, gameOver, Color.White);
            batch.DrawString(font, "Press 'P' to Play Again", playAgain, Color.White);
            batch.DrawString(font, "Press 'Q' to Quit", quit, Color.White);

            batch.DrawString(font, "Stats", statsPosition[0], Color.White);
            for (int i = 1; i < statsPosition.Length; i++)
            {
                batch.DrawString(font, statsMessages[i], statsPosition[i], Color.White);
            }
        }

        private void GenerateStats()
        {
            statsPosition[0] = new Vector2(85, 10);
            for (int i = 1; i < 9; i++)
            {
                statsPosition[i] = new Vector2(10, statsPosition[i-1].Y + 25);
            }
            statsMessages[0] = "Enemies Killed: " + statsCounts[0];
            statsMessages[1] = "Items Picked Up: " + statsCounts[1];
            statsMessages[2] = "Items Consumed: " + statsCounts[2];
            statsMessages[3] = "Projectiles Fired: " + statsCounts[3];
            statsMessages[4] = "Distance Travelled: " + statsCounts[4] + " Pixels";
            statsMessages[5] = "Rooms Entered: " + statsCounts[5];
            statsMessages[6] = "Damage Given: " + statsCounts[6]/20 +" Hearts";
            statsMessages[7] = "Damage Taken: " + statsCounts[7]/20 +" Hearts";
            statsMessages[8] = "Amount Healed: " + statsCounts[8]/20 + " Hearts";


        }
    }
}
