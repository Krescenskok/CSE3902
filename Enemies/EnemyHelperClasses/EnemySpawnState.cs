using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class EnemySpawnState : IEnemyState
    {

        private IEnemy enemy;
        private SpawnSprite sprite;

        public EnemySpawnState(IEnemy enemy, Game game)
        {
          
            this.enemy = enemy;
            ISprite newSprite = EnemySpriteFactory.Instance.CreateSpawnSprite();
            this.enemy.SetSprite(newSprite);

            sprite = (SpawnSprite)newSprite;
        }

        public void ChangeDirection()
        {
            //do nothing
        }

        public void MoveAwayFromCollision(Collision collision)
        {
          //do nothing
        }

        public void Update()
        {
            if (sprite.DoneSpawning()) enemy.Spawn();
        }

        public void Die()
        {
            //do nothing
        }

        public void TakeDamage(int amount)
        {
            //do nothing
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }

        public void Stun(bool b)
        {
            throw new NotImplementedException();
        }
    }
}
