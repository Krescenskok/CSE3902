using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Sprint5;

namespace Sprint5.DifficultyHandling
{
    public class DifficultyMultiplier
    {
        private static readonly DifficultyMultiplier instance = new DifficultyMultiplier();

        private Dictionary<String, int> linkHPValues;
        private Dictionary<String, int> linkMAXHPValues;
        private Dictionary<String, int> enemyHPValues;
        private Dictionary<String, int> enemyDropRate;
        private Dictionary<String, String> gameOverMessages;
        private String difficulty;
        private LinkPlayer player;

        public static DifficultyMultiplier Instance
        {
            get
            {
                return instance;
            }
        }

        private DifficultyMultiplier()
        {

                        //i figure adjusting link's hp and the enemy's removes the need
            //for adjusting enemy & link damage
            //Higher difficulty = enemies die slower, link dies faster

            //these are starting hp
            linkHPValues = new Dictionary<string, int>();
            linkHPValues.Add("Navice", 100); //5 hearts 
            linkHPValues.Add("Normal", 60); //3 hearts
            linkHPValues.Add("Tough", 40); //2 heart
            linkHPValues.Add("Link's Nightmare", 20); //1 heart

            //these are max HP
            linkMAXHPValues = new Dictionary<string, int>();
            linkMAXHPValues.Add("Navice", 120); //max 6 hearts
            linkMAXHPValues.Add("Normal", 120); //6 hearts
            linkMAXHPValues.Add("Tough", 80); //4 heart
            linkMAXHPValues.Add("Link's Nightmare", 40); //2 heart

            //these are HP MULTIPLIERS, multiplied to default HP
            enemyHPValues = new Dictionary<string, int>();
            enemyHPValues.Add("Navice", 1); // 1/2 hp but not in even numbers so math is done elsewhere
            enemyHPValues.Add("Normal", 1); //default
            enemyHPValues.Add("Tough", 2); //2x hp
            enemyHPValues.Add("Link's Nightmare", 4); //4x hp

            //these are HP MULTIPLIERS, multiplied to default HP
            enemyDropRate = new Dictionary<string, int>();
            enemyDropRate.Add("Navice", 5);
            enemyDropRate.Add("Normal", 5); 
            enemyDropRate.Add("Tough", 10); //1/2 drop rate of items
            enemyDropRate.Add("Link's Nightmare", 20); //1/4 drop rate of items

            gameOverMessages = new Dictionary<string, String>();
            gameOverMessages.Add("Navice", "Game Over"); 
            gameOverMessages.Add("Normal", "GAME OVER"); 
            gameOverMessages.Add("Tough", "YOU DIED"); 
            gameOverMessages.Add("Link's Nightmare", "NICE TRY"); 

        }

        //set at game start
        public void SetDifficulty(Game1 game)
        {
            this.difficulty = game.Difficulty;
        }

        public string DetermineGameOverMessage()
        {
            return gameOverMessages[difficulty];
        }
        public int DetermineDropChance()
        {
            return enemyDropRate[difficulty];
        }

        //manually switching between difficulties
        public void RotateDifficulty(String direction)
        {
            if (direction == "Up")
            {
                if (this.difficulty == "Navice")
                {
                    this.difficulty = "Normal";

                } else if (this.difficulty == "Normal")
                {
                    this.difficulty = "Tough";
                } else if (this.difficulty == "Tough")
                {
                    this.difficulty = "Link's Nightmare";
                }
            } else
            {
                if (this.difficulty == "Link's Nightmare")
                {
                    this.difficulty = "Tough";

                }
                else if (this.difficulty == "Tough")
                {
                    this.difficulty = "Normal";
                }
                else if (this.difficulty == "Normal")
                {
                    this.difficulty = "Navice";
                }
            }
            DetermineLinkHP(this.player);
        }

        public int DetermineLinkMaxHP()
        {
            return linkMAXHPValues[difficulty] / 20;
        }

        public void DetermineLinkHP(LinkPlayer player)
        {
            //this happens at game init so we will allways have access to player after construction
            this.player = player;
            player.Health = linkHPValues[difficulty];
            player.FullHealth = linkHPValues[difficulty];
            //HUD.Instance.ChangeDifficulty((int)player.Health, (int)player.Health);
            HUD.Instance.UpdateHearts(player);
        }


        public int DetermineEnemyHP(int hp)
        {
            if (this.difficulty == "Navice")
            {
                return (hp - (hp / 2));
            } else
            {
                return hp * enemyHPValues[difficulty];
            }
        }
    }
}
