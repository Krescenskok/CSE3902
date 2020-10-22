using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    /// <summary>
    /// Author: JT Thrash
    /// </summary>
    public class StalfosSpawnState : IEnemyState
    {

        private Stalfos stalfos;

        private  Vector2 location;
       

        public StalfosSpawnState(Stalfos stally, Vector2 location)
        {
            stalfos = stally;

            this.location = location;

            stalfos.SetSprite(EnemySpriteFactory.Instance.CreateStalfosSpawnSprite(this));
        }

        public void Attack()
        {
            //do nothing
        }

        public void ChangeDirection()
        {
           //do nothing
        }

        public void Die()
        {
           //do nothing
        }

        public void TakeDamage()
        {
            //do nothing
        }

        public void FinishSpawning(Vector2 position)
        {
            stalfos.state = new StalfosWalkingState(stalfos, location);
            
            stalfos.UpdateLocation(position);
        }

        public void Update()
        {
            //do nothing
        }

        public Vector2 GetLocation()
        {
            return location;
        }

        public void MoveAwayFromCollision(Collision collision)
        {
            throw new NotImplementedException();
        }

        public void TakeDamage(int amount)
        {
            throw new NotImplementedException();
        }
    }
}
