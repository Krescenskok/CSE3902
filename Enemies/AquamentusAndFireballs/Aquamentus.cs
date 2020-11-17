using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4.Link;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Sprint4
{
    /// <summary>
    /// Author: Yuan Hong
    /// </summary>
    class Aquamentus : IEnemy
    {
        public IEnemyState state;
        private ISprite sprite;
        private LinkPlayer link;
        private Vector2 aquamentusPos;
        private AquamentusNormalSprite aquamentusSprite;
        private XElement aquamentusInfo;
        private List<FireBall> fireBallList;
        private EnemyCollider aquamentusCollider;

        private int attackCountDown;
        private int ChangeDirectionCountDown;
        private int aquamentusHP;
        private static int AttackStrength = HPAmount.OneHeart;
        private static int RangeAttackStrength = HPAmount.OneHeart;
        private static int UpdatePerSec = 120;
        private float attackPerSec = 1;
        private float directionChangPerSec = (float)0.2;
        private float moveSpeedPerSec = 15;
        private float speed;
        private int directionIndex = -1;

        public Vector2 Location { get => aquamentusPos; }

        public IEnemyState State { get => state; }

        public List<ICollider> Colliders { get => new List<ICollider> { aquamentusCollider }; }
        

        public Aquamentus(Game game, Vector2 initialPos, XElement xml, LinkPlayer link)
        {
            this.link = link;
            aquamentusPos = initialPos;
            aquamentusInfo = xml;
            aquamentusHP = 40;
            attackCountDown = (int)(UpdatePerSec / attackPerSec);
            ChangeDirectionCountDown = (int)(UpdatePerSec / directionChangPerSec);
            state = new AquamentusNormalState(this, aquamentusPos, link);
            fireBallList = new List<FireBall>(); //added by JT
            aquamentusSprite = (AquamentusNormalSprite)sprite;
            aquamentusCollider = new EnemyCollider(aquamentusSprite.GetRectangle(aquamentusPos), this, AttackStrength);
            speed = moveSpeedPerSec / UpdatePerSec;
        }

        public void Spawn()
        {    
        }

        public void SetSprite(ISprite sprite)
        {
            this.sprite = sprite;
        }


        public void LostHP(int damage)
        {
            aquamentusHP -= damage;
        }

        public Boolean checkAlive()
        {
            return aquamentusHP > 0;
        }

        public void Die()
        {
            Sounds.Instance.PlayBossScream();
            CollisionHandler.Instance.RemoveCollider(aquamentusCollider);
            RoomEnemies.Instance.Destroy(this,Location);
            aquamentusInfo.SetElementValue("Alive", "false");
        }

        public Boolean TryAttack()
        {
            if (attackCountDown < 0)
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

        public Boolean TryChangeDirection()
        {
            if (ChangeDirectionCountDown == 0)
            {
                ChangeDirectionCountDown = (int)(UpdatePerSec / directionChangPerSec); ;
                return true;
            }
            else
            {
                ChangeDirectionCountDown--;
                return false;
            }
        }

        public void SpawnFireBall(Vector2 spawnPos, Vector2 targetPos)
        {
            fireBallList.Add(new FireBall(this, spawnPos, targetPos, RangeAttackStrength, link));
        }

        public void RemoveFireBall(FireBall fb)
        {
            fireBallList.Remove(fb);
        }

        public void Update()
        {
            if (TryChangeDirection())
            {
                state.ChangeDirection();
            }
            if (TryAttack())
            {
                state.Attack();
            }
            aquamentusPos.X += directionIndex * speed;
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

      

        public void TakeDamage(Direction dir, int amount)
        {
            state.TakeDamage(amount);
        }

        public void ObstacleCollision(Collision collision)
        {
            //do nothing
        }

        public void Stun()
        {
           //not affected
        }

    }
}
