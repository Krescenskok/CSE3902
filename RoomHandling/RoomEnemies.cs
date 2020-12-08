using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


namespace Sprint5
{

    /// <summary>
    /// Author: JT Thrash
    /// <para>Singleton class for displaying and updating enemies in current room</para>
    /// </summary>
    public class RoomEnemies
    {
        private List<IEnemy> enemies;
        private List<EnemyDeath> deaths;

        private List<TestCollider> testObjects;
        private TestCollider linkTestCollider;

        private Game game;


        public bool allDead;

        public static RoomEnemies Instance { get; } = new RoomEnemies();

        private RoomEnemies()
        {
           
            enemies = new List<IEnemy>();
            testObjects = new List<TestCollider>();
            deaths = new List<EnemyDeath>();
            
        }

    
        public void LoadRoom(Game game, XElement room)
        {

            this.game = game;
            enemies = new List<IEnemy>();
            testObjects = new List<TestCollider>();

            List<XElement> items = room.Elements("Item").ToList();
            foreach (XElement item in items)
            {
                XElement typeTag = item.Element("ObjectType");
                string objType = typeTag.Value;

                if (objType.Equals("Enemy")) enemies.Add(EnemyFactory.Instance.CreateEnemy(game as Game1, item));
            }

            
            allDead = enemies.Count == 0;
            
        }


        public void Update()
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] != null)
                    enemies[i].Update();
            }
            for (int i = 0; i < deaths.Count; i++)
            {
                if (deaths[i] != null)
                    deaths[i].Update();
            }
        }

       

        public void Draw(SpriteBatch batch)
        {
            
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] != null)
                    enemies[i].Draw(batch);
            }

            for(int i = 0; i < deaths.Count; i++)
            {
                if (deaths[i] != null)
                    deaths[i].Draw(batch);
            }
        }

     

        public void Destroy(IEnemy enemy, Vector2 location)
        {
            StatsScreen.Instance.KillCount++;
            deaths.Add(new EnemyDeath(location));
            enemies.Remove(enemy);
            CollisionHandler.Instance.RemoveCollider(enemy.Colliders);
            Sounds.Instance.Play("EnemyDie");

            allDead = enemies.Count == 0;
        }



        public void Destroy(EnemyDeath death)
        {
            deaths.Remove(death);
            
        }

        public void StunAllEnemies()
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                enemies[i].State.Stun(true);
            }
        }


        #region for testing colliders
        public void RemoveTest(TestCollider col)
        {
            testObjects.Remove(col);
            CollisionHandler.Instance.RemoveCollider(col);
        }

        public void DrawTests(SpriteBatch batch)
        {
            foreach (TestCollider col in testObjects)
            {

                col.Draw(batch);
            }
            if (linkTestCollider != null) linkTestCollider.Draw(batch);
        }

        public void AddTestCollider(Rectangle rect, EnemyCollider enemyCol)
        {
            testObjects.Add(new TestCollider(rect, game, enemyCol));

        }

        public void AddTestCollider(PlayerCollider player, Game game)
        {
            linkTestCollider = new TestCollider(game, player);
        }

        public TestCollider AddTestCollider(Rectangle rect, ICollider col)
        {
            TestCollider t = new TestCollider(game, rect, col);
            testObjects.Add(t);
            return t;
        }

        #endregion
    }
}
    
