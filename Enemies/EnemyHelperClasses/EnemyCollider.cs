using Microsoft.Xna.Framework;
using Sprint3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint3
{
    public class EnemyCollider : ICollider
    {
        private Rectangle bounds;
        private IEnemyState enemy;
        private int damageAmount;
        public string name;

        public string Name { get => name; }
        public Layer layer { get; set; }

        public EnemyCollider(Rectangle rect, IEnemyState enemy, int strength)
        {
            bounds = rect;

            this.enemy = enemy;

            damageAmount = strength;

            CollisionHandler.Instance.AddCollider(this);

            name = "Enemy";
        }

        public EnemyCollider(Rectangle rect, IEnemyState enemy, int strength, string name)
        {
            bounds = rect;

            this.enemy = enemy;
            this.name = name;
            damageAmount = strength;

            CollisionHandler.Instance.AddCollider(this, Layers.Enemy);
        }


        public EnemyCollider()
        {

        }

        public void ChangeState(IEnemyState state)
        {
            enemy = state;
        }

        public Rectangle Bounds()
        {
            return bounds;
            
        }

        public bool CompareTag(string tag)
        {
            return tag == name || tag == "Enemy";
        }

        public bool Equals(ICollider col)
        {
            return this == col;
        }

        public void HandleCollision(ICollider col, Collision collision)
        {
            
            if (col.CompareTag("Block") || col.CompareTag("Wall") || col.CompareTag("block") || col.CompareTag("wall") ||col.CompareTag("Trap"))
            {
                enemy.MoveAwayFromCollision(collision);
                
            }

            if (name.Equals("WallMaster") && col.CompareTag("Player")) { enemy.Attack(); col.SendMessage("Hand", (WallMasterMoveState)this.enemy); }
        }

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {
            string direction = collision.From().ToString();
            direction = char.ToUpper(direction[0]) + direction.Substring(1);

            if (col.CompareTag("Player")) col.SendMessage("TakeDamage" + direction, damageAmount);
            if (name.Equals("WallMaster") && col.CompareTag("Player")) { enemy.Attack(); col.SendMessage("Hand", (WallMasterMoveState)this.enemy); }
            else if (col.CompareTag("Item")) col.SendMessage("Impact", 0);
        }

        public void SendMessage(string msg, object value)
        {
            

            if (msg == "EnemyTakeDamage") enemy.TakeDamage((int)value);
            else if (msg == "Stun" && !Name.Equals("Keese")) enemy.Stun();
            else if (msg == "Stun") enemy.Die();
            else if (msg.Contains("EnemyTakeDamage"))
            { 
                string dir = msg.Substring(15);

                StalfosWalkingState stalfos = enemy as StalfosWalkingState;
                GoriyaMoveState goriya = enemy as GoriyaMoveState;

                if (stalfos != null) stalfos.TakeDamage(dir, (int)value);
                else if (goriya != null) goriya.TakeDamage(dir, (int)value);
                else enemy.TakeDamage((int)value);


            }
            



        }


        public void Update(IEnemy enemy)
        {
            this.enemy = enemy.State;
            bounds.Location = enemy.Location.ToPoint();
        }
    }
}
