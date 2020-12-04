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

        private Dictionary<DifficultyLevel, int> linkHPValues;
        private Dictionary<DifficultyLevel, int> linkMAXHPValues;
        private Dictionary<DifficultyLevel, int> enemyHPValues;
        private Dictionary<DifficultyLevel, int> enemyDropRate;
        private Dictionary<DifficultyLevel, String> gameOverMessages;
        public DifficultyLevel Difficulty { get; set; }
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
            linkHPValues = new Dictionary<DifficultyLevel, int>();
            linkHPValues.Add(DifficultyLevel.Navice, 100);
            linkHPValues.Add(DifficultyLevel.Normal, 60);
            linkHPValues.Add(DifficultyLevel.Tough, 40);
            linkHPValues.Add(DifficultyLevel.Nightmare, 20);

            linkMAXHPValues = new Dictionary<DifficultyLevel, int>();
            linkMAXHPValues.Add(DifficultyLevel.Navice, 120);
            linkMAXHPValues.Add(DifficultyLevel.Normal, 120);
            linkMAXHPValues.Add(DifficultyLevel.Tough, 80);
            linkMAXHPValues.Add(DifficultyLevel.Nightmare, 40);

            enemyHPValues = new Dictionary<DifficultyLevel, int>();
            enemyHPValues.Add(DifficultyLevel.Navice, 1);
            enemyHPValues.Add(DifficultyLevel.Normal, 1);
            enemyHPValues.Add(DifficultyLevel.Tough, 2);
            enemyHPValues.Add(DifficultyLevel.Nightmare, 4);

            enemyDropRate = new Dictionary<DifficultyLevel, int>();
            enemyDropRate.Add(DifficultyLevel.Navice, 5);
            enemyDropRate.Add(DifficultyLevel.Normal, 5); 
            enemyDropRate.Add(DifficultyLevel.Tough, 10);
            enemyDropRate.Add(DifficultyLevel.Nightmare, 20);

            gameOverMessages = new Dictionary<DifficultyLevel, String>();
            gameOverMessages.Add(DifficultyLevel.Navice, "Game Over"); 
            gameOverMessages.Add(DifficultyLevel.Normal, "GAME OVER"); 
            gameOverMessages.Add(DifficultyLevel.Tough, "YOU DIED"); 
            gameOverMessages.Add(DifficultyLevel.Nightmare, "NICE TRY"); 

        }
        public void SetDifficulty(Game1 game)
        {
            
            this.Difficulty = game.State.Difficulty;

            DetermineLinkHP(this.player);
        }

        public String DetermineGameOverMessage()
        {
            return gameOverMessages[Difficulty];
        }
        public int DetermineDropChance()
        {
            return enemyDropRate[Difficulty];
        }

        public void RotateDifficulty(String direction)
        {
            if (direction == "Up")
            {
                if (this.Difficulty == DifficultyLevel.Navice)
                {
                    this.Difficulty = DifficultyLevel.Normal;

                } else if (this.Difficulty == DifficultyLevel.Normal)
                {
                    this.Difficulty = DifficultyLevel.Tough;
                } else if (this.Difficulty == DifficultyLevel.Tough)
                {
                    this.Difficulty = DifficultyLevel.Nightmare;
                }
            } else
            {
                if (this.Difficulty == DifficultyLevel.Nightmare)
                {
                    this.Difficulty = DifficultyLevel.Tough;

                }
                else if (this.Difficulty == DifficultyLevel.Tough)
                {
                    this.Difficulty = DifficultyLevel.Normal;
                }
                else if (this.Difficulty == DifficultyLevel.Normal)
                {
                    this.Difficulty = DifficultyLevel.Navice;
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
           
            this.player = player;
            player.Health = linkHPValues[Difficulty];
            player.FullHealth = linkHPValues[Difficulty];
            HUD.Instance.UpdateHearts(player);
        }


        public int DetermineEnemyHP(int hp)
        {
            if (this.Difficulty == DifficultyLevel.Navice)
            {
                return (hp - (hp / 2));
            } else
            {
                return hp * enemyHPValues[Difficulty];
            }
        }
    }
}
