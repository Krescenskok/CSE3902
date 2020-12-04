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
        private String Difficulty;
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
            //Higher Difficulty = enemies die slower, link dies faster

            //these are starting hp
            linkHPValues = new Dictionary<string, int>();
            linkHPValues.Add("Navice", 100); //5 hearts 
            linkHPValues.Add("Normal", 60); //3 hearts
            linkHPValues.Add("Tough", 40); //2 heart
            linkHPValues.Add("LinksNightmare", 20); //1 heart

            //these are max HP
            linkMAXHPValues = new Dictionary<string, int>();
            linkMAXHPValues.Add("Navice", 120); //max 6 hearts
            linkMAXHPValues.Add("Normal", 120); //6 hearts
            linkMAXHPValues.Add("Tough", 80); //4 heart
            linkMAXHPValues.Add("LinksNightmare", 40); //2 heart

            //these are HP MULTIPLIERS, multiplied to default HP
            enemyHPValues = new Dictionary<string, int>();
            enemyHPValues.Add("Navice", 1); // 1/2 hp but not in even numbers so math is done elsewhere
            enemyHPValues.Add("Normal", 1); //default
            enemyHPValues.Add("Tough", 2); //2x hp
            enemyHPValues.Add("LinksNightmare", 4); //4x hp

            //these are HP MULTIPLIERS, multiplied to default HP
            enemyDropRate = new Dictionary<string, int>();
            enemyDropRate.Add("Navice", 5);
            enemyDropRate.Add("Normal", 5); 
            enemyDropRate.Add("Tough", 10); //1/2 drop rate of items
            enemyDropRate.Add("LinksNightmare", 20); //1/4 drop rate of items

            gameOverMessages = new Dictionary<string, String>();
            gameOverMessages.Add("Navice", "Game Over"); 
            gameOverMessages.Add("Normal", "GAME OVER"); 
            gameOverMessages.Add("Tough", "YOU DIED"); 
            gameOverMessages.Add("LinksNightmare", "NICE TRY"); 

        }

        //set at game start
        public void SetDifficulty(Game1 game)
        {
            
            this.Difficulty = DifficultyToString(game.State.Difficulty);
        }

        public string DifficultyToString(IDifficulty.Level DifficultyLevel)
        {
            string DifficultyStr = "";

            if (DifficultyLevel == IDifficulty.Level.Navice)
            {
                DifficultyStr = "Navice";
            } else if (DifficultyLevel == IDifficulty.Level.Normal)
            {
                DifficultyStr = "Normal";
            } else if (DifficultyLevel == IDifficulty.Level.Tough)
            {
                DifficultyStr = "Tough";
            } else if (DifficultyLevel == IDifficulty.Level.LinksNightmare)
            {
                DifficultyStr = "LinksNightmare";
            }

            return DifficultyStr;
        }

        public string DetermineGameOverMessage()
        {
            return gameOverMessages[Difficulty];
        }
        public int DetermineDropChance()
        {
            return enemyDropRate[Difficulty];
        }

        //manually switching between difficulties
        public void RotateDifficulty(String direction)
        {
            if (direction == "Up")
            {
                if (this.Difficulty == "Navice")
                {
                    this.Difficulty = "Normal";

                } else if (this.Difficulty == "Normal")
                {
                    this.Difficulty = "Tough";
                } else if (this.Difficulty == "Tough")
                {
                    this.Difficulty = "LinksNightmare";
                }
            } else
            {
                if (this.Difficulty == "LinksNightmare")
                {
                    this.Difficulty = "Tough";

                }
                else if (this.Difficulty == "Tough")
                {
                    this.Difficulty = "Normal";
                }
                else if (this.Difficulty == "Normal")
                {
                    this.Difficulty = "Navice";
                }
            }
            DetermineLinkHP(this.player);
        }

        public int DetermineLinkMaxHP()
        {
            return linkMAXHPValues[Difficulty] / 20;
        }

        public void DetermineLinkHP(LinkPlayer player)
        {
            //this happens at game init so we will allways have access to player after construction
            this.player = player;
            player.Health = linkHPValues[Difficulty];
            player.FullHealth = linkHPValues[Difficulty];
            //HUD.Instance.ChangeDifficulty((int)player.Health, (int)player.Health);
            HUD.Instance.UpdateHearts(player);
        }


        public int DetermineEnemyHP(int hp)
        {
            if (this.Difficulty == "Navice")
            {
                return (hp - (hp / 2));
            } else
            {
                return hp * enemyHPValues[Difficulty];
            }
        }
    }
}
