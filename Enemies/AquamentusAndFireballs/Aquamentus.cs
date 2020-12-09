using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Link;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Sprint5.DifficultyHandling;

namespace Sprint5
{
    /// <summary>
    /// Author: Yuan Hong
    /// </summary>
    class Aquamentus : IEnemy
    {
        public IEnemyState state;
        private EnemySprite sprite;
        private LinkPlayer link;
        private Vector2 aquamentusPos;
        private AquamentusNormalSprite aquamentusSprite;
        private XElement aquamentusInfo;
        private List<FireBall> fireBallList;
        private EnemyCollider aquamentusCollider;

        private int attackCountDown;
        private int ChangeDirectionCountDown;
        public int HP { get; private set; } = HPAmount.EnemyBoss1;
     

        private static int AttackStrength = HPAmount.Full_Heart;
        private static int RangeAttackStrength = HPAmount.Full_Heart;

        private static int UpdatePerSec = 120;
        private float attackPerSec = (float)0.5;

        private float directionChangPerSec = (float)0.4;
        private float moveSpeedPerSec = 15;
        private float speed;
        private int directionIndex = -1;

        public Vector2 Location { get => aquamentusPos; }

        public IEnemyState State { get => state; }

        public List<ICollider> Colliders { get => new List<ICollider> { aquamentusCollider }; }



        public Aquamentus(Vector2 initialPos, XElement xml, LinkPlayer link)
        {
            this.link = link;
            aquamentusPos = initialPos;
            aquamentusInfo = xml;
            attackCountDown = (int)(UpdatePerSec / attackPerSec);
            ChangeDirectionCountDown = (int)(UpdatePerSec / directionChangPerSec);
            speed = moveSpeedPerSec / UpdatePerSec;
            state = new AquamentusNormalState(this, aquamentusPos, link, directionIndex, speed);
            fireBallList = new List<FireBall>();
            aquamentusSprite = (AquamentusNormalSprite)sprite;
            aquamentusCollider = new EnemyCollider(aquamentusSprite.GetRectangle(aquamentusPos), this, AttackStrength);
            
            HP = DifficultyMultiplier.Instance.DetermineEnemyHP(HP);

            HPBarDrawer.AddBar(new BossHealthBar(this));
        }

        public void Spawn()
        {
        }

        public void SetSprite(EnemySprite sprite)
        {
            this.sprite = sprite;
        }

        public void UpdatePos(Vector2 pos)
        {
            aquamentusPos = pos;
        }

        public void LostHP(int damage)
        {
            HP -= damage;
        }

        public Boolean CheckAlive()
        {
            return HP > 0;
        }

        public void Die()
        {
            RoomItems.Instance.DropHeartContainer(Location);
            CollisionHandler.Instance.RemoveCollider(aquamentusCollider);
            RoomEnemies.Instance.Destroy(this,Location);
            aquamentusInfo.SetElementValue("Alive", "false");
            RoomDoors.Instance.OpenDoor(14);

            while(fireBallList.Count > 0)
            {
                fireBallList[fireBallList.Count-1].State.Die();
            }
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
                aquamentusSprite.AttackSprite();
                Sounds.Instance.Play("AquamentusRoar");
            }
            state.Update();
            foreach (FireBall fb in fireBallList){
                fb.Update();
            }
            sprite.Update();
            
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
