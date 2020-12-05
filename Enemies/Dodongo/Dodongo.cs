using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;

namespace Sprint5
{
    /// <summary>
    /// Author: Yuan Hong
    /// </summary>
    class Dodongo : IEnemy
    {
        public IEnemyState dodongoState;
        private ISprite dodongoSprite;
        private DodongoMovingSprite dodongoMovingSprite;
        private int dodongoHP;
        private Vector2 dodongoPos;
        private EnemyCollider dodongoCollider;
        private const int AttackStrength = HPAmount.Full_Heart;
        private string direction;
        private XElement dodongoInfo;
        private Point faceColliderSize = new Point(6, 6);
        private DodongoFaceCollider faceCollider;
        private string deathSoundFile = "BossScreams";
        private string damagedSoundFile = "DodongoRoar";
        private int changeDirecitonCounter = 600;

        public Vector2 Location { get => dodongoPos; }

        public IEnemyState State { get => dodongoState; }

        public List<ICollider> Colliders { get => new List<ICollider> { dodongoCollider, faceCollider }; }

        public Dodongo(Game game, Vector2 initialPos, XElement xml)
        {
            dodongoPos = initialPos;
            dodongoHP = 3;
            direction = "right";
            dodongoInfo = xml;
            dodongoState = new DodongoMovingState(this, initialPos);
            dodongoMovingSprite = (DodongoMovingSprite)dodongoSprite;
            dodongoCollider = new EnemyCollider(dodongoMovingSprite.GetRectangle(initialPos),this, AttackStrength);
            faceCollider = new DodongoFaceCollider(new Rectangle(0,0,6,6),dodongoState,this);
        }

        public string GetDirection()
        {
            return direction;
        }

        public Point UpdateFacePos()
        {
            Vector2 result;
            if(direction == "right")
            {
                result.X = dodongoPos.X + 32; 
                result.Y = dodongoPos.Y + 5;
            }
            else if(direction == "left")
            {
                result.X = dodongoPos.X - 6; 
                result.Y = dodongoPos.Y + 5;
            }
            else if(direction == "up")
            {
                result.X = dodongoPos.X + 5; 
                result.Y = dodongoPos.Y + 16;
            }
            else
            {
                result.X = dodongoPos.X + 5; 
                result.Y = dodongoPos.Y - 6;
            }
            return result.ToPoint();
        }

        public void UpdateDirection(string newDirection)
        {
            direction = newDirection;
            CollisionHandler.Instance.RemoveCollider(dodongoCollider);
            dodongoMovingSprite = (DodongoMovingSprite)dodongoSprite;
            dodongoCollider = new EnemyCollider(dodongoMovingSprite.GetRectangle(dodongoPos), this, AttackStrength);
            CollisionHandler.Instance.AddCollider(dodongoCollider, Layers.Enemy);
        }

        public void Update()
        {
            if(changeDirecitonCounter == 0)
            {
                dodongoState.ChangeDirection();
                changeDirecitonCounter = 210;
            }
            else
            {
                changeDirecitonCounter--;
            }
            dodongoState.Update();
        }

        public void UpdatePos(Vector2 newPos)
        {
            dodongoPos = newPos;
        }


        public void LostHP()
        {
            dodongoHP--;
            Sounds.Instance.Play(damagedSoundFile);
        }

        public Boolean checkAlive()
        {
            return dodongoHP >= 0;
        }

        public void Die()
        {
            Sounds.Instance.Play(deathSoundFile);
            CollisionHandler.Instance.RemoveCollider(dodongoCollider);
            CollisionHandler.Instance.RemoveCollider(faceCollider);
            RoomEnemies.Instance.Destroy(this, dodongoPos);
            dodongoInfo.SetElementValue("Alive", "false");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            dodongoSprite.Draw(spriteBatch, dodongoPos, 0, Color.White);
        }

        public void SetSprite(ISprite sprite)
        {
            dodongoSprite = sprite;
        }

        public void Spawn()
        {
        }

      
        public void TakeDamage(Direction dir, int amount)
        {
            dodongoState.TakeDamage(amount);
        }

        public void ObstacleCollision(Collision collision)
        {
            dodongoState.MoveAwayFromCollision(collision);
        }

        public void Stun()
        {
            dodongoState.Stun(false);
        }

    }
}
