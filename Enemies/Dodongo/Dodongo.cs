using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint3.Enemies.Dodongo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Sprint3
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
        private const int AttackStrength = 5;
        private string direction;
        private XElement dodongoInfo;
        private Point faceColliderSize = new Point(6, 6);
        private DodongoFaceCollider faceCollider;

        public Vector2 Location { get => dodongoPos; }

        public IEnemyState State { get => dodongoState; }

        public Dodongo(Game game, Vector2 initialPos, XElement xml)
        {
            dodongoPos = initialPos;
            dodongoHP = 2;
            direction = "Right";
            dodongoInfo = xml;
            dodongoState = new DodongoMovingState(this, initialPos);
            dodongoMovingSprite = (DodongoMovingSprite)dodongoSprite;
            dodongoCollider = new EnemyCollider(dodongoMovingSprite.GetRectangle(initialPos),dodongoState, AttackStrength);
            faceCollider = new DodongoFaceCollider(new Rectangle(0,0,6,6),dodongoState,AttackStrength);
        }

        public string GetDirection()
        {
            return direction;
        }

        public Point UpdateFacePos()
        {
            Vector2 result;
            if(direction == "Right")
            {
                result.X = dodongoPos.X + 32; 
                result.Y = dodongoPos.Y + 5;
            }
            else if(direction == "Left")
            {
                result.X = dodongoPos.X - 6; 
                result.Y = dodongoPos.Y + 5;
            }
            else if(direction == "Forward")
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
        }

        public void Update()
        {
            dodongoState.Update();
            dodongoCollider.Update(this);
            faceCollider.Update(UpdateFacePos());
        }

        public void UpdatePos(Vector2 newPos)
        {
            dodongoPos = newPos;
        }


        public void LostHP()
        {
            dodongoHP--;
        }

        public Boolean checkAlive()
        {
            return dodongoHP >= 0;
        }

        public void Die()
        {
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

        public EnemyCollider GetCollider()
        {
            return dodongoCollider;
        }
    }
}
