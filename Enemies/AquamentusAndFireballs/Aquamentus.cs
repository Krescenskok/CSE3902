using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    /// <summary>
    /// Author: Yuan Hong
    /// </summary>
    class Aquamentus : IEnemy
    {
        public IEnemyState state;
        private ISprite sprite;
        private int aquamentusHP;
        private Vector2 aquamentusPos;
        private int attackCountDown;
        private static int UpdatePerSec = 30;
        private float attackPerSec = 1;
        private List<FireBall> fireBallList;

        public Aquamentus(Vector2 initialPos)
        {
            aquamentusPos = initialPos;
            attackCountDown = (int)(UpdatePerSec / attackPerSec);
            aquamentusHP = 4;
            state = new AquamentusNormalState(this, initialPos);


            fireBallList = new List<FireBall>(); //added by JT
        } 

        public void SetSprite(ISprite sprite)
        {
            this.sprite = sprite;
        }

        public void TakeDamage()
        {
            state.TakeDamage();
        }

        public void LostHP()
        {
            aquamentusHP--;
        }

        public Boolean checkAlive()
        {
            return aquamentusHP > 0;
        }

        public void Die()
        {
            state.Die();
        }

        public Boolean TryAttack()
        {
            if (attackCountDown == 0)
            {
                attackCountDown = (int)(UpdatePerSec / attackPerSec);
                return true;
            }
            else
            {
                attackCountDown--;
                return false;
            }
        }

        public void SpawnFireBall(Vector2 spawnPos, Vector2 targetPos)
        {
            fireBallList.Add(new FireBall(spawnPos, targetPos));
        }

        public void RemoveFireBall(FireBall fb)
        {
            fireBallList.Remove(fb);
        }

        public void ChangePos(Vector2 newPos)
        {
            aquamentusPos = newPos;
        }

        public void Update()
        {
            state.Update();
            foreach (FireBall fb in fireBallList){
                fb.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, aquamentusPos, 0, Color.White);
            foreach(FireBall fb in fireBallList)
            {
                fb.Draw(spriteBatch);
            }
        }

    }
}
