using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Final.Enemies;
using Sprint2Final.EnemyAndNPC.Merchant;
using Sprint2Final.EnemyAndNPC.OldMan;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;

namespace Sprint2Final
{
    /// <summary>
    /// Author: JT Thrash
    /// <para>Singleton class for displaying Enemies and NPCs. Used in main Game class</para>
    /// </summary>
    public class EnemyNPCDisplay
    {

        private static readonly EnemyNPCDisplay instance = new EnemyNPCDisplay();

        List<IEnemy> enemies;
        IEnemy currentEnemy;

        private bool initialState;

        private Game game;
        private Vector2 location;
        private Vector2 trapTarget;
      
        public static EnemyNPCDisplay Instance
        {
            get
            {
                return instance;
            }
        }

        private EnemyNPCDisplay()
        {

        }

        /// <summary>
        /// Loads List of Enemies and NPCs to iterate through
        /// </summary>
        /// <param name="game"></param>
        /// <param name="pos"></param>
        /// <param name="pos2">target vector for blade trap</param>
        public void Load(Game game, Vector2 pos, Vector2 pos2)
        {
      

            currentEnemy = enemies[0];

            this.game = game;
            location = pos;
            trapTarget = pos2;

           
        }

       
        public void NextEnemy()
        {
            int index = enemies.IndexOf(currentEnemy);

            index = index == enemies.Count - 1 ? 0 : index + 1;

            currentEnemy =  enemies[index];

            initialState = false;
        }

        public void PreviousEnemy()
        {
            int index = enemies.IndexOf(currentEnemy);

            index = index == 0 ? enemies.Count - 1 : index - 1;

            currentEnemy = enemies[index];
            initialState = false;
        }

        public void Update()
        {
            currentEnemy.Update();
            
        }

        public void Draw(SpriteBatch batch, GameTime time)
        {
            currentEnemy.Draw(batch);
            
        }

        public void Reset()
        {
            if(!initialState) Load(game, location, trapTarget);

            initialState = true;

        }

    }
}
